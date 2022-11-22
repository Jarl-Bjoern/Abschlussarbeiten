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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Variablen_Definieren
        public bool Auswahl = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Geld_Abheben_Click(object sender, RoutedEventArgs e)
        {
            Auswahl = false;
            if (PIN_Window.PIN_Abfrage_Bereits_Erfolgreich == true)
            {
                Geldtransfer Transfer = new Geldtransfer(this);
                f_Inhalt.Content = Transfer;
                Geldeingabe_Window Abhebung = new Geldeingabe_Window(Transfer);
            }
            else
            {
                PIN_Window PIN_Fenster = new PIN_Window(this);
                PIN_Fenster.Show();
            }
        }

        private void btn_Hilfe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Auf der Startseite können Sie auswählen, ob Sie Geld abheben oder Ihren aktuellen Kontostand angezeigt bekommen möchten.\nFalls Sich kein Geld mehr im Automat aktuell befinden sollte, bitten wir Sie einem Mitarbeiter, dies zu melden!\nWenden Sie sich technischen Störungen, bitte an einen Administrator!");
        }

        private void btn_Kontostand_Click(object sender, RoutedEventArgs e)
        {
            Auswahl = true;
            if (PIN_Window.PIN_Abfrage_Bereits_Erfolgreich == true)
            {
                Kontostand_Page Konto = new Kontostand_Page();
                f_Inhalt.Content = Konto;
            }
            else
            {
                PIN_Window PIN_Fenster = new PIN_Window(this);
                PIN_Fenster.Show();
            }
        }
    }
}
