using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using WebApiActividadesApoyo.Models;

namespace WebApiActividadesApoyo.Data
{
    public class FicDBContext : DbContext
    {
        public FicDBContext(DbContextOptions<FicDBContext> options)
            : base(options)
        {

        }
        public DbSet<cat_actividades> cat_Actividades { get; set; }
    }
}
