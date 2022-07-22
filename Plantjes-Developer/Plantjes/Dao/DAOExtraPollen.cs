using Plantjes.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace Plantjes.Dao
{
    //Gesplitst door Warre
    internal class DAOExtraPollen : DAOLogic
    {
        public static List<ExtraPollenwaarde> FillExtraPollenwaardes()
        {
            return context.ExtraPollenwaardes.ToList();
        }
    }
}
