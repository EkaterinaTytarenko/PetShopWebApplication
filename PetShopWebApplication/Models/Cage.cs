using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWebApplication.Models
{
    public class Cage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        public int ShopID { get; set; }
        [Display(Name = "Назва магазину")]
        public Shop Shop { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Номер не може бути від'ємним")]
        [Display(Name = "Номер клітки")]
        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        public int Number { get; set; }

        [Display(Name = "Площа клітки")]
        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        [Range(0, int.MaxValue, ErrorMessage = "Площа не може бути від'ємною")]
        public int Square { get; set; }

       // public ICollection<Animal> Animals { get; set; }
    }
}
