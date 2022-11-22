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
    /// Interaktionslogik für Kontostand_Page.xaml
    /// </summary>
    public partial class Kontostand_Page : Page
    {
        // Variablen_Definieren
        double Kontostand = Geldtransfer.Kontostand;

        int Anzahl_Abhebungen = Geldtransfer.Anzahl_Abhebungen;

        string Datum = System.DateTime.Now.ToShortDateString(),
               Absender_Name = "Programmierumgebung | Saarbrücken | KBBZ-Halberg | HBFS-WI",
               Verwendungszweck = "T0001";

        // Stringbuilder_Definieren
        StringBuilder Kontoauszug = new StringBuilder();

        public Kontostand_Page()
        {
            InitializeComponent();

            // Standard_Kontostand
            tb_Kontostand.Text = Convert.ToString(Kontostand) + " €";

            // Methoden_Aufruf
            Konto_Aktivitaeten();
        }

        private void Konto_Aktivitaeten()
        {
            //int Betrag = 0, Hilfsvariable = 0, Hilfsvariable_Zwei = 0;

            Kontoauszug.AppendFormat("\t\t      Datum");
            Kontoauszug.AppendFormat("\t\t\t\t\t\t\t Name");
            Kontoauszug.AppendFormat("\t\t\t\t\t\t\t\t Verwendungszweck");
            Kontoauszug.AppendFormat("\t\t\t\t\t\t\t\tBetrag");
            //for (int i = 0; i != Anzahl_Abhebungen;)
            //{
            //    Hilfsvariable = Geldtransfer.Abhebung;
            //    Hilfsvariable_Zwei = Geldeingabe_Window.Abhebung;
            //    if (Anzahl_Abhebungen >= i)
            //    {
            //        if (Hilfsvariable >= Betrag)
            //        {
            //            Betrag = Betrag + Hilfsvariable;
            //        }
            //        else if (Hilfsvariable_Zwei >= Betrag)
            //        {
            //            Betrag = Betrag + Hilfsvariable_Zwei;
            //        }
            //        i = i + 1;
            //        Kontoauszug.AppendFormat("\n\t\t{0,13}\t\t\t\t{1,16}\t\t\t             {2,17}\t\t\t\t\t\t\t\t   {3,17:n} €", Datum, Absender_Name, Verwendungszweck, Betrag);
            //    }
            //    Betrag = 0;
            //}
            //lb_Kontoauszug.Items.Add(Kontoauszug.ToString());
        }
    }
}
