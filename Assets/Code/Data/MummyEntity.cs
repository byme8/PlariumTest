using Assets.Code.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Data
{
    public class MummyEntity : ZombieEntity
    {
        public MummyEntity()
        {
            this.Speed = 2;
        }

        protected override void OnPlayerCollide()
        {
            this.GetComponent<GameController>().MummyEatPlayer();
        }
    }
}
