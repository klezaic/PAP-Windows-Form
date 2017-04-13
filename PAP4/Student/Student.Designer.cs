namespace Student
{
    partial class Student
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelGornji = new System.Windows.Forms.Panel();
            this.panelNavigacija = new System.Windows.Forms.Panel();
            this.textBoxTrenutniRed = new System.Windows.Forms.TextBox();
            this.bttnNextRecord = new System.Windows.Forms.Button();
            this.bttnLastRecord = new System.Windows.Forms.Button();
            this.btnPreviousRecord = new System.Windows.Forms.Button();
            this.bttnFirstRecord = new System.Windows.Forms.Button();
            this.cBoxTablica = new System.Windows.Forms.ComboBox();
            this.labelTablica = new System.Windows.Forms.Label();
            this.splitCPodaci = new System.Windows.Forms.SplitContainer();
            this.dgvPodaci = new System.Windows.Forms.DataGridView();
            this.tablicaPodaciRed = new System.Windows.Forms.TableLayoutPanel();
            this.bttnDelete = new System.Windows.Forms.Button();
            this.bttnOdustani = new System.Windows.Forms.Button();
            this.bttnZapisiRed = new System.Windows.Forms.Button();
            this.bttnNew = new System.Windows.Forms.Button();
            this.comboBoxNaslovi = new System.Windows.Forms.ComboBox();
            this.panelGornji.SuspendLayout();
            this.panelNavigacija.SuspendLayout();
            this.splitCPodaci.Panel1.SuspendLayout();
            this.splitCPodaci.Panel2.SuspendLayout();
            this.splitCPodaci.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPodaci)).BeginInit();
            this.SuspendLayout();
            // 
            // panelGornji
            // 
            this.panelGornji.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGornji.Controls.Add(this.comboBoxNaslovi);
            this.panelGornji.Controls.Add(this.panelNavigacija);
            this.panelGornji.Controls.Add(this.cBoxTablica);
            this.panelGornji.Controls.Add(this.labelTablica);
            this.panelGornji.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGornji.Location = new System.Drawing.Point(0, 0);
            this.panelGornji.Name = "panelGornji";
            this.panelGornji.Size = new System.Drawing.Size(784, 50);
            this.panelGornji.TabIndex = 0;
            // 
            // panelNavigacija
            // 
            this.panelNavigacija.AccessibleDescription = "";
            this.panelNavigacija.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNavigacija.Controls.Add(this.textBoxTrenutniRed);
            this.panelNavigacija.Controls.Add(this.bttnNextRecord);
            this.panelNavigacija.Controls.Add(this.bttnLastRecord);
            this.panelNavigacija.Controls.Add(this.btnPreviousRecord);
            this.panelNavigacija.Controls.Add(this.bttnFirstRecord);
            this.panelNavigacija.Location = new System.Drawing.Point(520, 9);
            this.panelNavigacija.Name = "panelNavigacija";
            this.panelNavigacija.Size = new System.Drawing.Size(259, 27);
            this.panelNavigacija.TabIndex = 2;
            // 
            // textBoxTrenutniRed
            // 
            this.textBoxTrenutniRed.Location = new System.Drawing.Point(69, 3);
            this.textBoxTrenutniRed.Name = "textBoxTrenutniRed";
            this.textBoxTrenutniRed.Size = new System.Drawing.Size(121, 20);
            this.textBoxTrenutniRed.TabIndex = 4;
            // 
            // bttnNextRecord
            // 
            this.bttnNextRecord.AutoSize = true;
            this.bttnNextRecord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bttnNextRecord.Location = new System.Drawing.Point(196, 1);
            this.bttnNextRecord.Name = "bttnNextRecord";
            this.bttnNextRecord.Size = new System.Drawing.Size(23, 23);
            this.bttnNextRecord.TabIndex = 3;
            this.bttnNextRecord.Text = ">";
            this.bttnNextRecord.UseVisualStyleBackColor = true;
            this.bttnNextRecord.Click += new System.EventHandler(this.promjenaReda);
            // 
            // bttnLastRecord
            // 
            this.bttnLastRecord.AutoSize = true;
            this.bttnLastRecord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bttnLastRecord.Location = new System.Drawing.Point(225, 1);
            this.bttnLastRecord.Name = "bttnLastRecord";
            this.bttnLastRecord.Size = new System.Drawing.Size(29, 23);
            this.bttnLastRecord.TabIndex = 2;
            this.bttnLastRecord.Text = ">>";
            this.bttnLastRecord.UseVisualStyleBackColor = true;
            this.bttnLastRecord.Click += new System.EventHandler(this.promjenaReda);
            // 
            // btnPreviousRecord
            // 
            this.btnPreviousRecord.AutoSize = true;
            this.btnPreviousRecord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPreviousRecord.Location = new System.Drawing.Point(39, 1);
            this.btnPreviousRecord.Name = "btnPreviousRecord";
            this.btnPreviousRecord.Size = new System.Drawing.Size(23, 23);
            this.btnPreviousRecord.TabIndex = 1;
            this.btnPreviousRecord.Text = "<";
            this.btnPreviousRecord.UseVisualStyleBackColor = true;
            this.btnPreviousRecord.Click += new System.EventHandler(this.promjenaReda);
            // 
            // bttnFirstRecord
            // 
            this.bttnFirstRecord.AutoSize = true;
            this.bttnFirstRecord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bttnFirstRecord.Location = new System.Drawing.Point(4, 1);
            this.bttnFirstRecord.Name = "bttnFirstRecord";
            this.bttnFirstRecord.Size = new System.Drawing.Size(29, 23);
            this.bttnFirstRecord.TabIndex = 0;
            this.bttnFirstRecord.Text = "<<";
            this.bttnFirstRecord.UseVisualStyleBackColor = true;
            this.bttnFirstRecord.Click += new System.EventHandler(this.promjenaReda);
            // 
            // cBoxTablica
            // 
            this.cBoxTablica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxTablica.FormattingEnabled = true;
            this.cBoxTablica.Location = new System.Drawing.Point(63, 9);
            this.cBoxTablica.Name = "cBoxTablica";
            this.cBoxTablica.Size = new System.Drawing.Size(121, 21);
            this.cBoxTablica.TabIndex = 1;
            this.cBoxTablica.SelectedIndexChanged += new System.EventHandler(this.cBoxTablica_SelectedIndexChanged);
            // 
            // labelTablica
            // 
            this.labelTablica.AutoSize = true;
            this.labelTablica.Location = new System.Drawing.Point(12, 12);
            this.labelTablica.Name = "labelTablica";
            this.labelTablica.Size = new System.Drawing.Size(45, 13);
            this.labelTablica.TabIndex = 0;
            this.labelTablica.Text = "Tablica:";
            // 
            // splitCPodaci
            // 
            this.splitCPodaci.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitCPodaci.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCPodaci.Location = new System.Drawing.Point(0, 50);
            this.splitCPodaci.Name = "splitCPodaci";
            // 
            // splitCPodaci.Panel1
            // 
            this.splitCPodaci.Panel1.Controls.Add(this.dgvPodaci);
            // 
            // splitCPodaci.Panel2
            // 
            this.splitCPodaci.Panel2.AutoScroll = true;
            this.splitCPodaci.Panel2.Controls.Add(this.tablicaPodaciRed);
            this.splitCPodaci.Panel2.Controls.Add(this.bttnDelete);
            this.splitCPodaci.Panel2.Controls.Add(this.bttnOdustani);
            this.splitCPodaci.Panel2.Controls.Add(this.bttnZapisiRed);
            this.splitCPodaci.Panel2.Controls.Add(this.bttnNew);
            this.splitCPodaci.Size = new System.Drawing.Size(784, 512);
            this.splitCPodaci.SplitterDistance = 512;
            this.splitCPodaci.TabIndex = 1;
            // 
            // dgvPodaci
            // 
            this.dgvPodaci.AllowUserToAddRows = false;
            this.dgvPodaci.AllowUserToDeleteRows = false;
            this.dgvPodaci.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPodaci.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPodaci.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPodaci.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPodaci.Location = new System.Drawing.Point(0, 0);
            this.dgvPodaci.MultiSelect = false;
            this.dgvPodaci.Name = "dgvPodaci";
            this.dgvPodaci.ReadOnly = true;
            this.dgvPodaci.RowHeadersVisible = false;
            this.dgvPodaci.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPodaci.Size = new System.Drawing.Size(510, 510);
            this.dgvPodaci.TabIndex = 0;
            this.dgvPodaci.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPodaci_CellEnter);
            // 
            // tablicaPodaciRed
            // 
            this.tablicaPodaciRed.AutoSize = true;
            this.tablicaPodaciRed.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablicaPodaciRed.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tablicaPodaciRed.ColumnCount = 3;
            this.tablicaPodaciRed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablicaPodaciRed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablicaPodaciRed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablicaPodaciRed.Location = new System.Drawing.Point(9, 86);
            this.tablicaPodaciRed.Name = "tablicaPodaciRed";
            this.tablicaPodaciRed.RowCount = 1;
            this.tablicaPodaciRed.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablicaPodaciRed.Size = new System.Drawing.Size(8, 4);
            this.tablicaPodaciRed.TabIndex = 4;
            this.tablicaPodaciRed.Visible = false;
            // 
            // bttnDelete
            // 
            this.bttnDelete.Location = new System.Drawing.Point(167, 14);
            this.bttnDelete.Name = "bttnDelete";
            this.bttnDelete.Size = new System.Drawing.Size(75, 23);
            this.bttnDelete.TabIndex = 3;
            this.bttnDelete.Text = "Obriši red";
            this.bttnDelete.UseVisualStyleBackColor = true;
            this.bttnDelete.Click += new System.EventHandler(this.bttnDelete_Click);
            // 
            // bttnOdustani
            // 
            this.bttnOdustani.Enabled = false;
            this.bttnOdustani.Location = new System.Drawing.Point(91, 44);
            this.bttnOdustani.Name = "bttnOdustani";
            this.bttnOdustani.Size = new System.Drawing.Size(75, 23);
            this.bttnOdustani.TabIndex = 2;
            this.bttnOdustani.Text = "Odustani";
            this.bttnOdustani.UseVisualStyleBackColor = true;
            this.bttnOdustani.Click += new System.EventHandler(this.bttnOdustani_Click);
            // 
            // bttnZapisiRed
            // 
            this.bttnZapisiRed.Enabled = false;
            this.bttnZapisiRed.Location = new System.Drawing.Point(9, 45);
            this.bttnZapisiRed.Name = "bttnZapisiRed";
            this.bttnZapisiRed.Size = new System.Drawing.Size(75, 23);
            this.bttnZapisiRed.TabIndex = 1;
            this.bttnZapisiRed.Text = "Zapiši Red";
            this.bttnZapisiRed.UseVisualStyleBackColor = true;
            this.bttnZapisiRed.Click += new System.EventHandler(this.bttnZapisiRed_Click);
            // 
            // bttnNew
            // 
            this.bttnNew.Location = new System.Drawing.Point(9, 15);
            this.bttnNew.Name = "bttnNew";
            this.bttnNew.Size = new System.Drawing.Size(75, 23);
            this.bttnNew.TabIndex = 0;
            this.bttnNew.Text = "Novi Red";
            this.bttnNew.UseVisualStyleBackColor = true;
            this.bttnNew.Click += new System.EventHandler(this.bttnNew_Click);
            // 
            // comboBoxNaslovi
            // 
            this.comboBoxNaslovi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNaslovi.FormattingEnabled = true;
            this.comboBoxNaslovi.Location = new System.Drawing.Point(190, 9);
            this.comboBoxNaslovi.Name = "comboBoxNaslovi";
            this.comboBoxNaslovi.Size = new System.Drawing.Size(121, 21);
            this.comboBoxNaslovi.TabIndex = 3;
            // 
            // Student
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitCPodaci);
            this.Controls.Add(this.panelGornji);
            this.Name = "Student";
            this.Text = "Student";
            this.panelGornji.ResumeLayout(false);
            this.panelGornji.PerformLayout();
            this.panelNavigacija.ResumeLayout(false);
            this.panelNavigacija.PerformLayout();
            this.splitCPodaci.Panel1.ResumeLayout(false);
            this.splitCPodaci.Panel2.ResumeLayout(false);
            this.splitCPodaci.Panel2.PerformLayout();
            this.splitCPodaci.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPodaci)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGornji;
        private System.Windows.Forms.SplitContainer splitCPodaci;
        private System.Windows.Forms.ComboBox cBoxTablica;
        private System.Windows.Forms.Label labelTablica;
        private System.Windows.Forms.DataGridView dgvPodaci;
        private System.Windows.Forms.Panel panelNavigacija;
        private System.Windows.Forms.TextBox textBoxTrenutniRed;
        private System.Windows.Forms.Button bttnNextRecord;
        private System.Windows.Forms.Button bttnLastRecord;
        private System.Windows.Forms.Button btnPreviousRecord;
        private System.Windows.Forms.Button bttnFirstRecord;
        private System.Windows.Forms.Button bttnDelete;
        private System.Windows.Forms.Button bttnOdustani;
        private System.Windows.Forms.Button bttnZapisiRed;
        private System.Windows.Forms.Button bttnNew;
        private System.Windows.Forms.TableLayoutPanel tablicaPodaciRed;
        private System.Windows.Forms.ComboBox comboBoxNaslovi;
    }
}

