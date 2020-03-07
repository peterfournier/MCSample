using System.Collections.Generic;
using GraniteCore;
using MCSample.Marvel.Domain.Dtos;
using MCSample.Marvel.Domain.ApiModels;
using MCSample.Marvel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using System.Linq;

namespace MCSample.Marvel.API.Controllers
{
    [ApiController]
    [Route("avengers-team")]
    public class AvengersTeamController : ControllerBase
    {
        private readonly IAvengersTeamService _avengersTeamService;
        private readonly ILogger<AvengersTeamController> _logger;
        private readonly IGraniteMapper _mapper;

        public AvengersTeamController(
            IAvengersTeamService avengersTeamService,
            IGraniteMapper mapper,
            IHeroService heroService,
            ILogger<AvengersTeamController> logger)
        {
            _avengersTeamService = avengersTeamService;
            _mapper= mapper;
            _logger = logger;
        }

        [HttpGet]
        public IQueryable<AvengersTeamDto> Get()
        {
            return _avengersTeamService.GetAll();
        }

        [HttpGet("{teamID}")]
        public IActionResult GetTeam(int teamID)
        {
            var team = _avengersTeamService.GetById(teamID);

            return StatusCode(HttpStatusCode.OK.GetHashCode(), team);
        }

        [HttpPost]
        public async Task<ActionResult<POSTHero>> PostTeam(AvengersTeamDto avengersTeam)
        {
            var team = await _avengersTeamService.Create(avengersTeam);

            return CreatedAtAction(nameof(GetTeam), new { teamID = team.ID }, team);
        }
    }
}
