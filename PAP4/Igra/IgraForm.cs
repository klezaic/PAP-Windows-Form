using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Igra
{
    public partial class IgraForm : Form
    {
        int trenutniScore;
        int idealanScore;
        int tezina;

        int visina = 25; //jedinicna visina diska u px
        int sirina = 25; //jedinicna sirina diska u px

        Rectangle[] prvi;   //polje diskova prvog stupca
        Rectangle[] drugi;  //polje diskova drugog stupca
        Rectangle[] treci;  //polje diskova treceg stupca
        Rectangle tmp = new Rectangle();

        int brPrvi;         //broj diskova na prvom stupcu
        int brDrugi;        //broj diskova na drugom stupcu
        int brTreci;        //broj diskova na trecem stupcu

        int prviX = 115;    //x-koordinata sredine prvog stupca
        int drugiX = 402;   //x-koordinata sredine drugog stupca
        int treciX = 665;   //x-koordinata sredine treceg stupca
        int y = 400;        //y-kordinata dna stupaca

        bool diskPrimljen = false;  //globalna varijabla; oznacava da li je disk trenutno "u ruci"


        //
        public IgraForm()
        {
            InitializeComponent();
            this.brojPrstenovaComboBox.SelectedIndex = 2;

            this.pictureBox.Dock = DockStyle.Fill;
            this.pictureBox.BackColor = Color.White;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
        }
        //Inicijalizacija
        private void IgraForm_Load(object sender, EventArgs e)
        {            
            tezina = Convert.ToInt32(this.brojPrstenovaComboBox.SelectedItem.ToString());

            trenutniScore = 0;
            idealanScore =  (int)Math.Pow(2, (double)tezina) - 1;

            this.CurrentLabel.Text = "Trenutni rezultat: " + trenutniScore;
            this.IdealLabel.Text = "Idealni rezultat: " + idealanScore;

            generirajDiskove();

            diskPrimljen = false;

            this.pictureBox.Refresh();

        }

        //Restart igre
        private void newGameButton_Click(object sender, EventArgs e)
        {
            IgraForm_Load(sender, e);
        }

        //generiranje diskova za odabranu tezinu
        private void generirajDiskove()
        {
            prvi = new Rectangle[tezina];
            drugi = new Rectangle[tezina];
            treci = new Rectangle[tezina];

            brPrvi = tezina;
            brDrugi = 0;
            brTreci = 0;

            for (int n = 0; n < tezina; n++)
            {
                Rectangle rect = new Rectangle(prviX - ((tezina - n) * sirina) / 2, y - n * visina, (tezina - n) * sirina, visina);
                prvi[n] = rect;
            }
        }

        //Paint metoda, iscrtava sve prstenove
        //ujedno služi kao "Update funkcija", pošto se promjene(bodova) mogu dogoditi samo uspiješnim pomicanjem prstena
        private void pictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for(int i = 0; i< brPrvi; i++)
            {
                g.DrawRectangle(System.Drawing.Pens.Black, prvi[i]);
                e.Graphics.FillRectangle(System.Drawing.Brushes.LightGray, prvi[i]);
            }
            for (int i = 0; i < brDrugi; i++)
            {
                g.DrawRectangle(System.Drawing.Pens.Black, drugi[i]);
                e.Graphics.FillRectangle(System.Drawing.Brushes.LightGray, drugi[i]);
            }
            for (int i = 0; i < brTreci; i++)
            {
                g.DrawRectangle(System.Drawing.Pens.Black, treci[i]);
                e.Graphics.FillRectangle(System.Drawing.Brushes.LightGray, treci[i]);
            }

            g.DrawRectangle(System.Drawing.Pens.Gray, new Rectangle(prviX - sirina/2, y-(tezina+3)*visina, sirina, visina*(tezina-brPrvi+4)));
            g.DrawRectangle(System.Drawing.Pens.Gray, new Rectangle(drugiX - sirina / 2, y - (tezina + 3) * visina, sirina, visina * (tezina - brDrugi + 4)));
            g.DrawRectangle(System.Drawing.Pens.Gray, new Rectangle(treciX - sirina / 2, y - (tezina + 3) * visina, sirina, visina * (tezina - brTreci + 4)));

            g.DrawRectangle(System.Drawing.Pens.DarkGray, new Rectangle(prviX - ((tezina + 1)* sirina - 8) / 2,    y + visina,      ((tezina + 1) * sirina)-8,    visina));
            g.DrawRectangle(System.Drawing.Pens.DarkGray, new Rectangle(drugiX - ((tezina + 1) * sirina-8) / 2,    y + visina,      ((tezina + 1) * sirina)-8,    visina));
            g.DrawRectangle(System.Drawing.Pens.DarkGray, new Rectangle(treciX - ((tezina + 1) * sirina-8) / 2,    y + visina,      ((tezina + 1) * sirina)-8,    visina));

            if (!diskPrimljen)
            {
                this.trenutniScore++;
                this.CurrentLabel.Text = "Trenutni rezultat: " + (trenutniScore - 1);
                if (brTreci == tezina)
                {
                    DialogResult dr = MessageBox.Show("Do you want to restart?",
                      "Winner!!!", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                        Application.Restart();
                    else
                        this.Close();
                }
            }
        }

        //Click metoda pictureBox-a
        //Logika igre, te poziv "Update funkcije" nakon svakog(legalnog) poteza:
        // - pronalazak lokacije klika
        // - pronalazak odabranog prstena
        // - provjera je li potez legalan
        // - postavljanje odabranog prstena
        private void pictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs m = (MouseEventArgs)e;
            int curx = m.X;
            int cury = m.Y;

            int indexDiska = (y + visina - cury)/visina;
            int stupac = 0; ;

            //provjera ispravnosti X i Y koordinata klika
            if (indexDiska >= 0 && indexDiska < tezina && !diskPrimljen)
            {
                if (curx > (prviX - prvi[indexDiska].Width / 2) && curx < (prviX + prvi[indexDiska].Width / 2))
                    stupac = 1;
                else if (curx > (drugiX - drugi[indexDiska].Width / 2) && curx < (drugiX + drugi[indexDiska].Width / 2))
                    stupac = 2;
                else if (curx > (treciX - treci[indexDiska].Width / 2) && curx < (treciX + treci[indexDiska].Width / 2))
                    stupac = 3;
            }
            else if (diskPrimljen)
            {
                if (curx > (prviX - tezina * sirina / 2) && curx < (prviX + tezina * sirina / 2))
                    stupac = 1;
                else if (curx > (drugiX - tezina * sirina / 2) && curx < (drugiX + tezina * sirina / 2))
                    stupac = 2;
                else if (curx > (treciX - tezina * sirina / 2) && curx < (treciX + tezina * sirina / 2))
                    stupac = 3;
            }
            
            if (stupac >= 1 && stupac <= 3)
            {
                if (!diskPrimljen)//brisemo oznaceni disk i spremamo ga u memoriju
                {
                    if (stupac == 1 && indexDiska == brPrvi - 1)
                    {
                        tmp = prvi[indexDiska];
                        brPrvi--;
                        diskPrimljen = true;
                        this.pictureBox.Refresh();
                    }
                    else if (stupac == 2 && indexDiska == brDrugi - 1)
                    {
                        tmp = drugi[indexDiska];
                        brDrugi--;
                        diskPrimljen = true;
                        this.pictureBox.Refresh();
                    }
                    else if (stupac == 3 && indexDiska == brTreci - 1)
                    {
                        tmp = treci[indexDiska];
                        brTreci--;
                        diskPrimljen = true;
                        this.pictureBox.Refresh();
                    }
                }
                else if(diskPrimljen)//postavljamo spremljeni disk na oznacenu lokaciju
                {
                    if (stupac == 1)
                    {
                        tmp.X = prviX - tmp.Width/2;
                        if (brPrvi > 0)
                        {
                            tmp.Y = prvi[brPrvi - 1].Y - visina;
                            if (tmp.Width > prvi[brPrvi - 1].Width)
                            {
                                MessageBox.Show("Pravilo: Uvijek mora ići manji disk na veci");
                                return;
                            }

                        }
                        else
                            tmp.Y = y;
                        prvi[brPrvi] = tmp;
                        brPrvi++;
                        diskPrimljen = false;
                        this.pictureBox.Refresh();
                    }
                    else if (stupac == 2)
                    {
                        tmp.X = drugiX - tmp.Width / 2;
                        if (brDrugi > 0)
                        {
                            tmp.Y = drugi[brDrugi - 1].Y - visina;
                            if (tmp.Width > drugi[brDrugi - 1].Width)
                            {
                                MessageBox.Show("Pravilo: Uvijek mora ići manji disk na veci");
                                return;
                            }
                        }
                        else
                            tmp.Y = y;
                        drugi[brDrugi] = tmp;
                        brDrugi++;
                        diskPrimljen = false;
                        this.pictureBox.Refresh();
                    }
                    else if (stupac == 3)
                    {
                        tmp.X = treciX - tmp.Width / 2;
                        if (brTreci > 0)
                        {
                            tmp.Y = treci[brTreci - 1].Y - visina;
                            if (tmp.Width > treci[brTreci - 1].Width)
                            {
                                MessageBox.Show("Pravilo: Uvijek mora ići manji disk na veci");
                                return;
                            }
                        }
                        else
                            tmp.Y = y;
                        treci[brTreci] = tmp;
                        brTreci++;
                        diskPrimljen = false;
                        this.pictureBox.Refresh();
                    }
                }
            }
        }
    }
}
