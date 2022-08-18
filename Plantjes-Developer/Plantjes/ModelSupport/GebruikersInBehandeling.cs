// ReSharper disable once CheckNamespace
namespace Plantjes.Models.Db
{
    public partial class GebruikersInBehandeling
    {
        public string DisplayName => Achternaam + " " + Voornaam;
        public string DisplayNameWithEmail => $"{DisplayName} ({Vivesnr})  Email: {Emailadres}";

        public GebruikersInBehandeling Clone() {
            return (GebruikersInBehandeling)MemberwiseClone();
        }
    }
}