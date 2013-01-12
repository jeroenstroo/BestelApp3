using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestelApp2.Models
{
    public class KlantRepository : IKlantRepository
    {
        private List<Klanten> products = new List<Klanten>();
        private int _nextId = 1;

        public KlantRepository()
        {
            Add(new Klanten { KlantNaam = "Stroo", KlantVoorNaam = "Jeroen", Email = "jeroen@gmail.com", Telefoon = 50781234, Gsm = 475228997 });
            Add(new Klanten { KlantNaam = "Vanvoren", KlantVoorNaam = "Tom", Email = "Tom@gmail.com", Telefoon = 59127595, Gsm = 492548796 });
            Add(new Klanten { KlantNaam = "Dewaele", KlantVoorNaam = "Hans", Email = "Hansd@gmail.com", Telefoon = 9872548, Gsm = 48123595 });
            Add(new Klanten { KlantNaam = "Verest", KlantVoorNaam = "Tommy", Email = "tommy@hotmailcom", Telefoon = 50784785, Gsm = 49213568 });
        }

        public IEnumerable<Klanten> GetAll()
        {
            return products;
        }

        public Klanten Get(int id)
        {
            return products.Find(p => p.KlantId == id);
        }

        public Klanten Add(Klanten item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.KlantId = _nextId++;
            products.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            products.RemoveAll(p => p.KlantId == id);
        }

        public bool Update(Klanten item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = products.FindIndex(p => p.KlantId == item.KlantId);
            if (index == -1)
            {
                return false;
            }
            products.RemoveAt(index);
            products.Add(item);
            return true;
        }
    }
}