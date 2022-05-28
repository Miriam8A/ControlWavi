using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControlWavi.Models;
using ControlWavi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ControlWavi.Util;
using Microsoft.AspNetCore.Identity;

namespace ControlWavi.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CatalogoController(ApplicationDbContext context,
            ILogger<CatalogoController> logger,
            UserManager<IdentityUser> userManager)

            {
                _context = context;
                _logger = logger;
                _userManager = userManager;

            }
            
        public IActionResult Index_catalogo()
        {
            return View();
        }

       public async Task<IActionResult> Consolas_Nintendo()
        {
            return View(await _context.DataProductos.ToListAsync());
            
        }
          public async Task<IActionResult> Consolas_PlayStation()
        {
            return View(await _context.DataProductos.ToListAsync());
        }
          public async Task<IActionResult> Consolas_Sega()
        {
            return View(await _context.DataProductos.ToListAsync());
        }
          public async Task<IActionResult> Consolas_Xbox()
        {
            return View(await _context.DataProductos.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index(string? searchString)
        {
            
            var productos = from o in _context.DataProductos select o;
            //SELECT * FROM t_productos -> &
            if(!String.IsNullOrEmpty(searchString)){
                productos = productos.Where(s => s.Name.Contains(searchString)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }
            productos = productos.Where(s => s.Status.Contains("Activo"));
            
            return View(await productos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id){
            Producto objProduct = await _context.DataProductos.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
            return View(objProduct);
        }

        public async Task<IActionResult> Add(int? id){
            var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                List<Producto> productos = new List<Producto>();
                return  View("Index_catalogo",productos);
            }else{
                var producto = await _context.DataProductos.FindAsync(id);

                Util.SessionExtensions.Set<Producto>(HttpContext.Session,"Producto", producto);
                
                Proforma proforma = new Proforma();
                proforma.Producto = producto;
                proforma.Precio = producto.Precio;
                proforma.Cantidad = 1;
                proforma.UserID = userID;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index_catalogo));
            }

        }
    }
}
