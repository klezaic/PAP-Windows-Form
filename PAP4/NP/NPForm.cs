using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace NP
{
    public partial class NP : Form
    {
        //inicijalizacija objekata
        StreamReader procitajDatoteku;
        PrintDocument printDocument = new PrintDocument();
        
        string imeDatoteke = "Untitled";
        bool documentNotSaved = false;
        
        public bool documentSearched = false;
        public string lastSearchText;
        public bool lastSearchDown;
        public bool lastSearchCase;

        public NP()
        {
            InitializeComponent();
        }

        //NPForm
        //NPForm On Load -> NP_Load
        private void NP_Load(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem.Enabled = false;
            cutToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
            replaceToolStripMenuItem.Enabled = false;
            goToToolStripMenuItem.Enabled = false;
            wordWrapToolStripMenuItem.Checked = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            statusStrip.Visible = false;
            documentSearched = false;

            if (Clipboard.ContainsText())
                pasteToolStripMenuItem.Enabled = true;
            else
                pasteToolStripMenuItem.Enabled = false;

            if (this.textBox.TextLength > 0)
            {
                findToolStripMenuItem.Enabled = true;
                findNextToolStripMenuItem.Enabled = true;
                replaceToolStripMenuItem.Enabled = true;
            }
            else
            {
                findToolStripMenuItem.Enabled = false;
                findNextToolStripMenuItem.Enabled = false;
                replaceToolStripMenuItem.Enabled = false;
            }
        }

        //NPForm Close Button -> NP_FormClosing
        private void NP_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (documentNotSaved)
            {
                DialogResult unsavedChangesWarningDialog = MessageBox.Show("Do you want to save changes to " + imeDatoteke + "?", "NP", MessageBoxButtons.YesNoCancel);

                if (unsavedChangesWarningDialog == DialogResult.Yes)
                    saveAsToolStripMenuItem_Click(sender, e);
                else if (unsavedChangesWarningDialog == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }


        //TextBox
        //"Text changed" -> textBox
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            documentNotSaved = true;

            findNextToolStripMenuItem.Enabled = true;
            undoToolStripMenuItem.Enabled = true;

            if (this.textBox.TextLength > 0)
            {
                findToolStripMenuItem.Enabled = true;
                findNextToolStripMenuItem.Enabled = true;
                replaceToolStripMenuItem.Enabled = true;
            }
            else
            {
                findToolStripMenuItem.Enabled = false;
                findNextToolStripMenuItem.Enabled = false;
                replaceToolStripMenuItem.Enabled = false;
            }
            if (statusStrip.Visible == true)
            {
                int row = textBox.GetLineFromCharIndex(textBox.SelectionStart);
                int col = textBox.SelectionStart - textBox.GetFirstCharIndexFromLine(row);           

                toolStripStatusLabel.Text = string.Format("Row: {0} | Col:{1}", row + 1, col + 1);
                statusStrip.Refresh();
            }
        }

        //"Mouse Click" -> textBox
        private void textBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.textBox.SelectedText.Length > 0)
            {
                copyToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
            }
            else if (this.textBox.SelectedText.Length == 0)
            {
                copyToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
            if (statusStrip.Visible == true)
            {
                int row = textBox.GetLineFromCharIndex(textBox.SelectionStart);
                int col = textBox.SelectionStart - textBox.GetFirstCharIndexFromLine(row);

                toolStripStatusLabel.Text = string.Format("Row: {0} | Col:{1}", row + 1, col + 1);
                statusStrip.Refresh();
            }
        }
        
        //Menu "File" -> FileToolStripMenuItem
        //"New" -> newToolStripMenuItem
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.procitajDatoteku != null)
                this.procitajDatoteku.Close();
            this.imeDatoteke = "Untitled";
            this.textBox.Clear();
            this.Text = imeDatoteke + " - NP";

            NP_Load(sender, e);
        }

        //"Open..." -> openToolStripMenuItem
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.procitajDatoteku != null)
                    this.procitajDatoteku.Close();

                this.procitajDatoteku = new StreamReader(this.openFileDialog.FileName);
                this.textBox.Text = this.procitajDatoteku.ReadToEnd();
                this.imeDatoteke = this.openFileDialog.FileName;
                this.Text = this.openFileDialog.SafeFileName + " - NP";

                documentNotSaved = false;
                NP_Load(sender, e);

            }
        }

        //"Save" -> saveToolStripMenuItem
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.imeDatoteke == "Untitled")
                this.saveAsToolStripMenuItem_Click(sender, e);
            else
            {
                if (this.procitajDatoteku != null)
                    this.procitajDatoteku.Close();

                StreamWriter spremiDatoteku = new StreamWriter(this.imeDatoteke);
                spremiDatoteku.Write(this.textBox.Text);
                spremiDatoteku.Flush();
                spremiDatoteku.Close();

                this.procitajDatoteku = new StreamReader(this.imeDatoteke);
                this.Text = this.imeDatoteke.Substring(this.imeDatoteke.LastIndexOf('\\') + 1) + " -NP";

                documentNotSaved = false;
            }
        }
       
        //"Save As..." -> saveAsToolStripMenuItem
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveAsFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.procitajDatoteku != null)
                    this.procitajDatoteku.Close();

                StreamWriter spremiDatoteku = new StreamWriter(this.saveAsFileDialog.FileName);
                spremiDatoteku.Write(this.textBox.Text);
                spremiDatoteku.Flush();
                spremiDatoteku.Close();

                this.procitajDatoteku = new StreamReader(this.saveAsFileDialog.FileName);
                this.imeDatoteke = this.saveAsFileDialog.FileName;
                this.Text = this.saveAsFileDialog.FileName.Substring(this.saveAsFileDialog.FileName.LastIndexOf('\\') + 1) + " -NP";

                documentNotSaved = false;
            }
        }
       
        //"Page Setup" -> pageSetupToolStripMenuItem
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument.DocumentName = this.imeDatoteke;

            pageSetupDialog.Document = printDocument;

            if (this.pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;
                printDocument.PrinterSettings = pageSetupDialog.PrinterSettings;
            }
        }
        
        //"Print..." -> printToolStripMenuItem
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument.DocumentName = this.imeDatoteke;

            printDialog.Document = printDocument;
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = false;

            if (this.printDialog.ShowDialog() == DialogResult.OK)
                printDocument.Print();   
        }
        
        //"Exit" -> exitToolStripMenuItem
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (documentNotSaved)
            {
                DialogResult unsavedChangesWarningDialog = MessageBox.Show("Do you want to save changes to " + imeDatoteke + "?", "NP", MessageBoxButtons.YesNoCancel);

                if (unsavedChangesWarningDialog == DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(sender, e);
                    this.Close();
                }
                else if (unsavedChangesWarningDialog == DialogResult.No)
                    this.Close();
            }
            else
                this.Close();
        }

        //Menu "Edit" -> EditToolStripMenuItem
        //"Undo" -> undoToolStripMenuItem
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox.Undo();
        }
        
        //"Cut" -> cutToolStripMenuItem
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox.Cut();
        }
        
        //"Copy" -> copyToolStripMenuItem
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox.Copy();
        }
       
        //"Paste" -> pasteToolStripMenuItem
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox.Paste();
        }
        
        //"Delete" -> deleteToolStripMenuItem
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox.SelectedText = null;
        }
        
        //"Find..." -> findToolStripMenuItem
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindForm findForm = new FindForm();
            findForm.Owner = this;
            findForm.textBox.Text = this.textBox.SelectedText;
            findForm.Show();

            documentSearched = true;
        }
       
        //"Find Next"
        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (documentSearched)
            {
                if (findText(lastSearchText, lastSearchDown, lastSearchCase) == false)
                {
                    MessageBox.Show("Can't find \'" + lastSearchText + "\'", "Find", MessageBoxButtons.OK);
                    documentSearched = false;
                }
            }
            else
                findToolStripMenuItem_Click(sender, e);
        }
        
        //"Replace..." -> replaceToolStripMenuItem
        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceForm replaceForm = new ReplaceForm();
            replaceForm.Owner = this;
            replaceForm.textBoxFindWhat.Text = this.textBox.SelectedText;
            replaceForm.Show();
        }
        
        //"Go To..." -> goToToolStripMenuItem
        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
       
        //"Select All" -> selectAllToolStripMenuItem
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.SelectAll();
        }
        
        //"Time/Date" -> timeDateToolStripMenuItem
        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTimePicker dtp = new DateTimePicker();
            Controls.Add(dtp);

            textBox.SelectedText = dtp.Value.ToString();
        }
        

        //Menu "Format" -> FormatToolStripMenuItem
        //"Word Wrap" -> wordWrapToolStripMenuItem
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == true)
            {
                textBox.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
                textBox.ScrollBars = ScrollBars.Both;
            }
            else
            {
                textBox.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
                textBox.ScrollBars = ScrollBars.Vertical;
            }
        }
        
        //"Font" -> fontToolStripMenuItem
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult fontResult = fontDialog.ShowDialog();

            if (fontResult == DialogResult.OK)
                textBox.Font = fontDialog.Font;
        }


        //"View" -> ViewToolStripMenuItem
        //"Status Bar" -> statusBarToolStripMenuItem
        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusStrip.Visible == true)
            {
                statusStrip.Visible = false;
                statusBarToolStripMenuItem.Checked = false;
            }
            else 
            {
                statusStrip.Visible = true;
                statusBarToolStripMenuItem.Checked = true;
            }
        }


        //"Help" -> HelpToolStripMenuItem
        //"About NP" -> aboutNPToolStripMenuItem
        private void aboutNPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Windows Notepad clone application.\nNapravljeno za vježbe iz kolegija:\nProgramski Alati U Programiranju.", "About", MessageBoxButtons.OK);
        }
        

        //protected internal methods for "find" and "replace"
        //"Find text" -> returns true and selects text in TextBox, else returns false
        protected internal bool findText(string text, bool downSearch, bool caseSensitive)
        {
            StringComparison stringComparison;
            int startIndex = textBox.SelectionStart;

            if (caseSensitive)
                stringComparison = StringComparison.Ordinal;
            else
                stringComparison = StringComparison.OrdinalIgnoreCase;

            if (downSearch)
            {
                if (textBox.Text.IndexOf(text, startIndex + 1, stringComparison) >= 0)
                {
                    textBox.SelectionStart = textBox.Text.IndexOf(text, startIndex + 1, stringComparison);
                    textBox.SelectionLength = text.Length;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (textBox.Text.LastIndexOf(text, startIndex, stringComparison) >= 0)
                {
                    textBox.SelectionStart = textBox.Text.LastIndexOf(text, startIndex, stringComparison);
                    textBox.SelectionLength = text.Length;
                    return true;
                }
                else
                    return false;
            }
        }     
        
        //"Replace selected text"
        protected internal void replaceText(string text)
        {
            textBox.SelectedText = text;
        }
       
        //"Replace all text"
        protected internal void replaceAllText(string inputText, string outputText, bool caseSensitive)
        {
            textBox.SelectionStart = 0;

            while (findText(inputText, true, caseSensitive))
                textBox.SelectedText = outputText;
        }

        private void font1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();

            fontDialog1.Font = textBox.Font;

            DialogResult fontResult = fontDialog1.ShowDialog();

            if (fontResult == DialogResult.OK)
            {
                textBox.Font = fontDialog1.Font;
            }            
        }

        private void textBox_Move(object sender, EventArgs e)
        {
            
        }
    }
}
