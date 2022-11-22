using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P01_Projekt
{
    /// <summary>
    /// Interaktionslogik für Geldtransfer.xaml
    /// </summary>
    public partial class Geldtransfer : Page
    {
        // Variablen_Definieren
        static public double Kontostand = 5076.45, Neuer_Kontostand, Ueberziehungslimit = -80.00;
        static public int Abhebung, Anzahl_Abhebungen;
        static public bool Abbruch_Erfolgreich = false;

        // Fenster_Aufruf_Definieren
        MainWindow mainWindow;

        //// List_Definieren
        //List<int> Betraege = new List<int>();

        //// Array_Definieren
        //const int Maximale_Kontobelastungen = 50;
        //int[] Tabelle = new int[Maximale_Kontobelastungen + 1]; 

        public Geldtransfer(MainWindow _mainWindow)
        {
            InitializeComponent();

            // Fenster_Implementierung
            mainWindow = _mainWindow;

            // Methoden_Aufruf
            Kontostand_Alt();
            Kontostand_Neu();
        }

        private void Geld_Abheben(int Betrag)
        {

            if (Abbruch_Erfolgreich == true)
            {
                Fehlermeldung();
            }
            else
            {
                if (Kontostand >= Ueberziehungslimit)
                {
                    Kontostand_Alt();
                    Kontostand = Kontostand - Betrag;
                    tb_Geldbetrag_Eingabe.Text = Betrag.ToString() + " €";
                    Neuer_Kontostand = Kontostand;
                    Kontostand_Neu();
                    Abhebung = -Betrag;
                    Anzahl_Abhebungen = Anzahl_Abhebungen + 1;
                    MessageBox.Show("Sie haben erfolgreich " + Betrag.ToString() + " € abgehoben!");
                    //Abhebungsbetraege_Speichern();
                }
                else
                {
                    Ueberziehungs_Warnung();
                }
            }
        }

        private void btn_Zwanzig_Click(object sender, RoutedEventArgs e)
        {
            Geld_Abheben(20);
        }
        private void btn_Fuenfzig_Click(object sender, RoutedEventArgs e)
        {
            Geld_Abheben(50);
        }

        private void btn_Einhundert_Click(object sender, RoutedEventArgs e)
        {
            Geld_Abheben(100);
        }

        private void btn_Fuenfhundert_Click(object sender, RoutedEventArgs e)
        {
            Geld_Abheben(500);
        }

        //private void Abhebungsbetraege_Speichern()
        //{
        //    int Betrag = Abhebung, Ergebnis;

        //    for (int i = 1; i <= Maximale_Kontobelastungen; i++)
        //    {
        //        Tabelle[i] = Betrag;
        //        Ergebnis = Tabelle[i];
        //    }
        //    //Betraege.Add(Abhebung);
        //    //for (int i = 1; i <= Betraege.Count; i++)
        //    //{
        //    //    MessageBox.Show(Betraege[i].ToString());
        //    //}
        //}

        public void Kontostand_Alt()
        {
            tb_alter_Kontostand.Text = Kontostand.ToString() + " €";
        }

        public void Kontostand_Neu()
        {
            tb_neuer_Kontostand.Text = Neuer_Kontostand.ToString() + " €";
        }

        public void Ueberziehungs_Warnung()
        {
            MessageBox.Show("Das maximale Überziehungslimit liegt bei -100,00 €!");
        }

        public void Fehlermeldung()
        {
            MessageBox.Show("Ihre Sitzung ist abgelaufen!\nBitte wählen Sie im Menü eine Option aus!");
        }
    }
}
