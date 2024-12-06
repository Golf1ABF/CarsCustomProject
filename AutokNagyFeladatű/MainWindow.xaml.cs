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

            foreach (var item in lista)
            {
                lbxCars.Items.Add(item);
            }
        }

        private void loadInSourceFile() 
        {
            var sr = new StreamReader("../../../src/src.txt", encoding: Encoding.UTF8);

            while (!sr.EndOfStream)
            {
                lista.Add(new Autok(sr.ReadLine()));
            }

            foreach (var item in lista.OrderBy(x => x.Marka))
            {
                if (!cmbxBrand.Items.Contains(item.Marka))
                {
                    cmbxBrand.Items.Add(item.Marka);
                }
            }

            foreach (var item in lista.OrderByDescending(x => x.GyartasiEv))
            {
                if (!cmbxYear.Items.Contains(item.GyartasiEv))
                {
                    cmbxYear.Items.Add(item.GyartasiEv);
                }
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

        private void tbxHowManyKm_TextChanged(object sender, TextChangedEventArgs e)
        {
            var selectedCar = (Autok)lbxCars.SelectedItem;

            if (!int.TryParse(tbxHowManyKm.Text, out int km))
            {
                tblCarCost.Text = "Adj meg egy érvényes számot!";
                return;
            }

            double annualConsumption = km * (selectedCar.Fogyasztas / 100);
            tblCarCost.Text =
                $"Ha {km} kilométert teszel meg évente, a kiválasztott autó ({selectedCar.Marka} {selectedCar.Tipus}) " +
                $"{annualConsumption:F2} liter üzemanyagot fogyaszt el, ami {annualConsumption*650:F0} Ft évente.";
        }

        private void cmbxYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var convertedYear = Convert.ToInt32(cmbxYear.SelectedItem);

            if (cmbxBrand.SelectedItem != null)
            {
                lbxCars.Items.Clear();
                var selectedYear = lista.Where(x => x.GyartasiEv == convertedYear && x.Marka == cmbxBrand.Text).ToList();
                if (selectedYear.Count > 0)
                {
                    foreach (var item in selectedYear)
                    {
                        lbxCars.Items.Add(item);
                    }
                }
                else MessageBox.Show("Nincs ilyen autó ilyen évjáratban és márkában.");
            }
            else
            {
                lbxCars.Items.Clear();
                var selectedYear = lista.Where(x => x.GyartasiEv == convertedYear).ToList();
                foreach (var item in selectedYear)
                {
                    lbxCars.Items.Add(item);
                }
            }
        }

        private void cmbxBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var convertedYear = Convert.ToInt32(cmbxYear.SelectedItem);
            lbxCars.Items.Clear();
            if (cmbxYear.SelectedItem != null)
            {
                var selectedBrand = lista.Where(x => x.Marka == cmbxBrand.Text && x.GyartasiEv == convertedYear).ToList();
                if (selectedBrand.Count > 0)
                {
                    foreach (var item in selectedBrand)
                    {
                        lbxCars.Items.Add(item);
                    }
                }
                MessageBox.Show("Nincs ilyen autó ilyen évjáratban és márkában.");
            }
            else
            {
                var selectedBrand = lista.Where(x => x.Marka == cmbxBrand.Text).ToList();
                foreach (var item in selectedBrand)
                {
                    lbxCars.Items.Add(item);
                }
            }
        }
    }
}