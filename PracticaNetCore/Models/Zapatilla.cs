using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaNetCore.Models
{

    [Table("ZAPASPRACTICA")]
    public class Zapatilla
    {
        [Key]
        [Column("IDPRODUCTO")]
        public int Id { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("PRECIO")]
        public int Precio { get; set; }
    }
}
