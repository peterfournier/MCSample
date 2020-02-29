using GraniteCore;

namespace MCSample.Marvel.Domain
{
    public class Hero : BaseModel<int>
    {
        public string Name { get; set; }
    }
}
