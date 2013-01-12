using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BestelApp2.Models;

namespace BestelApp2.Controllers
{
    public class KlantController : ApiController
    {
        static readonly IKlantRepository repository = new KlantRepository();

        public IEnumerable<Klanten> GetAllKlanten()
        {
            return repository.GetAll();
        }
        public Klanten GetKlanten(int id)
        {
            Klanten item = repository.Get(id);
            if (item == null || !(item is Klanten))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        public IEnumerable<Klanten> GetKlantenByVoornaam(string voornaam)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.KlantVoorNaam, voornaam, StringComparison.OrdinalIgnoreCase));
        }
        public IEnumerable<Klanten> GetKlantenByNaam(string naam)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.KlantNaam, naam, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostKlant(Klanten item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Klanten>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.KlantId });
            response.Headers.Location = new Uri(uri);
            return response;
        }
        public void PutKlant(int id, Klanten klant)
        {
            klant.KlantId = id;
            if (!repository.Update(klant))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public void DeleteKlant(int id, Klanten product)
        {
            Klanten item = repository.Get(id);
            if (item == null || !(item is Klanten))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(id);
        }
    }
}

