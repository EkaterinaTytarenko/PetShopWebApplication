using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWebApplication.Models
{
    public class Shop
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        public int ID { get; set; }

        [StringLength(30)]
        [Display(Name = "Назва магазину")]
        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        public string Name { get; set; }

        [Display(Name = "Адреса магазину")]
        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        [StringLength(35)]
        public string Adress { get; set; }

        //public ICollection<Animal> Animals { get; set; }
        [Display(Name = "Список кліток")]
        public ICollection<Cage> Cages { get; set; }
    }
    
    
}
