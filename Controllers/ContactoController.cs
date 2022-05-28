using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControlWavi.Models;
using ControlWavi.Data;
using ControlWavi.Integration.Sengrid;

namespace ControlWavi.Controllers
{
    public class ContactoController: Controller
    {
       private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;

        private readonly SendMailIntegration _sendgrid;

        public ContactoController(ApplicationDbContext context,
            ILogger<ContactoController> logger,
            SendMailIntegration sendgrid)
        {
            _context = context;
            _logger = logger;
            _sendgrid = sendgrid;
        } 

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]

       public async Task<IActionResult> Create(Contacto objContacto)
        {
            _context.Add(objContacto);
            _context.SaveChanges();

            await _sendgrid.SendMail(objContacto.Email,
                objContacto.Name,
                "Bienvenido al e-comerce",
                "Revisaremos su consulta en breves momentos y le responderemos",
                SendMailIntegration.SEND_SENDGRID);

            await _sendgrid.SendMail(objContacto.Email,
                objContacto.Name,
                "Bienvenido al e-comerce",
                "Revisaremos su consulta en breves momentos y le responderemos",
                SendMailIntegration.SEND_REST);

            ViewData["Message"] = "Se registro el contacto";
            return View("Index");
        }
    }
}
