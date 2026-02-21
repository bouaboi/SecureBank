using SecureBank.Forms;
using System;
using System.Windows.Forms;

namespace SecureBank
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmOpen());


            bool keepRunning = true;


            while (keepRunning)
            {
                frmLogin login = new frmLogin();

                if (login.ShowDialog() == DialogResult.OK)
                {
                    frmMain main = new frmMain();
                    Application.Run(main);

                    keepRunning = (main.DialogResult == DialogResult.OK);
                }
                else
                {
                    keepRunning = false; 
                }
            }





        }
    }
}
