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
    public partial class Parametar : Form
    {
        public Parametar()
        {
            InitializeComponent();
            OK.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            OK.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        public int nValue
        {
            get
            {
                return (Convert.ToInt32(Value.Text, 10));
            }
            set { Value.Text = value.ToString(); }
        }
    }
}
