using Assets.Code.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(LevelCreator))]
    [RequireComponent(typeof(CoinController))]
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(UserInputController))]
    public class GameController : MonoBehaviour
    {
        public GameObject LevelRoot;
        public Camera Camera;

        private CoinController CoinController;

        void Start()
        {
            var levelCreator = this.GetComponent<LevelCreator>();
            var level = levelCreator.CreateLevel(this.LevelRoot, 15);

            var playerController = this.GetComponent<PlayerController>();
            var player = playerController.CreatePlayer(this.LevelRoot, level);
            this.Camera.transform.parent = player.transform;
            this.Camera.transform.localPosition = new Vector3(0, 0,-10);

            var userInputController = this.GetComponent<UserInputController>();
            userInputController.Player = player.GetComponent<PlayerEntity>();

            this.CoinController = this.GetComponent<CoinController>();
            this.CoinController.GroundCells = level.GroundCells;
            this.CoinController.StartCoinGeneration();
        }
    }
}