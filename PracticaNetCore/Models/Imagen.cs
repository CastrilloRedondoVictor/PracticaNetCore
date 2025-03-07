using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaNetCore.Models
{
    [Table("IMAGENESZAPASPRACTICA")]
    public class Imagen
    {

        [Column("IDIMAGEN")]
        public int Id { get; set; }
        [Column("IDPRODUCTO")]
        public int IdProducto { get; set; }
        [Column("IMAGEN")]
        public string Url { get; set; }
    }
}
