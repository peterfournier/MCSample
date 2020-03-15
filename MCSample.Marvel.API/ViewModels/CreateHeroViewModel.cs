using MCSample.Marvel.Domain.ApiModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MCSample.Marvel.API.ViewModels
{
    public class CreateHeroViewModel
    {
        [Required]
        public Guid TeamID { get; set; }

        [Required]
        public POSTHero Hero { get; set; }
    }
}
