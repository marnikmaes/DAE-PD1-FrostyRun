using FrostyRun.PD1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostyRun.FrostyElements
{
    public class IceSpike : IcePlatform
    {
        public IceSpike(SpriteSheet spriteSheet) : base(spriteSheet)
        {
            IsEnemy = true;
        }
    }
}
