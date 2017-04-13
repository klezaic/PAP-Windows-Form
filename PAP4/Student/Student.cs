using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data;
using System.Data.OleDb;

namespace Student
{
    public partial class Student : Form
    {
        private string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=TVZ_Data2003.mdb";

        private string imePrimarnogKljuca;
        private int indeksPrimarnogKljuca;
        private bool automatskoGeneriranjePrimarnogKljuca;


        public Student()
        {

            InitializeComponent();

            OleDbConnection konekcija = new OleDbConnection(this.connectionString);
            

            try
            {
                DataTable tablice = null;
                konekcija.Open();
                tablice = konekcija.GetSchema("Tables", new string[4] { null, null, null, "Table" });
                for (int i = 0; i < tablice.Rows.Count; i++)
                    if (!tablice.Rows[i][2].ToString().StartsWith("~"))
                        this.cBoxTablica.Items.Add(tablice.Rows[i][2].ToString());

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("konstruktor");
                this.Enabled = false;
            }
            finally
            {
                konekcija.Close();
            }

            
        }
        //Izmjena tablice u ComboBoxu
        private void cBoxTablica_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection konekcija = new OleDbConnection(this.connectionString);

            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM[" + this.cBoxTablica.SelectedItem + "]", konekcija);
                DataSet ds = new DataSet();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds);
                this.dgvPodaci.DataSource = ds.Tables[0];

                this.tablicaPodaciRed.Visible = false;
                this.tablicaPodaciRed.Controls.Clear();
                this.tablicaPodaciRed.RowCount = ds.Tables[0].Columns.Count;
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    Label imeStupca = new Label();
                    imeStupca.Name = "label_" + i;
                    imeStupca.Tag = i;
                    imeStupca.Text = ds.Tables[0].Columns[i].ColumnName;
                    imeStupca.AutoSize = true;
                    this.tablicaPodaciRed.Controls.Add(imeStupca, 0, i);

                    TextBox vrijednostCelije = new TextBox();
                    vrijednostCelije.Name = "textBox_" + i;
                    vrijednostCelije.Tag = i;
                    this.tablicaPodaciRed.Controls.Add(vrijednostCelije, 1, i);

