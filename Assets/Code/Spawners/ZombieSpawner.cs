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

        private void CreateZombie()
        {
            var coord = this.Level.GroundCells.GetRandom();
            var zombie = GameObject.Instantiate(this.Zombie, new Vector3(coord.x, coord.y, 0), Quaternion.identity, this.LevelRoot);
            var zombieEntity = zombie.GetComponent<ZombieEntity>();
        }
    }
}
