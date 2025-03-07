using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaNetCore.Data;
using PracticaNetCore.Models;


//create procedure SP_IMAGENES_ZAPATILLA
//(@posicion int, @zapatilla int, @registros int out)
//as
//	select @registros = count(*) from IMAGENESZAPASPRACTICA where IDPRODUCTO = @zapatilla

//	select IDIMAGEN, IDPRODUCTO, IMAGEN from 
//	(
//		select ROW_NUMBER() OVER (ORDER BY IDIMAGEN) as POSICION, IDIMAGEN, IDPRODUCTO, IMAGEN
//		from IMAGENESZAPASPRACTICA
//		where IDPRODUCTO = @zapatilla
//	) QUERY
//	where POSICION = @posicion
//go

namespace PracticaNetCore.Repositories
{
    public class RepositoryZapatillas
    {

        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }

        public async Task<Zapatilla> FindZapatillaAsync(int id)
        {
            return await this.context.Zapatillas.FindAsync(id);
        }

        public async Task<(int, Imagen)> GetImagenesZapatillaAsync(int posicion, int zapatilla)
        {
            // Definir el parámetro de salida
            SqlParameter registrosParam = new SqlParameter("@registros", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            // Ejecutar el procedimiento almacenado sin capturar el resultado de empleados
            await this.context.Database.ExecuteSqlRawAsync(
                "EXEC SP_IMAGENES_ZAPATILLA @posicion, @zapatilla, @registros OUT",
                new SqlParameter("@posicion", posicion),
                new SqlParameter("@zapatilla", zapatilla),
                registrosParam
            );

            // Obtener el número total de registros después de ejecutar el procedimiento
            int registros = (registrosParam.Value != DBNull.Value) ? (int)registrosParam.Value : 0;

            // Ejecutar otra consulta para obtener los empleados
            var imagenes = await this.context.Imagenes
                .FromSqlRaw("EXEC SP_IMAGENES_ZAPATILLA @posicion, @zapatilla, @registros OUT",
                    new SqlParameter("@posicion", posicion),
                    new SqlParameter("@zapatilla", zapatilla),
                    registrosParam)
                .AsNoTracking()
                .ToListAsync();

            return (registros, imagenes.FirstOrDefault());
        }
    }
}
