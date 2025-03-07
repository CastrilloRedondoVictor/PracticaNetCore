using Microsoft.EntityFrameworkCore;
using PracticaNetCore.Models;

namespace PracticaNetCore.Data
{
    public class ZapatillasContext: DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options) : base(options)
        {
        }
        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
    }
}
