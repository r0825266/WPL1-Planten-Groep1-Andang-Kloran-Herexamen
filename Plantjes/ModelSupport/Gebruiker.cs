// ReSharper disable once CheckNamespace
namespace Plantjes.Models.Db
{
    public partial class Gebruiker {
        public string DisplayName => Achternaam + " " + Voornaam;
        public string DisplayNameWithNr => $"{DisplayName} ({Vivesnr})";

        public Gebruiker Clone() {
            return (Gebruiker)MemberwiseClone();
        }
    }
}