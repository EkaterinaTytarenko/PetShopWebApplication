using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWebApplication.Models
{
    public class Food
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(35)]
        public string Name { get; set; }

       // public ICollection<SpeciesFood> SpeciesFood { get; set; }
    }
}
