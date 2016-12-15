using Assets.Code.Data;
using Assets.Code.Spawners;
using UnityEngine;

namespace Assets.Code.Spawner
{
    public class ZombieSpawner : MonoBehaviour
    {
        public GameObject Zombie;
        public Level Level;

        public Transform LevelRoot;

        public ZombieEntity CreateZombie()
        {
            var coord = this.Level.GroundCells.GetRandom();
            return this.Zombie.Create<ZombieEntity>(coord, this.LevelRoot);
        }
    }
}
