﻿using CsvHelper.Configuration;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace TestDatabase.Models
{

     [Table("provider")]
     public class Provider
     {
          [PrimaryKey, Unique]
          public string Name { get; set; }


          public string listOfProducts { get; set; }

          [ManyToMany(typeof(Supplies))]
          public List<Product> Products { get; set; }

          public string Frequency{ get; set; }

   

     }
    public class ProviderMap : ClassMap<Provider>
    {
        public ProviderMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.listOfProducts).Name("Product(s)");
            Map(m => m.Frequency).Name("Frequency");
        }
    }
}
