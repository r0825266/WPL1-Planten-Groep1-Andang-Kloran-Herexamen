using Plantjes.Models.Db;
using System.Linq;

namespace Plantjes.Dao
{
    //Gesplitst door Warre
    internal class DAOTfgsv : DAOLogic
    {
        //Xander - return object directly
        public static IQueryable<TfgsvType> fillTfgsvType()
        {
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext
            return context.TfgsvTypes.Distinct();
        }
        //Xander - return object directly
        public static IQueryable<TfgsvFamilie> fillTfgsvFamilie(int selectedItem)
        {
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext

            if (selectedItem > 0)
            {
                return context.TfgsvFamilies.Distinct().OrderBy(s => s.Familienaam).Where(s => s.TypeTypeid == selectedItem);
            }
            else
            {
                return context.TfgsvFamilies.Distinct().OrderBy(s => s.Familienaam);
            }
        }
        //Xander - return object directly
        public static IQueryable<TfgsvGeslacht> fillTfgsvGeslacht(int selectedItem)
        {
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext
            if (selectedItem > 0)
            {
                return context.TfgsvGeslachts.Distinct().OrderBy(s => s.Geslachtnaam).Where(s => s.FamilieFamileId == selectedItem);
            }
            else
            {
                return context.TfgsvGeslachts.Distinct().OrderBy(s => s.Geslachtnaam);
            }
        }
        //Xander - return object directly
        public static IQueryable<TfgsvSoort> fillTfgsvSoort(int selectedItem)
        {
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext
            if (selectedItem > 0)
            {
                return context.TfgsvSoorts.Where(s => s.GeslachtGeslachtId == selectedItem).OrderBy(s => s.Soortnaam).Distinct();
            }
            else
            {
                return context.TfgsvSoorts.Distinct().OrderBy(s => s.Soortnaam);
            }
        }
        //Xander - return object directly
        public static IQueryable<TfgsvVariant> fillTfgsvVariant()
        {
            // request List of wanted type
            // distinct to prevrent more than one of each type
            // The if else is to check if something is selected in the previous combobox. if its not he doesn't filter
            // Here we use IQueryable<T>, it makes it easier for us to use our search queries and find the objects that we need.
            // This will also make it possible for us to use all the properties instead of only a selection of an object in our ViewModels.
            // Good way to interact with our datacontext

            return context.TfgsvVariants.Distinct().OrderBy(s => s.Variantnaam);
        }
    }
}
