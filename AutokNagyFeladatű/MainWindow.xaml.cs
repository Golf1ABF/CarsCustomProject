using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutokNagyFeladatű
{
    public partial class MainWindow : Window
    {
        List<Autok> lista = new List<Autok>();
        public MainWindow()
        {
            InitializeComponent();

            loadInSourceFile();

            chbxOwner.Items.Add("cég");
            chbxOwner.Items.Add("magánszemély");
        }

        private void loadInSourceFile() 
        {
            var sr = new StreamReader("../../../src/src.txt", encoding: Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                lista.Add(new Autok(sr.ReadLine()));
            }

            foreach (var item in lista)
            {
                cmbxYear.Items.Add(item.GyartasiEv);
            }
        }

        private void searchByLicensePlate_TextChanged(object sender, TextChangedEventArgs e)
        {
            var byLicensePlate = lista.Where(x => x.Rendszam == searchByLicensePlate.Text).ToList();

            foreach (var item in byLicensePlate)
            {
                searchByLicensePlatelbl.Content = item;
            }
        }

        private void allCarsBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in lista)
            {
                lbxCars.Items.Add(item);
            }
        }

        private void cmbxYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbxCars.Items.Clear();
            var convertedYear = Convert.ToInt32(cmbxYear.SelectedItem);
            var selectedYear = lista.Where(x => x.GyartasiEv == convertedYear).ToList();
            foreach (var item in selectedYear)
            {
                lbxCars.Items.Add(item);
            }
        }

        private void newCarBtn_Click(object sender, RoutedEventArgs e)
        {
            var sw = new StreamWriter("../../../src/src.txt", false);

            sw.WriteLine($"{lista.Max(x => x.ID) + 1}{tbxBrand.Text};{tbxType.Text};{tbxColor.Text};{tbxFuelConsumption.Text};{tbxPrice.Text};{chbxOwner.SelectedItem};{tbxLicensePlate.Text}");
            loadInSourceFile();
        }
    }
}