using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopWebApplication.Models
{
    public class SpeciesFood
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int SpeciesID { get; set; }
        public Species Species { get; set; }

        public int FoodID { get; set; }
        public Food Food { get; set; }
    }
}
