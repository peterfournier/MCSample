using System;

namespace MCSample.Marvel.Domain.ApiModels
{
    public class POSTHero
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public POSTHero() : base()
        {
            ID = Guid.NewGuid(); // for demo purposes only
        }

    }
}
