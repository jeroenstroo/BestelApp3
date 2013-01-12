using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKlantenControllerClient
{
    class Klanten
    {
        public int KlantId { get; set; }
        public string KlantNaam { get; set; }
        public string KlantVoorNaam { get; set; }
        public string Email { get; set; }
        public int AanspreekTitel { get; set; }
        public int BTW { get; set; }
        public int Adres { get; set; }
        public int Telefoon { get; set; }
        public int Gsm { get; set; }
        public int Fax { get; set; }
    }
    class KlantenCollection : ObservableCollection<Klanten>
    {
        public void CopyFrom(IEnumerable<Klanten> products)
        {
            this.Items.Clear();
            foreach (var p in products)
            {
                this.Items.Add(p);
            }

            this.OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
