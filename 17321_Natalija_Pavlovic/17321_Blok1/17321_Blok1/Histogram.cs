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
	public partial class Histogram : Form
	{
		public Histogram()
		{
			InitializeComponent();
			button1.DialogResult = System.Windows.Forms.DialogResult.OK;
		}
		public int minC
		{
			get
			{
				if (textBox1.Text == "")
					return 0;
				else
					return (Convert.ToInt32(textBox1.Text));
			}
			set { textBox1.Text = value.ToString(); }
		}

		public int maxC
		{
			get
			{
				if (textBox2.Text == "")
					return 255;
				else return (Convert.ToInt32(textBox2.Text));
			}
			set { textBox2.Text = value.ToString(); }
		}
		public int minM
		{
			get
			{
				if (textBox3.Text == "")
					return 0;
				else
					return (Convert.ToInt32(textBox3.Text));
			}
			set { textBox3.Text = value.ToString(); }
		}

		public int maxM
		{
			get
			{
				if (textBox4.Text == "")
					return 255;
				else return (Convert.ToInt32(textBox4.Text));
			}
			set { textBox4.Text = value.ToString(); }
		}
		public int minY
		{
			get
			{
				if (textBox5.Text == "")
					return 0;
				else
					return (Convert.ToInt32(textBox5.Text));
			}
			set { textBox5.Text = value.ToString(); }
		}

		public int maxY
		{
			get
			{
				if (textBox6.Text == "")
					return 255;
				else return (Convert.ToInt32(textBox6.Text));
			}
			set { textBox6.Text = value.ToString(); }
		}

        private void button1_Click_1(object sender, EventArgs e)
        {
			if (int.Parse(textBox2.Text) > 255 || textBox2.Text=="")
			{
				textBox2.Text = "255";
			}
			else if (int.Parse(textBox2.Text) < 128)
			{
				textBox2.Text = "128";
			}
			if (int.Parse(textBox4.Text) > 255 || textBox4.Text == "")
			{
				textBox4.Text = "255";
			}
			else if (int.Parse(textBox4.Text) < 128)
			{
				textBox4.Text = "128";
			}
			if (int.Parse(textBox6.Text) > 255 || textBox6.Text == "")
			{
				textBox6.Text = "255";
			}
			else if (int.Parse(textBox6.Text) < 128)
			{
				textBox6.Text = "128";
			}
			button1.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
			if (int.Parse(textBox1.Text) > 127)
			{
				textBox1.Text = "127";
			}
			else if (int.Parse(textBox1.Text) < 0)
			{
				textBox1.Text = "0";
			}
		}

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
			if (int.Parse(textBox3.Text) > 127)
			{
				textBox3.Text = "127";
			}
			else if (int.Parse(textBox3.Text) < 0)
			{
				textBox3.Text = "0";
			}
		}

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
			if (int.Parse(textBox5.Text) > 127)
			{
				textBox5.Text = "127";
			}
			else if (int.Parse(textBox5.Text) < 0)
			{
				textBox5.Text = "0";
			}
		}
    }
}
