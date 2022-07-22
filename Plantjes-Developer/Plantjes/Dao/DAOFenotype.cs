using Plantjes.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace Plantjes.Dao
{
    //Gesplitst door Andang
    internal class DAOFenotype : DAOLogic
    {
        //Xander - return object directly
        public static List<Fenotype> GetAllFenoTypes()
        {
            return context.Fenotypes.ToList();
        }
        //Xander - return object directly
        public static List<Fenotype> fillFenoTypeRatioBloeiBlad()
        {
            //Commentaar van vorige groep.
            // this is NOT part of the cascade function and wil not be added as it is not needed 
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter.
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext

            return context.Fenotypes.Distinct().OrderBy(s => s.RatioBloeiBlad).ToList();
        }
        //Xander - return object directly
        public static List<Fenotype> filterFenoTypeFromPlant(int selectedItem)
        {
            return context.Fenotypes.Distinct().Where(s => s.PlantId == selectedItem).ToList();
        }
        //Xander - return object directly
        public static List<FenotypeMulti> FilterFenotypeMultiFromPlant(int selectedItem)
        {
            return context.FenotypeMultis.Distinct().Where(s => s.PlantId == selectedItem).ToList();
        }
    }
}
