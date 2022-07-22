using System.Collections.Generic;
using System.Linq;
using Plantjes.Models.Db;

namespace Plantjes.Dao
{

    //Gesplitst door Xander
    public class DAOAbiotiek : DAOLogic
    {
        //Get a list of all the Abiotiek types
        public static List<Abiotiek> GetAllAbiotieks()
        {
            return context.Abiotieks.ToList();
        }
        //Get a list of all the AbiotiekMulti types
        public static List<AbiotiekMulti> GetAllAbiotieksMulti()
        {
            //List is unfiltered, a plantId can be present multiple times
            //The aditional filteren will take place in the ViewModel
            return context.AbiotiekMultis.ToList();
        }

        //Xander - optimisation: filter first, then select distinct

        public static List<Abiotiek> filterAbiotiekFromPlant(int selectedItem)
        {
            return context.Abiotieks.Where(s => s.PlantId == selectedItem).Distinct().ToList();
        }

        //Xander - optimisation: filter, then select distinct

        public static List<AbiotiekMulti> filterAbiotiekMultiFromPlant(int selectedItem)
        {
            return context.AbiotiekMultis.Where(s => s.PlantId == selectedItem).Distinct().ToList();
        }
    }
}