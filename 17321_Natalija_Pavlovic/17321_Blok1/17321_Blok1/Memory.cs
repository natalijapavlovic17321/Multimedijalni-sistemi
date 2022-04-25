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
    public partial class Memory : Form
    {
        public Memory()
        {
            InitializeComponent();
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Unositi samo brojeve.");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }
        public int Broj
        {
            get
            {
                return (Int32.Parse(textBox1.Text));
            }
            set { textBox1.Text = value.ToString(); }
        }
    }
}
