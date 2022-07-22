using Plantjes.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace Plantjes.Dao
{
    //Gesplitst door Warre
    internal class DAOExtraNectar : DAOLogic
    {
        //Xander - return object directly
        public static List<ExtraNectarwaarde> FillExtraNectarwaardes()
        {
            return context.ExtraNectarwaardes.ToList();
        }
    }
}
