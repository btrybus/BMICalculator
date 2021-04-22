using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMICalulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
            double masa = double.Parse(txtMasa.Text);
            double wzrost = double.Parse(txtWzrost.Text);

            string opis = "OK :)";

            double BMI = masa / ((wzrost / 100.0) * (wzrost / 100.0));

            if (BMI < 18.5)
                opis = "niedowaga";
            else if (BMI > 30)
                opis = "otyłość";
            else if (BMI >= 25)
                opis = "nadwaga";

            //MessageBox.Show(opis);
            txtWynik.Text = opis;

        }
    }
}
