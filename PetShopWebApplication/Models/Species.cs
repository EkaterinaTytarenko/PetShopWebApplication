using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWebApplication.Models
{
   
    public class Species
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Display(Name = "Назва виду")]
        [StringLength(35)]
        public string Name { get; set; }

        [Display(Name = "Середня тривалість життя")]
        [Range(0, int.MaxValue, ErrorMessage = "Середня тривалість життя не може бути від'ємною")]
        public int LifeTime { get; set; }

        [Display(Name = "Середня комфортна температура")]
        [Range(-50, 70, ErrorMessage = "Занадто висока або низька температура")]
        public int Temperature { get; set; }

        //public ICollection<Animal> Animals { get; set; }
        [Display(Name = "Їжа")]
        public ICollection<SpeciesFood> SpeciesFood { get; set; }
    }
     /*<div class="form-group">
                <label asp-for="ID" class="control-label"></label>
                <input asp-for="ID" class="form-control" />
                <span asp-validation-for="ID" class="text-danger"></span>
            </div>*/
}
