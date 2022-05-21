using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ControlWavi.Data;
using ControlWavi.Models;

namespace ControlWavi.Controllers.Rest
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoRestController
    {
        private readonly ApplicationDbContext _context;

        public ProductoRestController(ApplicationDbContext context){
             _context = context;
        }

        [HttpGet]
        public IEnumerable<Producto> ListProductos()
        {
             var listProductos=_context.DataProductos.OrderBy(s => s.Id).ToList();   
             return listProductos.ToArray();
        
    }
}
}