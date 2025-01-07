using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHK_Transform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AzubiService azubiService = new AzubiService();
            AzubiController azubiController = new AzubiController(azubiService);
            List<Azubi> azubis = azubiController.GetAzubi();
            azubiController.DisplayAzubis();
        }
    }
}
