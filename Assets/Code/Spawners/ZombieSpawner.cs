using Assets.Code.Data;
using Assets.Code.Spawners;
using UnityEngine;

namespace Assets.Code.Spawner
{
    public class ZombieSpawner : MonoBehaviour
    {
        public GameObject Zombie;
        public GameObject Mummy;
        public Level Level;

        public Transform LevelRoot;

        public ZombieEntity CreateZombie()
        {
            var coord = this.Level.GroundCells.GetRandom();
            return this.Zombie.Create<ZombieEntity>(coord, this.LevelRoot);
        }

        public ZombieEntity CreateMummy()
        {
            var coord = this.Level.GroundCells.GetRandom();
            return this.Mummy.Create<MummyEntity>(coord, this.LevelRoot);
        }
    }
}
