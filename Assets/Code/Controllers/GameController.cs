using Assets.Code.Data;
using Assets.Code.Spawner;
using Assets.Code.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    [RequireComponent(typeof(LevelCreator))]
    [RequireComponent(typeof(CoinSpawner))]
    [RequireComponent(typeof(PlayerSpawner))]
    [RequireComponent(typeof(UserInputController))]
    [RequireComponent(typeof(ZombieSpawner))]
    public class GameController : MonoBehaviour
    {
        public GameObject LevelRoot;
        public Camera Camera;


        void Awake()
        {
            var levelCreator = this.GetComponent<LevelCreator>();
            var level = levelCreator.CreateLevel(this.LevelRoot, 30);

            var playerController = this.GetComponent<PlayerSpawner>();
            var player = playerController.CreatePlayer(this.LevelRoot, level);
            this.Camera.transform.parent = player.transform;
            this.Camera.transform.localPosition = new Vector3(0, 0, -10);

            var userInputController = this.GetComponent<UserInputController>();
            userInputController.Player = player.GetComponent<PlayerEntity>();

            var coinController = this.GetComponent<CoinSpawner>();
            coinController.GroundCells = level.GroundCells;
            coinController.StartCoinGeneration();

            var zombieController = this.GetComponent<ZombieSpawner>();
        }
    }
}