using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductExample.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}