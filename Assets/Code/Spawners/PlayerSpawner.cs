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

        public GameObject CreatePlayer(GameObject root, Level level)
        {
            var coords = level.GroundCells.GetRandom();

            var player = GameObject.Instantiate<GameObject>(this.Player, new Vector3(coords.x, coords.y, -0.1f), Quaternion.identity);
            player.transform.parent = root.transform;

            return player;
        }
    }
}
