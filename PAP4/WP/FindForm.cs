using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WP
{
    public partial class FindForm : Form
    {
        public FindForm()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.TextLength > 0)
                buttonFindNext.Enabled = true;
            else
                buttonFindNext.Enabled = false;

            //Expanding TextBox depending on iWPut text
            float size = textBox.Font.SizeInPoints;

            if (textBox.Width < (int)(size * textBox.TextLength))
                if (textBox.Width < this.Width - 200)
                    textBox.Width = (int)(size * textBox.TextLength);
                else textBox.Width = this.Width - 200;

        }

        private void buttonFindNext_Click(object sender, EventArgs e)
        {
            if (((WPForm)this.Owner).findText(this.textBox.Text, this.radioButtonDown.Checked, this.checkBoxMatchCase.Checked) == false)
                MessageBox.Show("Can't find \'" + this.textBox.Text + "\'", "Find", MessageBoxButtons.OK);
            else
                ((WPForm)this.Owner).Focus();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ((WPForm)this.Owner).documentSearched = false;
            this.Close();
        }

        private void FindForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((WPForm)this.Owner).lastSearchText = textBox.Text;
            ((WPForm)this.Owner).lastSearchDown = radioButtonDown.Checked;
            ((WPForm)this.Owner).lastSearchCase = checkBoxMatchCase.Checked;
        }


    }
}
