using System.ComponentModel.DataAnnotations;

namespace Web_API_MVC_helpers.Models
{
    public class marcas
    {
        [Key]
        [Display(Name = "ID")]
        public int? id_marca { get; set; }
        [Display(Name = "Nombre")]  
        public string nombre_marca { get; set; }
        [Display(Name = "Estado")]
        public string estados { get; set; }
    }
}
