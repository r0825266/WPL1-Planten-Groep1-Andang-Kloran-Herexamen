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
    }
}
