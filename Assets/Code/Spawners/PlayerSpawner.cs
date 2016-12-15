using Assets.Code.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Spawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject Player;

        public PlayerEntity CreatePlayer(GameObject root, Level level)
        {
            var coords = level.GroundCells.GetRandom();
            return this.Player.Create<PlayerEntity>(new Vector3(coords.x, coords.y, -0.1f), root.transform);
        }
    }
}
