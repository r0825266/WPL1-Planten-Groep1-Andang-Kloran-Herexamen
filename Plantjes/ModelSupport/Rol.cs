// ReSharper disable once CheckNamespace
namespace Plantjes.Models.Db
{
    public partial class Rol {
        public bool CanEditPlants => Omschrijving != "Oud-Student";
        public bool CanManageUsers => Omschrijving == "Docent";
    }
}