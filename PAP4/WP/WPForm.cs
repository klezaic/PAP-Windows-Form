using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WP
{
    public partial class WPForm : Form
    {
        StreamReader otvorenaDatoteka;
        int tipDatoteke;
        string imeDatoteke;
        int oznacenaBoja;

        public bool documentSearched = false;
        public string lastSearchText;
        public bool lastSearchDown;
        public bool lastSearchCase;


        FontFamily[] popisFontova;

        public WPForm()
        {
            InitializeComponent();
            this.popisFontova = FontFamily.Families;
            for (int i = 0; i < this.popisFontova.Length; i++)
                this.CBoxFont.Items.Add(this.popisFontova[i].Name);
            this.richTextBox_SelectionChanged(this, new EventArgs());

            string[] popisBoja = System.Enum.GetNames(typeof(KnownColor));

            for (int i = 0; i < popisBoja.Length; i++)
            {
                Color boja = Color.FromKnownColor((KnownColor)System.Enum.Parse(typeof(KnownColor), popisBoja[i]));
                if (boja.IsSystemColor == false && boja.Name != Color.Transparent.Name)
                    this.bttnFontColor.DropDownItems.Add(popisBoja[i]);
            }
        }

        //Novi dokument
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.otvorenaDatoteka != null)
                this.otvorenaDatoteka.Close();
            this.richTextBox.Clear();
            this.imeDatoteke = null;
            this.tipDatoteke = 0;
            this.Text = "Untitled - WP";
        }

        //Otvaranje postojeceg dokumenta
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.otvorenaDatoteka != null)
                    this.otvorenaDatoteka.Close();
                if (this.openFileDialog.FilterIndex == 1)
                {
                    this.richTextBox.LoadFile(this.openFileDialog.FileName, RichTextBoxStreamType.RichText);
                    this.tipDatoteke = 1;
                }
                else
                {
                    this.richTextBox.LoadFile(this.openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                    this.tipDatoteke = 2;
                }
                this.otvorenaDatoteka = new StreamReader(this.openFileDialog.FileName);
                this.imeDatoteke = this.openFileDialog.FileName;
                this.Text = this.openFileDialog.SafeFileName + " - WP";
            }
        }

        //Spremanje dokumenta pod novim imenom
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.otvorenaDatoteka != null)
                    this.otvorenaDatoteka.Close();
                if (this.saveFileDialog.FilterIndex == 1)
                {
                    this.richTextBox.SaveFile(this.saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                    this.tipDatoteke = 1;
                }
                else
                {
                    this.richTextBox.SaveFile(this.saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                    this.tipDatoteke = 2;
                }
                this.otvorenaDatoteka = new StreamReader(this.saveFileDialog.FileName);
                this.imeDatoteke = this.saveFileDialog.FileName;
                this.Text = this.saveFileDialog.FileName.Substring(this.saveFileDialog.FileName.LastIndexOf('\\') + 1) + " - WP";
            }
        }

        //Spremanje promjena na dokumentu
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.otvorenaDatoteka != null && this.imeDatoteke != null)
            {
                this.otvorenaDatoteka.Close();
                if (this.tipDatoteke == 1)
                    this.richTextBox.SaveFile(this.imeDatoteke, RichTextBoxStreamType.RichText);
                else
                    this.richTextBox.SaveFile(this.imeDatoteke, RichTextBoxStreamType.PlainText);
                this.otvorenaDatoteka = new StreamReader(this.imeDatoteke);
            }
            else
                this.saveAsToolStripMenuItem_Click(sender, e);
        }

        //Izlazak iz aplikacije
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Vracanje zadnju promjenu
        private void undoStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox.Undo();
        }

        //Kopiranje i brisanje oznacenog teksta
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox.Cut();
        }

        //Kopiranje oznacenog teksta
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox.Copy();
        }

        //Umetanje teksta iz meduspremnika na poziciju pokazivaca
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox.Paste();
        }

        //Brisanje oznacenog teksta
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox.SelectedText = "";
        }

        //Find-> otvara novu formu za pretrazivanje teksta
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindForm findForm = new FindForm();
            findForm.Owner = this;
            findForm.textBox.Text = this.richTextBox.SelectedText;
            findForm.Show();

            documentSearched = true;
        }

        //Promjena oznacenog fonta u ComboBox-u
        private void CBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.richTextBox.SelectionFont = new Font(this.popisFontova[this.CBoxFont.SelectedIndex], int.Parse(this.CBoxSize.SelectedItem.ToString()), this.richTextBox.Font.Style, this.richTextBox.Font.Unit);
            }
            catch { }
            finally
            {
                this.richTextBox.Focus();
            }
        }

        //Promjena oznacenog teksta
        private void richTextBox_SelectionChanged(object sender, EventArgs e)
        {

            this.textBox.Text = this.richTextBox.SelectedRtf;
            
            try
            {
                this.CBoxFont.SelectedItem = this.richTextBox.SelectionFont.FontFamily.Name;
                this.CBoxSize.SelectedItem = Math.Round(this.richTextBox.SelectionFont.Size).ToString();
            }
            catch { }
        }

        //Promjena oznacene velicine fonta u ComboBox-u
        private void CBoxSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.richTextBox.SelectionFont = new Font(this.richTextBox.SelectionFont.FontFamily, int.Parse(this.CBoxSize.SelectedItem.ToString()), this.richTextBox.Font.Style, this.richTextBox.Font.Unit);
            }
            catch { }
            finally
            {
                this.richTextBox.Focus();
            }
        }

        //Odabir boje fonta iz DropDown meni-a
        private void bttnFontColor_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.richTextBox.SelectionColor = Color.FromKnownColor((KnownColor)System.Enum.Parse(typeof(KnownColor), e.ClickedItem.Text));
            ((ToolStripMenuItem)this.bttnFontColor.DropDownItems[this.oznacenaBoja]).Checked = false;
            this.oznacenaBoja = this.bttnFontColor.DropDownItems.IndexOf((ToolStripMenuItem)e.ClickedItem);
            ((ToolStripMenuItem)e.ClickedItem).Checked = true;
        }

        //pomocna metoda koju FindForm koristi za pretrazivanje teksta
        protected internal bool findText(string text, bool downSearch, bool caseSensitive)
        {
            StringComparison stringComparison;
            int startIndex = richTextBox.SelectionStart;

            if (caseSensitive)
                stringComparison = StringComparison.Ordinal;
            else
                stringComparison = StringComparison.OrdinalIgnoreCase;

            if (downSearch)
            {
                if (richTextBox.Text.IndexOf(text, startIndex, stringComparison) >= 0)
                {
                    richTextBox.SelectionStart = richTextBox.Text.IndexOf(text, startIndex, stringComparison);
                    richTextBox.SelectionLength = text.Length;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (richTextBox.Text.LastIndexOf(text, startIndex, stringComparison) >= 0)
                {
                    if (startIndex > 0)
                        startIndex--;
                    else return false;
                    richTextBox.SelectionStart = richTextBox.Text.LastIndexOf(text, startIndex, stringComparison);
                    richTextBox.SelectionLength = text.Length;
                    return true;
                }
                else
                    return false;
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
        }



        
    }
}
