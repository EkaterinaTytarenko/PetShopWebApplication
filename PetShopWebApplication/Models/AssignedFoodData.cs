using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopWebApplication.Models
{
    public class AssignedFoodData
    {
        public int FoodID { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}
