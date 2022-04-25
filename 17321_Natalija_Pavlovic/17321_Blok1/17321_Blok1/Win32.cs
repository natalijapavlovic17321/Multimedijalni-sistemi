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
    public partial class Win32 : Form
    {
        public Win32()
        {
            InitializeComponent();
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        public string Option
        {
            get
            {
                if (radioButton1.Checked)
                    return radioButton1.Text;
                else return radioButton2.Text;
            }
        }
    }
}
