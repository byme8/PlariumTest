using Assets.Code.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class PlayerController : MonoBehaviour
    {
        private System.Random random = new System.Random();

        public GameObject Player;

        public GameObject CreatePlayer(GameObject root, Level level)
        {
            var coords = level.GroundCells[this.random.Next(0, level.GroundCells.Length - 1)];

            var player = GameObject.Instantiate<GameObject>(this.Player, new Vector3(coords.x, coords.y, -0.1f), Quaternion.identity);
            player.transform.parent = root.transform;

            return player;
        }
    }
}
