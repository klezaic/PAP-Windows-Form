using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Kalkulator
{
    public partial class KalkulatorForm : Form
    {
        private bool noviUnos = true;
        private bool unosOperand2 = false;

        private double operand1;
        private double operand2;

        private byte operacija = 0; 


        public KalkulatorForm()
        {
            InitializeComponent();
        }       

        //button s brojem
        private void buttonNum_Click(object sender, EventArgs e)
        {
            if (this.textBoxDisplay.Text != "0")
                this.textBoxDisplay.Text += ((Button)sender).Text;
            else
                this.textBoxDisplay.Text = ((Button)sender).Text;
        }

        //button s decimalnom tockom
        private void buttonDot_Click(object sender, EventArgs e)
        {
            if (this.textBoxDisplay.Text.Contains(',') == false)
                this.textBoxDisplay.Text += ",";
        }

        //button za mjenjanje predznaka
        private void buttonPlusMinus_Click(object sender, EventArgs e)
        {
            if (this.textBoxDisplay.Text != "0")
            {
                if (this.textBoxDisplay.Text.Contains('-') == true)
                    this.textBoxDisplay.Text = this.textBoxDisplay.Text.Remove(0, 1);
                else
                    this.textBoxDisplay.Text = "-" + this.textBoxDisplay.Text;
            }
        }

        //button za brisanje jednog znaka
        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (this.textBoxDisplay.Text != "0")
            {
                this.textBoxDisplay.Text = this.textBoxDisplay.Text.Remove(this.textBoxDisplay.Text.Length - 1);
                if (this.textBoxDisplay.Text.Length == 0 || this.textBoxDisplay.Text == "-")
                    this.textBoxDisplay.Text = "0";
            }
        }

        //button za brisanje unosa 
        private void buttonC_Click(object sender, EventArgs e)
        {
            this.textBoxDisplay.Text = "0";
            this.noviUnos = true;
        }

        //button za resitiranje
        private void buttonCE_Click(object sender, EventArgs e)
        {
            this.noviUnos = true;
            this.operacija = 0;
            this.operand1 = 0;
            this.operand2 = 0;
            this.textBoxDisplay.Text = "0";
        }

        //button za odabir matematicke operacije; "+,-,*,/" tipka
        private void buttonOperacija_Click(object sender, EventArgs e)
        {
            if (this.unosOperand2 == false)
                double.TryParse(this.textBoxDisplay.Text, out this.operand1);
            else this.buttonEquals_Click(sender, e);

            switch (((Button)sender).Text)
            {
                case "+":
                    this.operacija = 1;
                    break;
                case "-":
                    this.operacija = 2;
                    break;
                case "*":
                    this.operacija = 3;
                    break;
                case "/":
                    this.operacija = 4;
                    break;
            }
            this.noviUnos = true;
            this.textBoxDisplay.Text = "0";
            this.unosOperand2 = true;
        }

        //button za izvrsavanje unesene operacije; "="
        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (this.noviUnos == true)
                double.TryParse(this.textBoxDisplay.Text, out this.operand2);
            switch (this.operacija)
            {
                case 1:
                    this.operand1 = this.operand1 + this.operand2;
                    break;
                case 2:
                    this.operand1 = this.operand1 - this.operand2;
                    break;
                case 3:
                    this.operand1 = this.operand1 * this.operand2;
                    break;
                case 4:
                    this.operand1 = this.operand1 / this.operand2;
                    break;
            }
            this.textBoxDisplay.Text = this.operand1.ToString();
            this.noviUnos = true;
            this.unosOperand2 = false;
        }

        //KeyDown metoda, switchCase-om pomocu imena pritisnutog objekta poziva prikladnu metodu
        private void KalkulatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            string numberPressed = e.KeyData.ToString();
            int number;

            if (numberPressed.StartsWith("D") == true || numberPressed.StartsWith("Num") == true)
            {
            }*/

            switch (e.KeyCode)
            {
                case Keys.NumPad0:
                case Keys.D0:
                    this.buttonNum_Click(this.button0, new EventArgs());
                    this.button0.Focus();
                    e.Handled = true;
                    break;

                case Keys.NumPad1:
                case Keys.D1:
                    this.buttonNum_Click(this.button1, new EventArgs());
                    this.button1.Focus();
                    e.Handled = true;
                    break;

                case Keys.NumPad2:
                case Keys.D2:
                    this.buttonNum_Click(this.button2, new EventArgs());
                    this.button2.Focus();
                    e.Handled = true;
                    break;

                case Keys.NumPad3:
                case Keys.D3:
                    this.buttonNum_Click(this.button3, new EventArgs());
                    this.button3.Focus();
                    e.Handled = true;
                    break;

                case Keys.NumPad4:
                case Keys.D4:
                    this.buttonNum_Click(this.button4, new EventArgs());
                    this.button4.Focus();
                    e.Handled = true;
                    break;

                case Keys.NumPad5:
                case Keys.D5:
                    this.buttonNum_Click(this.button5, new EventArgs());
                    this.button5.Focus();
                    e.Handled = true;
                    break;

                case Keys.NumPad6:
                case Keys.D6:
                    this.buttonNum_Click(this.button6, new EventArgs());
                    this.button6.Focus();
                    e.Handled = true;
                    break;

                case Keys.NumPad7:
                case Keys.D7:
                    this.buttonNum_Click(this.button7, new EventArgs());
                    this.button7.Focus();
                    e.Handled = true;
                    break;

                case Keys.NumPad8:
                case Keys.D8:
                    this.buttonNum_Click(this.button8, new EventArgs());
                    this.button8.Focus();
                    e.Handled = true;
                    break;

                case Keys.NumPad9:
                case Keys.D9:
                    this.buttonNum_Click(this.button9, new EventArgs());
                    this.button9.Focus();
                    e.Handled = true;
                    break;

                case Keys.Decimal:
                case Keys.Oemcomma:
                case Keys.OemPeriod:
                    this.buttonDot_Click(this.buttonDot, new EventArgs());
                    this.buttonDot.Focus();
                    e.Handled = true;
                    break;

                case Keys.Back:
                    this.buttonBack_Click(this.buttonBack, new EventArgs());
                    this.buttonBack.Focus();
                    e.Handled = true;
                    break;

                case Keys.Delete:
                    this.buttonC_Click(this.buttonC, new EventArgs());
                    this.buttonC.Focus();
                    e.Handled = true;
                    break;

                case Keys.Oemplus:
                case Keys.Add:
                    this.buttonOperacija_Click(this.buttonPlus, new EventArgs());
                    this.buttonPlus.Focus();
                    e.Handled = true;
                    break;

                case Keys.OemMinus:
                case Keys.Subtract:
                    this.buttonOperacija_Click(this.buttonMinus, new EventArgs());
                    this.buttonMinus.Focus();
                    e.Handled = true;
                    break;

                case Keys.Multiply:
                    this.buttonOperacija_Click(this.buttonMul, new EventArgs());
                    this.buttonMul.Focus();
                    e.Handled = true;
                    break;

                case Keys.Divide:
                    this.buttonOperacija_Click(this.buttonDiv, new EventArgs());
                    this.buttonDiv.Focus();
                    e.Handled = true;
                    break;

                case Keys.F11:
                    this.buttonPlusMinus_Click(this.buttonPlusMinus, new EventArgs());
                    this.buttonPlusMinus.Focus();
                    e.Handled = true;
                    break;

                case Keys.F12:
                    this.buttonEquals_Click(this.buttonEquals, new EventArgs());
                    this.buttonEquals.Focus();
                    e.Handled = true;
                    break;

                case Keys.Return:
                    this.buttonEquals_Click(this.buttonEquals, new EventArgs());
                    this.buttonEquals.Focus();
                    e.Handled = true;
                    break;             
                    

                default:
                    MessageBox.Show(e.KeyCode.ToString());
                    break;

            }
            
            this.buttonEquals.Focus();
            this.buttonEquals.Enabled = false;
            this.buttonEquals.Enabled = true;
        }
    }
}
