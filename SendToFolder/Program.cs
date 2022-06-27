using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendToFolder
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length < 1)
            {
                MessageBox.Show(
                $"At least one argument is needed",
                version(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Application.Run(new Form1(args));
            }
        }
        static string version()
        {
            //Alternative plattform descriptions
            //string dotNetFW = AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
            //string dotNetCore = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
            
            string fwDesc = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;

            return $"\n{fwDesc}";
        }
    }
}