                    Button tipkaUpdate = new Button();
                    tipkaUpdate.Name = "button_" + i;
                    tipkaUpdate.Tag = i;
                    tipkaUpdate.Text = "Promijeni";
                    tipkaUpdate.Click += new EventHandler(bttnPromjeni_click);
                    this.tablicaPodaciRed.Controls.Add(tipkaUpdate, 2, i);
                    
                }
                this.tablicaPodaciRed.Visible = true;

                this.imePrimarnogKljuca = ds.Tables[0].PrimaryKey[0].ColumnName;
                this.indeksPrimarnogKljuca = ds.Tables[0].PrimaryKey[0].Ordinal;
                this.automatskoGeneriranjePrimarnogKljuca = ds.Tables[0].PrimaryKey[0].AutoIncrement;

                if (this.automatskoGeneriranjePrimarnogKljuca == true)
                {
                    ((TextBox)this.tablicaPodaciRed.Controls["textBox_" + this.indeksPrimarnogKljuca]).Enabled = false;
                    ((Button)this.tablicaPodaciRed.Controls["button_" + this.indeksPrimarnogKljuca]).Enabled = false;
                }
                for (int i = 0; i < dgvPodaci.Columns.Count; i++)
                {
                    if (i == indeksPrimarnogKljuca && dgvPodaci.Columns[indeksPrimarnogKljuca].ValueType == Type.GetType("System.Int32"))
                    {
                        ((TextBox)this.Controls.Find("textBox_" + i.ToString(), true)[0]).Enabled = false;
                        ((Button)this.Controls.Find("button_" + i.ToString(), true)[0]).Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("cBoxTablica_SelectedIndexChanged");
            }
            finally
            {
                popunjavanjeTablicePodacima();
                konekcija.Close();

                popuniDrugiComboBox();
            }
        }

        private void popuniDrugiComboBox()
        {
            this.comboBoxNaslovi.Items.Clear();

            for (int i = 0; i < dgvPodaci.ColumnCount; i++)
                this.comboBoxNaslovi.Items.Add(dgvPodaci.Columns[i].HeaderText.ToString());
            
            this.comboBoxNaslovi.SelectedIndex = 0;
        }
        
        //Izmjena oznacenog reda u tablici
        private void promjenaReda(object sender, EventArgs e)
        {
            if (this.dgvPodaci.RowCount > 0)
            {
                int brReda, maxBrRedova = this.dgvPodaci.RowCount - 1;

                switch (((Button)sender).Text)
                {
                    case "<<":
                        this.dgvPodaci.CurrentCell = this.dgvPodaci.Rows[0].Cells[0];
                        break;
                    case "<":
                        brReda = this.dgvPodaci.CurrentCell.RowIndex;
                        if (brReda > 0)
                            this.dgvPodaci.CurrentCell = this.dgvPodaci.Rows[brReda - 1].Cells[0];
                        break;
                    case ">":
                        brReda = this.dgvPodaci.CurrentCell.RowIndex;
                        if (brReda < maxBrRedova)
                            this.dgvPodaci.CurrentCell = this.dgvPodaci.Rows[brReda + 1].Cells[0];
                        break;
                    case ">>":
                        this.dgvPodaci.CurrentCell = this.dgvPodaci.Rows[maxBrRedova].Cells[0];
                        break;
                }
            }
            popunjavanjeTablicePodacima();
        }

        //Oznaka reda u tablici
        private void dgvPodaci_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int brReda = this.dgvPodaci.CurrentCell.RowIndex + 1;
            this.textBoxTrenutniRed.Text = brReda.ToString();

            popunjavanjeTablicePodacima();
        }

        //Popunjavanje TextBox-ova s podatcima iz tablice
        private void popunjavanjeTablicePodacima()
        {
            try
            {
                for (int i = 0; i < this.tablicaPodaciRed.RowCount; i++)
                {
                    ((TextBox)this.tablicaPodaciRed.Controls["textBox_" + i]).Text = this.dgvPodaci.SelectedCells[i].Value.ToString();
                }

            }
            catch
            {
            }
        }

        //Brisanje oznacenog reda
        private void bttnDelete_Click(object sender, EventArgs e)
        {
            OleDbConnection konekcija = new OleDbConnection(this.connectionString);

            try
            {
                OleDbCommand naredbaBrisanja;

                if (this.dgvPodaci.SelectedCells[this.indeksPrimarnogKljuca].ValueType == Type.GetType("System.String"))
                {
                    naredbaBrisanja = new OleDbCommand("DELETE FROM [" + this.cBoxTablica.SelectedItem
                        + "] WHERE " + this.imePrimarnogKljuca + "='"
                        + this.dgvPodaci.SelectedCells[this.indeksPrimarnogKljuca].Value.ToString() + "'", konekcija);
                }
                else
                {
                    naredbaBrisanja = new OleDbCommand("DELETE FROM [" + this.cBoxTablica.SelectedItem
                        + "] WHERE " + this.imePrimarnogKljuca + "="
                        + this.dgvPodaci.SelectedCells[this.indeksPrimarnogKljuca].Value.ToString(), konekcija);
                }

                konekcija.Open();

                int rezultatBrisanja = naredbaBrisanja.ExecuteNonQuery();

                if (rezultatBrisanja > 0)
                {
                    this.dgvPodaci.Rows.Remove(this.dgvPodaci.SelectedRows[0]);
                    this.popunjavanjeTablicePodacima();
                }
                else
                {
                    MessageBox.Show("Pojavila se greška prilikom brisanja. \nRed nije obrisan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("bttnDelete_Click");
            }
            finally
            {
                konekcija.Close();
            }
        }

        //Prelazak u mod za dodavanje novog reda
        private void bttnNew_Click(object sender, EventArgs e)
        {

            this.textBoxTrenutniRed.Text = (dgvPodaci.RowCount+1).ToString();

            for (int i = 0; i < dgvPodaci.Columns.Count; i++)
            {
                if (i == indeksPrimarnogKljuca && dgvPodaci.Columns[indeksPrimarnogKljuca].ValueType == Type.GetType("System.Int32"))
                {
                    int max = Convert.ToInt32(dgvPodaci[indeksPrimarnogKljuca, dgvPodaci.RowCount-1].Value.ToString());
                    max++;
                    ((TextBox)this.Controls.Find("textBox_" + i.ToString(), true)[0]).Text = max.ToString();
                }
                else
                    ((TextBox)this.Controls.Find("textBox_" + i.ToString(), true)[0]).Text = null;
            }
           

            this.bttnZapisiRed.Enabled = true;
            this.bttnOdustani.Enabled = true;

            for (int i = 0; i < dgvPodaci.Columns.Count; i++)
            {
                if(i==indeksPrimarnogKljuca && dgvPodaci.Columns[indeksPrimarnogKljuca].ValueType == Type.GetType("System.Int32"))
                    ((TextBox)this.Controls.Find("textBox_" + i.ToString(), true)[0]).Enabled = false;
                ((Button)this.Controls.Find("button_" + i.ToString(), true)[0]).Enabled = false;
            }
            

        }   

        //Izlazak it moda, te unos reda u bazu
        private void bttnZapisiRed_Click(object sender, EventArgs e)
        {
            OleDbConnection konekcija = new OleDbConnection(this.connectionString);

            string statement = "INSERT INTO " + this.cBoxTablica.SelectedItem;

            statement += " VALUES ";
            statement += "(";

            for (int i = 0; i < dgvPodaci.Columns.Count; i++)
            {
                string txt = ((TextBox)this.Controls.Find("textBox_" + i.ToString(), true)[0]).Text;

                if (i != 0)
                    statement += ", ";

                if (this.dgvPodaci.Columns[i].ValueType == Type.GetType("System.String"))
                    statement += "'" + txt + "'";
                else if (this.dgvPodaci.Columns[i].ValueType == Type.GetType("System.DateTime"))
                    statement += "'" + txt + "'";                    
                else
                    statement += txt;
            }

            statement += ");";

            try
            {
                OleDbCommand naredbaUnosaReda;

                naredbaUnosaReda = new OleDbCommand(statement, konekcija);

                konekcija.Open();
                naredbaUnosaReda.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(statement);
                MessageBox.Show(imePrimarnogKljuca + "     " + indeksPrimarnogKljuca);
            }
            finally
            {
                konekcija.Close();

                refresh();

                this.dgvPodaci.CurrentCell = this.dgvPodaci.Rows[this.dgvPodaci.RowCount - 1].Cells[0];
                this.textBoxTrenutniRed.Text = this.dgvPodaci.RowCount.ToString();
                popunjavanjeTablicePodacima();
            }

            this.bttnOdustani.Enabled = false;
            this.bttnZapisiRed.Enabled = false;

            for (int i = 0; i < dgvPodaci.Columns.Count; i++)
                ((Button)this.Controls.Find("button_" + i.ToString(), true)[0]).Enabled = true;

        }

        //Izlazak iz moda, bez unosa reda u bazu
        private void bttnOdustani_Click(object sender, EventArgs e)
        {
            this.bttnZapisiRed.Enabled = false;
            this.bttnOdustani.Enabled = false;

            this.textBoxTrenutniRed.Text = 1.ToString();

            refresh();
            popunjavanjeTablicePodacima();

            for (int i = 0; i < dgvPodaci.Columns.Count; i++)
                ((Button)this.Controls.Find("button_" + i.ToString(), true)[0]).Enabled = true;

        }

        //Mjenjanje odredene vrijednosti u bazi
        private void bttnPromjeni_click(object sender, EventArgs e)
        {
            string bttnName = ((Button)sender).Name;
            string[] tkn = bttnName.Split('_');
            int bttnIdx = Convert.ToInt32(tkn[1]);

            string text = ((TextBox)this.Controls.Find("textBox_" + tkn[1], true)[0]).Text;

            OleDbConnection konekcija = new OleDbConnection(this.connectionString);

            string statement = "UPDATE " + this.cBoxTablica.SelectedItem;

            statement += " SET ";
            statement += dgvPodaci.Columns[bttnIdx].Name.ToString();
            statement += "=";
            if (this.dgvPodaci.Columns[bttnIdx].ValueType == Type.GetType("System.String") || this.dgvPodaci.Columns[bttnIdx].ValueType == Type.GetType("System.DateTime"))
                statement += "'" + text + "'";
            else
                statement += text;
            statement += " WHERE ";

            statement += imePrimarnogKljuca;
            statement += "=";
            statement += ((TextBox)this.Controls.Find("textBox_" + indeksPrimarnogKljuca, true)[0]).Text;

            try
            {
                OleDbCommand naredbaUnosaReda;

                naredbaUnosaReda = new OleDbCommand(statement, konekcija);

                konekcija.Open();
                naredbaUnosaReda.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(statement);
                MessageBox.Show(dgvPodaci.Columns[1].ValueType.ToString());
            }
            finally
            {
                konekcija.Close();

                refresh();

                this.dgvPodaci.CurrentCell = this.dgvPodaci.Rows[this.dgvPodaci.RowCount - 1].Cells[0];
                this.textBoxTrenutniRed.Text = this.dgvPodaci.RowCount.ToString();
                popunjavanjeTablicePodacima();
            }


        }

        //refresh metoda za ucitavanje tablice nakon promjene
        private void refresh()
        {
            this.cBoxTablica.Enabled = false;
            int curIdx = this.cBoxTablica.SelectedIndex;
            if (curIdx < this.cBoxTablica.Items.Count - 1)
            {
                this.cBoxTablica.SelectedIndex++;
                this.cBoxTablica.SelectedIndex--;
            }
            else
            {
                this.cBoxTablica.SelectedIndex--;
                this.cBoxTablica.SelectedIndex++;
            }

            this.cBoxTablica.Enabled = true;
        }
    }
}
