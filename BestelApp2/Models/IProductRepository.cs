using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestelApp2.Models
{
    public interface IKlantRepository
    {
        IEnumerable<Klanten> GetAll();
        Klanten Get(int id);
        Klanten Add(Klanten item);
        void Remove(int id);
        bool Update(Klanten item);
    }
}