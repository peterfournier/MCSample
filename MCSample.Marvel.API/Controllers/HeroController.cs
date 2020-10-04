using GraniteCore;
using MCSample.Marvel.Domain.Dtos;
using MCSample.Marvel.Domain.ApiModels;
using MCSample.Marvel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using System;
using External.WebServices.Marvel;
using MCSample.Marvel.API.ViewModels;

namespace MCSample.Marvel.API.Controllers
{
    [ApiController]
    [Route("heros")]
    public class HeroController : MarvelControllerBase
    {
        private readonly IAvengersTeamService _avengersTeamService;
        private readonly IHeroService _heroService;
        private readonly ILogger<HeroController> _logger;
        private readonly IGraniteMapper _mapper;
        private readonly RestAPIClient _marvelRestClient;

        public HeroController(
            RestAPIClient marvelRestClient,
            IAvengersTeamService avengersTeamService,
            IHeroService heroService,
            IGraniteMapper mapper,
            ILogger<HeroController> logger)
        {
            _avengersTeamService = avengersTeamService;
            _heroService = heroService;
            _marvelRestClient = marvelRestClient;
            _mapper= mapper;
            _logger = logger;
        }

        [HttpGet("search-marvel")]
        public async Task<IActionResult> SearchHeros(string nameStartsWith)
        {
            var results= await _marvelRestClient.SearchMarvelCharacters(nameStartsWith);
            return StatusCode(HttpStatusCode.OK.GetHashCode(), results);
        }

        [HttpGet("{heroID}")]
        public async Task<ActionResult<GETHero>> GetHero(Guid heroID)
        {
            if (heroID == null || heroID.Equals(Guid.Empty)) return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please specific a hero ID."); // todo create a data object to respond with.
            var hero = await _heroService.GetByID(heroID);

            return StatusCode(HttpStatusCode.OK.GetHashCode(), hero);
        }

        [HttpPost]
        public async Task<ActionResult<POSTHero>> CreateHero([FromBody] CreateHeroViewModel createHeroViewModel)
        {
            if (ModelState.IsValid == false) return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Invalid Hero Model."); // todo enable better property ModelBinding

            if (createHeroViewModel.TeamID == null || createHeroViewModel.TeamID.Equals(Guid.Empty)) return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please a team ID to assign the hero to.");

            var team = await _avengersTeamService.GetByID(createHeroViewModel.TeamID);

            if (team == null) return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), $"Unable to locate avengers team with ID: {createHeroViewModel.TeamID}. Please check your ID and try again.");

            if (createHeroViewModel.Hero == null) return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Invalid Hero Model.");

            var dto =await _heroService.Create(_mapper.Map<POSTHero,HeroDto>(createHeroViewModel.Hero));

            team.HeroRoster.Add(dto); // todo there is no unique Hero validation in the Repo

            await _avengersTeamService.Update(createHeroViewModel.TeamID, team);

            return CreatedAtAction(nameof(GetHero), new { heroID = dto.ID }, dto);
        }
    }
}
