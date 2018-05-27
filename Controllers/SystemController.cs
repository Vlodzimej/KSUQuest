using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSUQuest.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace KSUQuest.Controllers
{
    [Produces("application/json")]
    [Route("api/System")]
    public class SystemController : Controller
    {
        public SystemController(DataContext context)
        {
            _context = context;
        }

        private readonly DataContext _context;

        [HttpPost("database")]
        public void CreateDatabase()
        {
            _context.Database.Migrate();
        }
    }
}