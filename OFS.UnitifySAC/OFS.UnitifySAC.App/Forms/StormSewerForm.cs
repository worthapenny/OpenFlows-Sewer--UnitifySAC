using Haestad.Framework.Windows.Forms.Forms;
using Haestad.Framework.Windows.Forms.Resources;
using Haestad.Support.Support;
using Haestad.Support.User;
using OpenFlows.StormSewer.Domain;
using Serilog;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OFS.UnitifySAC.App.Forms
{
    public partial class StormSewerForm : HaestadParentForm
    {
        #region Constructor
        public StormSewerForm()
        {
            InitializeComponent();
            Log.Debug($"{nameof(StormSewerForm)} class initialized");
        }
        #endregion

        #region Private Overridden Methods
        protected override bool CheckForUnsavedChanges(bool aboolAll)
        {
            return true;
        }
        protected override void InitializeVisually()
        {
            Icon = (Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Interpolate];

            this.toolStripButtonUnitifySAC.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.FolderOpen]).ToBitmap();
            this.toolStripButtonOpen.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.FolderOpen]).ToBitmap();
            this.toolStripButtonSave.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Save]).ToBitmap();
            this.toolStripButtonSaveAs.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.SaveToGraphManager]).ToBitmap();
            this.toolStripButtonUnitifySAC.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Interpolate]).ToBitmap();
            this.toolStripButtonOpenInMainApp.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Bentley16]).ToBitmap();
            this.toolStripButtonShowLog.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Log]).ToBitmap();

            Text = "Unitify Scenarios, Alternatives, and Calc. Options (SAC)";

            var basicSteps = "\nSteps:\n";
            basicSteps += "1. Open up the model for a clean-up (to keep the active SAC, and delete the rest.)\n";
            basicSteps += "2. Click on 'Unitify SAC' button\n";
            basicSteps += "3. Either click on 'Save' or 'Save As'\n";
            basicSteps += "4. To view the change made in the main application, click on 'Open in Main App'.\n\n";

            basicSteps += "For any questions or comments, submit an issue on GitHub, and thanks.";
            this.richTextBox.Text = basicSteps;

            // Disable the toolbar buttons
            EnableControls(false);

            Log.Debug($"{nameof(InitializeVisually)} completed");
        }
        protected override void InitializeEvents()
        {
            // Open model
            this.toolStripButtonOpen.Click += async (o, e) => await OpenHydraulicModelAsync();

            // Save
            this.toolStripButtonSave.Click += (o, e) => SSModel?.Save();


            // Save As
            this.toolStripButtonSaveAs.Click += (o, e) =>
             {
                 var fileDialog = new SaveFileDialog();
                 fileDialog.Filter = "StormSewer Files (*.stsw)|*.stsw";
                 if (fileDialog.ShowDialog() == DialogResult.OK && SSModel != null)
                 {
                     SSModel.SaveAs(fileDialog.FileName);
                     Log.Information($"Model saved to a new file: {fileDialog.FileName}");
                     Log.Debug(new string('-', 100));
                 }
             };

            // Unitify
            this.toolStripButtonUnitifySAC.Click +=  (o, e) => RunUnitifySAC();

            // Open in main application
            this.toolStripButtonOpenInMainApp.Click += (o, e) => OpenInMainApplication();

            // Open up log
            this.toolStripButtonShowLog.Click += (s, e) => ShowLogFile();

            Log.Debug($"{nameof(InitializeEvents)} completed");
        }

        protected override void HaestadForm_Closing(object sender, CancelEventArgs acea)
        {
            Log.Debug($"Form is closing...");

            SSModel?.Dispose();
            Log.Debug($"Hydraulic model disposed.");

            base.HaestadForm_Closing(sender, acea);
        }
        public override void CloseApplication()
        {
            Log.Debug($"Application is about to exit");
            base.CloseApplication();
        }
        #endregion

        #region Private Method
        private void EnableControls(bool enable)
        {
            this.toolStripButtonOpen.Enabled = !enable;
            this.toolStripButtonSave.Enabled = enable;
            this.toolStripButtonSaveAs.Enabled = enable;
            this.toolStripButtonUnitifySAC.Enabled = enable;
            this.toolStripButtonOpenInMainApp.Enabled = enable;

            //this.toolStripButtonShowLog.Enabled = enable;
        }

        //
        // Open Model
        private async Task<bool> OpenHydraulicModelAsync()
        {
            try
            {
                EnterOrUpdateLongRunningOperation("About to open up the model...");

                if (SSModel != null)
                    SSModel.Close();

                var openFileDialog = NewOpenFileDialog();
                openFileDialog.Filter = "StormSewer Files (*.stsw)|*.stsw";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    try
                    {
                        Log.Debug($"About to open up a model filefrom: {openFileDialog.FileName}");

                        SSModel = await Task.Run(() =>
                        {
                            return OpenFlows.StormSewer.OpenFlowsStormSewer.Open(openFileDialog.FileName);
                        });

                        if (SSModel != null)
                        {
                            EnableControls(true);
                            EnterOrUpdateLongRunningOperation($"Model is opened!");
                            
                            Log.Information($"Model file opened from: {openFileDialog.FileName}");
                            MessageBox.Show(this, $"Model file: {SSModel.ModelInfo.Filename}\n...is opened. \n\nReady to click on 'Unitify SAC'.", "Model opened", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            var message = $"Failed to open up the model from: {openFileDialog.FileName}";
                            Log.Error(message);
                            MessageBox.Show(this, message, "Failed to open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, $"...while openig up the model file from: {openFileDialog.FileName}");
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"...while openig up the model.");
            }
            finally
            {
                var message = SSModel != null ? "Model is opened!" : "Model is not opened.";
                EndLongRunningOperation(message);
            }

            Log.Debug(new string('-', 100));
            return SSModel != null;
        }

        //
        // Open in main application (SewerGEMS)
        private void OpenInMainApplication()
        {
            var process = Process.Start(SSModel.ModelInfo.Filename);
            if (process == null)
            {
                var message = "Failed to start the application, please try manually.";
                Log.Warning(message);
                MessageBox.Show(this, message, "Failed to open", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Log.Debug($"External application opened: ID: {process.Id}, Name: {process.ProcessName}");
            }

            Log.Debug(new string('-', 100));
        }

        //
        // Run Unitify
        private void RunUnitifySAC()
        {
            var pi = new ProgressIndicatorForm(true, this);

            try
            {
                EnterOrUpdateLongRunningOperation("About to unitify the SAC in the model...");
                statusStrip.Update();

                pi.CanCancel = false;
                pi.Show(this);

               UnitifyScenarioAlternativeCalcOptions(pi);

                Log.Debug(new string('=', 100));
            }
            finally
            {
                pi.Done();
                EndLongRunningOperation("SAC unitified");
            }
        }

        //
        // Unitify
        private void UnitifyScenarioAlternativeCalcOptions(IProgressIndicator pi)
        {
            var unitifier = new Domain.UnitifyScenariosAlternativesCalcOptions(SSModel);
            var success = unitifier.Unitify(pi);
            if (success)
            {
                var message = "Simplified Scenarios, Alternatives, and Calc Options.\n\nClick on Save or Save As.\nClick on Open in main UI application to see the changes.";
                MessageBox.Show(this, message, "Modified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.Debug(message);
            }
            else
            {
                var message = "Please see to logs to find out what went wrong. Thanks.";
                MessageBox.Show(this, message, "Something didn't go the right way", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Log.Debug(message);
            }
        }


        //
        // Show the log file
        private void ShowLogFile()
        {
            var datetime = $"{DateTime.Now:yyyyMMdd}";
            var logFilePattern = Path.Combine(Program.LOG_DIRECTORY, Program.LOG_FILE_PATTERN);
            var logFilePath = logFilePattern.Replace(".txt", $"{datetime}.txt");

            if (File.Exists(logFilePath))
                Process.Start(logFilePath);
            else
            {
                var message = $"Log file couldn't be located. Please check this director: {Program.LOG_DIRECTORY}";
                MessageBox.Show(this, message, "Log file loation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Log.Warning(message);
            }
        }
        private void EnterOrUpdateLongRunningOperation(string message)
        {
            this.toolStripProgressBar.Visible = true;
            this.toolStripStatusProgressLabel.Text = message;
        }
        private void EndLongRunningOperation(string message)
        {
            this.toolStripProgressBar.Visible = false;
            this.toolStripStatusProgressLabel.Text = message;
        }
        #endregion

        #region Private Properties
        private IStormSewerModel SSModel { get; set; }

        #endregion

    }
}
