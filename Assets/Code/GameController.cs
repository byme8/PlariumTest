using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(LevelCreator))]
    [RequireComponent(typeof(CoinController))]
    public class GameController : MonoBehaviour
    {
        public GameObject LevelRoot;

        private CoinController CoinController;

        void Start()
        {
            var levelCreator = this.GetComponent<LevelCreator>();
            var level = levelCreator.CreateLevel(this.LevelRoot, 10);

            this.CoinController = this.GetComponent<CoinController>();
            this.CoinController.GroundCells = level.GroundCells;
            this.CoinController.StartCoinGeneration();
        }
    }
}