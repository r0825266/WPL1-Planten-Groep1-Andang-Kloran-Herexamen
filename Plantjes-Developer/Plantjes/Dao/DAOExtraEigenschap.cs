using Plantjes.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace Plantjes.Dao
{
    //Gesplitst door Andang
    internal class DAOExtraEigenschap : DAOLogic
    {
        //Xander - return object directly
        public static List<ExtraEigenschap> GetAllExtraEigenschap()
        {
            return context.ExtraEigenschaps.ToList();
        }
        //Xander - return object directly, filter first, then distinct
        public static List<ExtraEigenschap> FilterExtraEigenschapFromPlant(int selectedItem)
        {
            return context.ExtraEigenschaps.Where(s => s.PlantId == selectedItem).Distinct().ToList();
        }
    }
}
