using System.Collections.Generic;
using System.Linq;
using Plantjes.Models.Db;
    

namespace Plantjes.Dao
{
    //Gesplitst door Andang
    public class DAOFoto : DAOLogic
    {
        //Xander - return object directly
        public static List<Foto> GetAllFoto()
        {
            return context.Fotos.ToList();
        }


        public static string GetImages(long id, string ImageCategorie)
        {
            var foto = context.Fotos.Where(s => s.Eigenschap == ImageCategorie).SingleOrDefault(s => s.Plant == id);


            if (foto != null)
            {
                var location = foto;
                return location.UrlLocatie;
            }

            return null;
        }
    }

}
