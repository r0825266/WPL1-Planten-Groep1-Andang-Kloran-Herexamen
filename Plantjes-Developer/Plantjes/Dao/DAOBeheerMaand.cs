using System.Collections.Generic;
using System.Linq;
using Plantjes.Models.Db;

namespace Plantjes.Dao
{
    //Gesplitst door Xander
    public class DAOBeheerMaand : DAOLogic
    {
        //Xander - optimisation: filter first then select distinct
        public static List<BeheerMaand> FilterBeheerMaandFromPlant(int selectedItem)
        {
            return context.BeheerMaands.Where(s => s.PlantId == selectedItem).Distinct().ToList();
        }

        public static List<BeheerMaand> FillBeheerdaad()
        {
            return context.BeheerMaands.ToList();
        }

        //Get a list of all the Beheermaand types
        public static List<BeheerMaand> GetBeheerMaanden()
        {
            return context.BeheerMaands.ToList();
        }
    }
}