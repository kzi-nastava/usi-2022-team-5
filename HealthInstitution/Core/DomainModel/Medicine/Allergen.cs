﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core
{
    public class Allergen
    {
        private int _id;
        private string _name;

        [JsonProperty("Id")]
        public int ID { get => _id; set { _id = value; } }
        [JsonProperty("Name")]
        public string Name { get => _name; set { _name = value; } }

        public Allergen()
        {

        }

        public Allergen(string name)
        {
            _name = name;
        }

        public Allergen(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}