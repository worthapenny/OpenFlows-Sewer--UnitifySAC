/*using Haestad.Framework.Windows.Forms.Resources;
using Haestad.Support.Support;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OFS.UnitifySAC.App.Components
{
    public partial class ToolStripMain : ToolStrip
    {

        public event EventHandler<string> OpenFileClicked;
        public event EventHandler SaveClicked;
        public event EventHandler<string> SaveAsClicked;
        public event EventHandler OpenInWinAppClicked;

        public ToolStripMain()
        {
            InitializeComponent();

            this.toolStripButtonOpen.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.FolderOpen]).ToBitmap();
            this.toolStripButtonSave.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Save]).ToBitmap();
            this.toolStripButtonSaveAs.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.SaveToGraphManager]).ToBitmap();
            this.toolStripButtonOpenInWinApp.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Bentley16]).ToBitmap();

        }


        #region Public Methods
        public void EnableControls(bool enable)
        {
            this.toolStripButtonSave.Enabled = enable;
            this.toolStripButtonSaveAs.Enabled = enable;
            this.toolStripButtonOpenInWinApp.Enabled = enable;
        }
        #endregion

        #region Private Methods
        private OpenFileDialog NewOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "";
            openFileDialog.Title = "Select hydraulic model file.";
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "StormSewer Files (*.stsw)|*.stsw";

            return openFileDialog;
        }
        #endregion

        #region Event Handlers
        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            var openFileDialog = NewOpenFileDialog();
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                toolStripButtonOpen.Enabled = false;

                ModelFilePath = openFileDialog.FileName;
                OpenFileClicked?.Invoke(this, ModelFilePath);
            }
        }
        #endregion

        #region Public Properties
        public string ModelFilePath { get; set; }
        #endregion
    }
}
*/