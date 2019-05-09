using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShopWebApplication.Models
{
    public class Animal
    {
        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        [StringLength(25)]
        [Display(Name = "Ім'я тварини")]
        public string Name { get; set; }

        [Display(Name = "Стать")]
        public char? Sex { get; set; }
        /**/

        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        public int ShopID { get; set; }
        [Display(Name = "Назва магазину")]
        public Shop Shop { get; set; }

        //[Required(ErrorMessage = "Поле повинно бути встановлено")]
        /**/
            //Bind
        public int? CageID { get; set; }
        [Display(Name = "Номер клітки")]
        public Cage Cage { get; set; }

        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        public int SpeciesID { get; set; }
        [Display(Name = "Назва виду")]
        public Species Species { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата народження")]
        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        public DateTime Date { get; set; }

        public int? ColorID { get; set; }
        [Display(Name = "Колір")]
        public Color Color { get; set; }

        [Range(0,int.MaxValue, ErrorMessage = "Ціна не може бути від'ємною")]
        [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Поле повинно бути встановлено")]
        public int Price { get; set; }

    }
    
}
