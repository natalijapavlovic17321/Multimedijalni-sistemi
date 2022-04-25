using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _17321_Blok1
{
    public partial class HueSaturation : Form
    {
        public HueSaturation()
        {
            InitializeComponent();
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

		public double Hue
		{
			get
			{
				return (Convert.ToDouble(textBox1.Text));
			}
			set { textBox1.Text = value.ToString(); }
		}

		public double Saturation
		{
			get
			{
				if (textBox2.Text == "")
					return 101;
				else return (Convert.ToDouble(textBox2.Text));
			}
			set { textBox2.Text = value.ToString(); }
		}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
			if (double.Parse(textBox1.Text) > 5)
			{
				textBox1.Text = "5";
			}
			else if (double.Parse(textBox1.Text) < -1)
			{
				textBox1.Text = "-1";
			}
		}

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
			if (double.Parse(textBox2.Text) > 1.0)
			{
				textBox2.Text = "1.0";
			}
			else if (double.Parse(textBox2.Text) < 0.0)
			{
				textBox2.Text = "0.0";
			}
		}
    }
}
