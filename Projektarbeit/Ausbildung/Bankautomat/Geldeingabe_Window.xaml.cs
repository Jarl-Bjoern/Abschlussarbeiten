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
using System.Windows.Shapes;

namespace P01_Projekt
{
    /// <summary>
    /// Interaktionslogik für Geldeingabe_Window.xaml
    /// </summary>
    public partial class Geldeingabe_Window : Window
    {
        // Variablen_Definieren
        int Konverter,
            Anzahl_Abhebungen = Geldtransfer.Anzahl_Abhebungen;

        // Public_Variablen_Definieren
        static public int Abhebung = Geldtransfer.Abhebung;

        // Fenster_Aufruf_Definieren
        Geldtransfer Geldtransfer;
        MainWindow mainWindow = new MainWindow();

        public Geldeingabe_Window(Geldtransfer _Geldtransfer)
        {
            InitializeComponent();

            // Fenster_Implementierung
            Geldtransfer = _Geldtransfer;
        }

        private void btn_Abbruch_Click(object sender, RoutedEventArgs e)
        {
            PIN_Window.PIN_Abfrage_Bereits_Erfolgreich = false;
            Geldtransfer.Abbruch_Erfolgreich = true;
            this.Close();
        }

        private void btn_Korrektur_Click(object sender, RoutedEventArgs e)
        {
            tb_Abhebung.Text = "";
            MessageBox.Show("Ihre Eingabe wurde erfolgreich zurückgesetzt!");
        }

        private void btn_Hilfe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Geben Sie bitte Ihre 4-Stellige Geheimzahl (PIN) ein.\nWenden Sie sich bei technischen Störungen, bitte an einen Administrator!");
        }

        private void btn_Bestaetigung_Click(object sender, RoutedEventArgs e)
        {
            int Abhebungslimit = 2000, Minimalabhebung = 20;
            double Ueberziehungslimit = Geldtransfer.Ueberziehungslimit;

            if (Convert.ToDouble(tb_Abhebung.Text) <= Abhebungslimit && Convert.ToDouble(tb_Abhebung.Text) >= Minimalabhebung)
            {
                if (Geldtransfer.Kontostand >= Ueberziehungslimit)
                {
                    Kontostand_Berechnen();
                }
                else
                {
                    Geldtransfer.Ueberziehungs_Warnung();
                }
            }
            else
            {
                if (Convert.ToInt32(tb_Abhebung.Text) > 2000)
                {
                    MessageBox.Show("Sie können nur 2.000,00 € maximal abheben!");
                    tb_Abhebung.Text = "";
                }
                else if (Convert.ToInt32(tb_Abhebung.Text) < 20)
                {
                    MessageBox.Show("Der Mindestwert für eine Abhebung liegt bei 20,00 €!");
                    tb_Abhebung.Text = "";
                }
            }
        }

        private void Kontostand_Berechnen()
        {
            Geldtransfer.Kontostand_Alt();
            Geldtransfer.Kontostand = Geldtransfer.Kontostand - Convert.ToDouble(tb_Abhebung.Text);
            Geldtransfer.Neuer_Kontostand = Geldtransfer.Kontostand;
            Geldtransfer.Kontostand_Neu();
            Abhebung = Convert.ToInt32(tb_Abhebung.Text);
            Anzahl_Abhebungen = Anzahl_Abhebungen + 1;
            MessageBox.Show("Sie haben erfolgreich " + tb_Abhebung.Text + ",00 € abgehoben!");
            tb_Abhebung.Text = "";
            Geldtransfer.tb_Geldbetrag_Eingabe.Text = "";
        }

        private void Tasten_Auswahl(int Zahl)
        {
            tb_Abhebung.Text = tb_Abhebung.Text + Zahl;
            Geldtransfer.tb_Geldbetrag_Eingabe.Text = Geldtransfer.tb_Geldbetrag_Eingabe.Text + Zahl;
        }

        private void btn_Eins_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(1);
        }

        private void btn_Zwei_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(2);
        }

        private void btn_Drei_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(3);
        }

        private void btn_Vier_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(4);
        }

        private void btn_Fuenf_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(5);
        }

        private void btn_Sechs_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(6);
        }

        private void btn_Sieben_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(7);
        }

        private void btn_Acht_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(8);
        }

        private void btn_Neun_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(9);
        }

        private void btn_Minus_Click(object sender, RoutedEventArgs e)
        {
            int Null_Hilfsvariable = 0;
            Konverter = Convert.ToInt32(tb_Abhebung.Text) / 10;
            tb_Abhebung.Text = Convert.ToString(Konverter);
            if (tb_Abhebung.Text == Convert.ToString(Null_Hilfsvariable))
            {
                tb_Abhebung.Text = "";
            }
            if (Geldtransfer.tb_Geldbetrag_Eingabe.Text == Convert.ToString(Null_Hilfsvariable))
            {
                Geldtransfer.tb_Geldbetrag_Eingabe.Text = "";
            }
        }

        private void btn_Null_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(0);
        }

        private void btn_Plus_Click(object sender, RoutedEventArgs e)
        {
            Konverter = Convert.ToInt32(tb_Abhebung.Text) * 10;
            tb_Abhebung.Text = Convert.ToString(Konverter);
            Geldtransfer.tb_Geldbetrag_Eingabe.Text = Convert.ToString(Konverter);
        }
    }
}
