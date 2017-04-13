using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NP
{
    public partial class ReplaceForm : Form
    {
        public ReplaceForm()
        {
            InitializeComponent();
        }

        private void textBoxFindWhat_TextChanged(object sender, EventArgs e)
        {
            if (textBoxFindWhat.TextLength > 0)
            {
                buttonFindNext.Enabled = true;
                buttonReplace.Enabled = true;
                buttonReplaceAll.Enabled = true;
            }
            else
            {
                buttonFindNext.Enabled = false;
                buttonReplace.Enabled = false;
                buttonReplaceAll.Enabled = false;
            }
        }

        private void buttonFindNext_Click(object sender, EventArgs e)
        {
            if (((NP)this.Owner).findText(this.textBoxFindWhat.Text, true, this.checkBoxMatchCase.Checked) == false)
                MessageBox.Show("Can't find \'" + this.textBoxFindWhat.Text + "\'", "Find", MessageBoxButtons.OK);
            else
                ((NP)this.Owner).Focus();
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            ((NP)this.Owner).replaceText(this.textBoxReplaceWith.Text);
        }

        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            ((NP)this.Owner).replaceAllText(this.textBoxFindWhat.Text, this.textBoxReplaceWith.Text, this.checkBoxMatchCase.Checked);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }






    }
}
