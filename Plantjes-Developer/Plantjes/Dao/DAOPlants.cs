using System.Collections.Generic;
using System.Linq;
using Plantjes.Models.Db;

namespace Plantjes.Dao
{
    internal class DAOPlants : DAOLogic
    {
        //get a list of all the plants.
        ///Kenny
        public static List<Plant> getAllPlants()
        /// Xander - cleanup
        {
            return context.Plants.ToList();
        }


        ///Owen
        /// Xander - cleanup/optimisation
        public static string GetImages(long id, string ImageCategorie)
        {
            var foto = context.Fotos.Where(s => s.Eigenschap == ImageCategorie).FirstOrDefault(s => s.Plant == id);
            return foto?.UrlLocatie;
        }

        //Written by Andang Kloran
        //Adding the new Plant to the database
        public static void AddPlant(TfgsvType type, TfgsvFamilie familie, TfgsvGeslacht geslacht, TfgsvSoort soort, TfgsvVariant variant, string ratioBloeiBlad, string nederlandseNaam)
        {
            var plant = new Plant
            {
                Type = type.Planttypenaam,
                Familie = familie.Familienaam,
                Geslacht = geslacht.Geslachtnaam,
                Soort = soort.Soortnaam,         
                Fgsv = familie.Familienaam + " " + geslacht.Geslachtnaam + " " + soort.Soortnaam,               
                NederlandsNaam = nederlandseNaam,
                //RatioBloeiBlad = ratioBloeiBlad,
                //Variant = variant.Variantnaam,

            };
            context.Plants.Add(plant);
            context.SaveChanges();
        }
    }
}
