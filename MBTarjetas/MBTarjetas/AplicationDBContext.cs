using Microsoft.EntityFrameworkCore;
using MBTarjetas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBTarjetas
{
    public class AplicationDBContext : DbContext
    {
        public DbSet<TarjetaCredito> TarjetaCredito { get; set; }

        public AplicationDBContext(DbContextOptions<AplicationDBContext> options): base(options) { 
        
        
        }

    }
}
