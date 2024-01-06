using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_PRODUCTOSMVC.Models
{
    public class Producto
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? caracteristicas { get; set; }
        public char? calidad { get; set; }
        public decimal? precio { get; set; }
        public DateTime? fecha_ingreso { get; set; }
        public int? unidades { get; set; }
        public Boolean? estado { get; set; }    
        
      
        //public virtual Estado estado { get; set; }
        
    }
}
