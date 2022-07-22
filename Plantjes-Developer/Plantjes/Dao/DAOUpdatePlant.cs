using Plantjes.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace Plantjes.Dao
{
    //Gesplitst door Warre
    internal class DAOUpdatePlant : DAOLogic
    {
        //Xander - return object directly
        public static List<UpdatePlant> GetAllUpdatePlant()
        {
            return context.UpdatePlants.ToList();
        }
    }
}
