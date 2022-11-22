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
using System.Threading;

namespace P01_Projekt
{
    /// <summary>
    /// Interaktionslogik für PIN_Window.xaml
    /// </summary>
    public partial class PIN_Window : Window
    {
        // Variablen_Definieren
        int PIN = 9999,
            Konverter,
            Fehleingabe = 0;

        bool Karte_Einbehalten = false,
             Bestaetigen = false;

        static public bool PIN_Abfrage_Bereits_Erfolgreich = false;

        // Fenster_Definieren
        MainWindow mainWindow;

        public PIN_Window(MainWindow _mainWindow)
        {
            InitializeComponent();

            // Fenster_Implementierung
            mainWindow = _mainWindow;
        }

        private void btn_Abbruch_Click(object sender, RoutedEventArgs e)
        {
            if (Karte_Einbehalten == true)
            {
                Karten_Einbehaltungs_Warnung();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void btn_Korrektur_Click(object sender, RoutedEventArgs e)
        {
            if (Karte_Einbehalten != true)
            {
                pwb_PIN.Password = "";
                MessageBox.Show("Ihre Eingabe wurde erfolgreich zurückgesetzt!");
            }
            else
            {
                Karte_Einbehalten = true;
            }
        }

        private void btn_Hilfe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Geben Sie bitte Ihre 4-Stellige Geheimzahl (PIN) ein.\nWenden Sie sich bei technischen Störungen, bitte an einen Administrator!");
        }

        public void btn_Bestaetigung_Click(object sender, RoutedEventArgs e)
        {
            string Fehlermeldung = "";
            Bestaetigen = true;

            if (pwb_PIN.Password == Convert.ToString(PIN) && Bestaetigen == true)
            {
                PIN_Ueberpruefen_Erfolgreich();
            }
            else if (Convert.ToInt32(pwb_PIN.Password) > 9999)
            {
                MessageBox.Show("Ihre PIN ist zu lang, sie darf maximal nur aus 4 Stellen bestehen!");
            }
            else if  (Convert.ToInt32(pwb_PIN.Password) < 1000)
            {
                MessageBox.Show("Ihre PIN ist zu kurz, sie muss mindestens aus 4 Stellen bestehen!");
            }
            else if (Karte_Einbehalten == true)
            {
                Karten_Abfrage();
            }
            else
            {
                pwb_PIN.Password = "";
                Bestaetigen = false;

                if (Fehleingabe == 0)
                {
                    Fehlermeldung = "Ihre Geheimzahl wurde falsch eingegeben!\nBitte versuchen Sie es erneut!";
                }
                else if (Fehleingabe == 1)
                {
                    Fehlermeldung = "Ihre Geheimzahl wurde ein weiteres Mal falsch eingegeben!\nSie haben noch einen weiteren Versuch übrig!";
                }
                else if (Fehleingabe == 2)
                {
                    Fehlermeldung = "Sie haben insgesamt drei Mal Ihre Geheimzahl falsch eingegeben!\nIhre Bankkarte wird einbehalten und Sie werden nun wieder auf die Startseite zurückgeleitet!\n";
                    Karte_Einbehalten = true;
                }
                MessageBox.Show(Fehlermeldung);
                Fehleingabe = Fehleingabe + 1;
            }
        }

        private void Karten_Abfrage()
        {
            if (pwb_PIN.Password == Convert.ToString(PIN))
            {
                Karten_Einbehaltungs_Warnung();
            }
            else if (pwb_PIN.Password != Convert.ToString(PIN))
            {
                Karten_Einbehaltungs_Warnung();
            }
        }

        private void Karten_Einbehaltungs_Warnung()
        {
            MessageBox.Show("Sie hatten Ihren PIN drei Mal falsch eingegeben, wodurch Ihre Karte einbehalten wurde!\nBei einer Rückforderung der Bankkarte, wenden Sie sich bitte an einen Mitarbeiter!");
        }

        public void PIN_Ueberpruefen_Erfolgreich()
        {
            Fehleingabe = 0;
            if (Karte_Einbehalten != true)
            {
                if (mainWindow.Auswahl == false)
                {
                    MessageBox.Show("Ihre Eingabe war erfolgreich!");
                    this.Close();
                    Geldtransfer Transfer = new Geldtransfer(mainWindow);
                    mainWindow.f_Inhalt.Content = Transfer;
                    Geldeingabe_Window Abhebung = new Geldeingabe_Window(Transfer);
                    Abhebung.Show();
                    PIN_Abfrage_Bereits_Erfolgreich = true;
                }
                else
                {
                    MessageBox.Show("Ihre Eingabe war erfolgreich!");
                    this.Close();
                    Kontostand_Page Konto = new Kontostand_Page();
                    mainWindow.f_Inhalt.Content = Konto;
                    PIN_Abfrage_Bereits_Erfolgreich = true;
                }
            }
        }

        private void Tasten_Auswahl(int Zahl)
        {
            pwb_PIN.Password = pwb_PIN.Password + Zahl;
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
            Konverter = Convert.ToInt32(pwb_PIN.Password) / 10;
            pwb_PIN.Password = Convert.ToString(Konverter);
            if (pwb_PIN.Password == Convert.ToString(Null_Hilfsvariable))
            {
                pwb_PIN.Password = "";
            }
        }

        private void btn_Null_Click(object sender, RoutedEventArgs e)
        {
            Tasten_Auswahl(0);
        }

        private void btn_Plus_Click(object sender, RoutedEventArgs e)
        {
            Konverter = Convert.ToInt32(pwb_PIN.Password) * 10;
            pwb_PIN.Password = Convert.ToString(Konverter);
        }
    }
}
