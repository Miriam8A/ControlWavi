using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ControlWavi.Models;

namespace ControlWavi.Data;
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ControlWavi.Models.Contacto> DataContactos { get; set; }

        public DbSet<ControlWavi.Models.Producto> DataProductos { get; set; } 
    
        public DbSet<ControlWavi.Models.Proforma> DataProforma { get; set; }

        public DbSet<ControlWavi.Models.Pago> DataPago { get; set; }

        public DbSet<ControlWavi.Models.Pedido> DataPedido { get; set; }

         public DbSet<ControlWavi.Models.DetallePedido> DataDetallePedido { get; set; }
    }

    