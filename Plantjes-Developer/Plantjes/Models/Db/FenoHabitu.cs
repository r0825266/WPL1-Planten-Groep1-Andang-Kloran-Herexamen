﻿using System;
using System.Collections.Generic;

namespace Plantjes.Models.Db
{
    public partial class FenoHabitu
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public byte[] Figuur { get; set; }
        public string UrlLocatie { get; set; }
    }
}