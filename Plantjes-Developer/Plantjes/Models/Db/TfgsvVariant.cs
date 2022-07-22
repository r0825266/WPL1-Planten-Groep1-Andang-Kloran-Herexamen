﻿using System;
using System.Collections.Generic;

namespace Plantjes.Models.Db
{
    public partial class TfgsvVariant
    {
        public long VariantId { get; set; }
        public long SoortSoortid { get; set; }
        public string Variantnaam { get; set; }
        public string NlNaam { get; set; }

        public virtual TfgsvSoort SoortSoort { get; set; }
    }
}
