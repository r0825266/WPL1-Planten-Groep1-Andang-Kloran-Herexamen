using System;
using System.Collections.Generic;

namespace Plantjes.Models.Db
{
    public partial class Rol
    {
        public Rol()
        {
            Gebruikers = new HashSet<Gebruiker>();
            GebruikersInBehandelings = new HashSet<GebruikersInBehandeling>();
        }

        public int Id { get; set; }
        public string Omschrijving { get; set; }

        public virtual ICollection<Gebruiker> Gebruikers { get; set; }
        public virtual ICollection<GebruikersInBehandeling> GebruikersInBehandelings { get; set; }
    }
}
