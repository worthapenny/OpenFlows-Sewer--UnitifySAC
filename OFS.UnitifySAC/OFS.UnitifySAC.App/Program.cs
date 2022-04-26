using OFS.UnitifySAC.App.Forms;
using OpenFlows.StormSewer;
using Serilog;
using Serilog.Formatting.Display;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Sinks.WinForms.Base;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OFS.UnitifySAC.App
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        public static string LOG_FILE_PATTERN = "UnitifySAC_Log_.txt";
        public static string LOG_DIRECTORY = Path.Combine(Path.GetTempPath(), "__UnitifySAC");


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static int Main()
        {
            // redirect console output to parent process;
            // must be before any calls to Console.WriteLine()
            AttachConsole(ATTACH_PARENT_PROCESS);

            var logFileFile = Path.Combine(LOG_DIRECTORY, LOG_FILE_PATTERN);
            var logTemplate = "{Timestamp:HH:mm:ss.ff} | {Level:u3} | {Message}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteToSimpleAndRichTextBox(new MessageTemplateTextFormatter(logTemplate))
                .WriteTo.Console(outputTemplate: logTemplate, theme: AnsiConsoleTheme.Code)
                .WriteTo.File(
                    logFileFile,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    flushToDiskInterval: TimeSpan.FromSeconds(5),
                    outputTemplate: logTemplate)
                .CreateLogger();

            Log.Debug("");
            Log.Debug($"Log file initialized at: {logFileFile}");


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OpenFlowsStormSewer.StartSession(StormSewerProductLicenseType.SewerGEMS);
            Log.Information($"OpenFlows session started for {StormSewerProductLicenseType.SewerGEMS}");

            Application.Run(new StormSewerForm());

            OpenFlowsStormSewer.EndSession();
            //Log.Information($"OpenFlows session ended for {StormSewerProductLicenseType.SewerGEMS}");

            //Log.Debug($"Application is about to exit");
            return 0;
        }
    }
}
