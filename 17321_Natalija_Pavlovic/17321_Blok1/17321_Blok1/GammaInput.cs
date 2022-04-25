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
    public partial class GammaInput : Form
    {
        public GammaInput()
        {
            InitializeComponent();
			OK.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

        private void OK_Click(object sender, EventArgs e)
        {
            OK.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
		public double red
		{
			get
			{
				return (Convert.ToDouble(Red.Text));
			}
			set { Red.Text = value.ToString(); }
		}

		public double green
		{
			get
			{
				return (Convert.ToDouble(Green.Text));
			}
			set { Green.Text = value.ToString(); }
		}

		public double blue
		{
			get
			{
				return (Convert.ToDouble(Blue.Text));
			}
			set { Blue.Text = value.ToString(); }
		} 
	}
}
