using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Mvc;
using System.Data.Objects.SqlClient;


namespace BestelApp2.Models
{
    public class BestelApp : DbContext
    {
        public DbSet<Producten> Producten { get; set; }
        public DbSet<Klanten> Klanten { get; set; }
        public DbSet<Bestellingen> Bestellingen { get; set; }
        public DbSet<Categorie> Categorie { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    public class Producten
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
      
        [Required]
        [StringLength(100, ErrorMessage = "Geef een naam op.", MinimumLength = 3)]
        [Display(Name = "Productnaam")]
        public string ProductNaam { get; set; }

        public int CategorieId { get; set; }
        [ForeignKey("CategorieId")]
        public virtual Categorie Categorie { get; set; }

        [ForeignKey("BestellingId")]
        public virtual ICollection<Bestellingen> Bestelling { get; set; }
    }

    //public class ProductenVerwerking
    //{

    //    private BestelApp db = new BestelApp();

    //    public ProductenVerwerking(BestelApp db)
    //    {
    //        this.db = db;
    //    }

    //    public void Create(Producten producten, String Categorie_nummer)
    //    {
    //        //var test = db.Categorie.Where(p => p.CategorieId == Convert.ToInt32(CategorieNummer));
    //        //var agqry = db.partener.Where(p => p.part_type == 1).Where(p => p.activ == true);
    //        //producten.Categorie = test.CategorieNaam;
    //        //producten.Categorie.CategorieId = Convert.ToInt32(CategorieNummer);
    //        Categorie categorie = new Categorie();
    //        //categorie.CategorieNaam = Categorie_nummer;
    //       // producten.Categorie = categorie;
        
    //        db.Producten.Add(producten);
    //        db.SaveChanges();
    //    }

    //    public IEnumerable<SelectListItem> GetCategorie()
    //    {
    //        //List<SelectListItem> categorieLijst = new List<SelectListItem>();
    //        //foreach (var temp in db.Categorie) 
    //        //{
    //        //    categorieLijst.Add(new SelectListItem { Text = temp.CategorieNaam, Value = temp.CategorieNaam });
            
    //        //}
    //        IEnumerable<SelectListItem> items = db.Categorie.Select(c => new SelectListItem()
    //        {
    //            //Value = SqlFunctions.StringConvert((decimal?)c.CategorieId), //Hetzelfde als ToString(), enkel werkt dit niet in de SQL-omgeving (dropdownlist geeft anders een error)
    //            Value = c.CategorieNaam,
    //            Text = c.CategorieNaam
    //        });


    //        return items;
    //        //return new SelectList(db.Categorie, "CategorieNaam");
    //    }

    //}

    public class Categorie
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CategorieId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Geef een naam op.", MinimumLength = 3)]
        [Display(Name = "Categorienaam")]
        public string CategorieNaam { get; set; }
    }

    public class Klanten
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int KlantId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Geef een naam op.", MinimumLength = 5)]
        [Display(Name = "Klantnaam")]
        public string KlantNaam { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Geef een naam op.", MinimumLength = 5)]
        [Display(Name = "Klantnaam")]
        public string KlantVoorNaam { get; set; }

        [Required]
        [EmailAddressAttribute]
        [Display(Name = "E-mail adres")]
        public string Email { get; set; }

        [Display(Name = "Aanspreektitel")]
        public int AanspreekTitel { get; set; }

        [Display(Name = "BTW-nummer")]
        public int BTW { get; set; }

        [Display(Name = "Adres")]
        public int Adres { get; set; }

        [Display(Name = "Telefoonnummer")]
        public int Telefoon { get; set; }

        [Display(Name = "GSM nummer")]
        public int Gsm { get; set; }

        [Display(Name = "Fax nummer")]
        public int Fax { get; set; }
    }

    public class Bestellingen
    {
        
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BestellingId { get; set; }

        [Display(Name = "Bestelling")]
        public String BestellingsNaam { get; set; }

        [ForeignKey("ProductId")]
        public virtual ICollection<Producten> Product { get; set; }

        public DateTime Datum { get; set; }

        public bool IsTemplate { get; set; }

        [DataType(DataType.MultilineText)]
        public String Opmerkingen { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserProfile UserProfile { get; set; }
    }
}