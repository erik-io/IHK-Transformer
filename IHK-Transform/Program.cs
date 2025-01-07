using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHK_Transform
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AzubiService azubiService = new AzubiService();
            AzubiController azubiController = new AzubiController(azubiService);
            List<Azubi> azubis = azubiController.GetAzubi();
            foreach (Azubi azubi in azubis)
            {
                Console.WriteLine(azubi.ToString());
            }
            Console.ReadLine();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
