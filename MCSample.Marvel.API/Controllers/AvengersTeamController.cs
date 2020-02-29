using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSample.Marvel.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MCSample.Marvel.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvengersTeamController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AvengersTeamController> _logger;

        public AvengersTeamController(ILogger<AvengersTeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public AvengersTeam Get()
        {
            var team = new AvengersTeam();
            team.Add(new Hero
            {
                ID = 2,
                Name = "Captain America"
            });

            return team;
        }
    }
}
