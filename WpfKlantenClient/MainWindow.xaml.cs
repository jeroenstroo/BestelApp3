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
using System.Net.Http;
using System.Net.Http.Headers;

namespace WpfKlantenControllerClient
{
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        KlantenCollection _products = new KlantenCollection();

        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("http://localhost:47520");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            this.Klantlijst.ItemsSource = _products;
        }
        private async void GetKlanten(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGetProducts.IsEnabled = false;

                var response = await client.GetAsync("api/klant/");
                response.EnsureSuccessStatusCode(); // Throw on error code.

                var klanten = await response.Content.ReadAsAsync<IEnumerable<Klanten>>();
                _products.CopyFrom(klanten);
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // This exception indicates a problem deserializing the request body.
                MessageBox.Show(jEx.Message);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnGetProducts.IsEnabled = true;
            }
        }
        private async void PostKlant(object sender, RoutedEventArgs e)
        {
            btnPostProduct.IsEnabled = false;

            try
            {
                var postklant = new Klanten()
                {
                    KlantNaam = textName.Text,
                    KlantVoorNaam = textVoorNaam.Text,
                    Email = textEmail.Text,
                    Telefoon = Int32.Parse(textTelefoon.Text),
                    Gsm = Int32.Parse(textGsm.Text)
                };
                var response = await client.PostAsJsonAsync("api/klant", postklant);
                response.EnsureSuccessStatusCode(); // Throw on error code.

                _products.Add(postklant);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Price must be a number");
            }
            finally
            {
                btnPostProduct.IsEnabled = true;
            }
        }
        private async void DeleteKlant(object sender, RoutedEventArgs e)
        {
            btnDeleteProduct.IsEnabled = false;

            try
            {
                string todeleteproduct = "api/klant/"+textId.Text;
                var response = await client.DeleteAsync(todeleteproduct);
                //response.EnsureSuccessStatusCode(); // Throw on error code.

                _products.Clear();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("ID must be a naturel number");
            }
            finally
            {
                //MessageBox.Show("worked!");
                btnDeleteProduct.IsEnabled = true;
            }
        }

        private async void GetKlant(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGetKlant.IsEnabled = false;
                string togetklant = "api/klant/" + textGetId.Text;
                var response = await client.GetAsync(togetklant);
                response.EnsureSuccessStatusCode(); // Throw on error code.

                var klant = await response.Content.ReadAsAsync<Klanten>();

                if (klant != null)
                {
                    _products.Clear();
                    _products.Add(klant);
                }
                
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // This exception indicates a problem deserializing the request body.
                MessageBox.Show(jEx.Message);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnGetKlant.IsEnabled = true;
            }
        }

        private async void GetKlantbyVoorNaam(object sender, RoutedEventArgs e)
        {
            try
            {
                string togetklant = "/api/klant?voornaam=" + textGetbyText.Text;
                var response = await client.GetAsync(togetklant);
                response.EnsureSuccessStatusCode(); // Throw on error code.

                var klanten = await response.Content.ReadAsAsync<IEnumerable<Klanten>>();

                if (klanten != null)
                {
                    _products.CopyFrom(klanten);
                }

            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // This exception indicates a problem deserializing the request body.
                MessageBox.Show(jEx.Message);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                RadiobuttonVoornaam.IsChecked = false;
            }
        }

        private async void GetKlantbyNaam(object sender, RoutedEventArgs e)
        {
            try
            {
                string togetklant = "/api/klant?naam=" + textGetbyText.Text;
                var response = await client.GetAsync(togetklant);
                response.EnsureSuccessStatusCode(); // Throw on error code.

                var klanten = await response.Content.ReadAsAsync<IEnumerable<Klanten>>();

                if (klanten != null)
                {
                    _products.CopyFrom(klanten);
                }

            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // This exception indicates a problem deserializing the request body.
                MessageBox.Show(jEx.Message);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                RadiobuttonNaam.IsChecked = false;
            }
        }

    }
}
