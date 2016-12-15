using Assets.Code.Data;
using Assets.Code.Spawner;
using Assets.Code.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Code.Controllers
{
    [RequireComponent(typeof(LevelCreator))]
    [RequireComponent(typeof(CoinSpawner))]
    [RequireComponent(typeof(CoinController))]
    [RequireComponent(typeof(PlayerSpawner))]
    [RequireComponent(typeof(UserInputController))]
    [RequireComponent(typeof(ZombieSpawner))]
    public class GameController : MonoBehaviour
    {
        public GameObject LevelRoot;
        public Camera Camera;

        public CoinController CoinController;
        public ZombieSpawner ZombieSpawner;
        public CoinSpawner CoinSpawner;
        public UserInputController UserInputController;
        public PlayerSpawner PayerController;
        public LevelCreator LevelCreator;

        void Start()
        {
            this.LevelCreator = this.GetComponent<LevelCreator>();
            this.PayerController = this.GetComponent<PlayerSpawner>();
            this.UserInputController = this.GetComponent<UserInputController>();
            this.CoinSpawner = this.GetComponent<CoinSpawner>();
            this.CoinController = this.GetComponent<CoinController>();
            this.ZombieSpawner = this.GetComponent<ZombieSpawner>();
            this.ZombieSpawner.LevelRoot = this.LevelRoot.transform;

            this.StartGame();
        }

        public void StartGame()
        {
            var level = this.LevelCreator.CreateLevel(this.LevelRoot, 30);
            var player = this.PayerController.CreatePlayer(this.LevelRoot, level);
            this.Camera.transform.parent = player.transform;
            this.Camera.transform.localPosition = new Vector3(0, 0, -10);

            this.ZombieSpawner.Level = level;
            this.CoinSpawner.StartCoinGeneration();
            this.CoinSpawner.GroundCells = level.GroundCells;
            this.UserInputController.Player = player.GetComponent<PlayerEntity>();

            this.StartCoroutine(this.StartStoryCoroutine(level, player));
        }

        private IEnumerator StartStoryCoroutine(Level level, PlayerEntity player)
        {
            var firstZombie = this.ZombieSpawner.CreateZombie();
            firstZombie.StartWalk(level);

            yield return new WaitWhile(() => this.CoinController.Coins < 5);

            var secondZombie = this.ZombieSpawner.CreateZombie();
            secondZombie.StartWalk(level);

            yield return new WaitWhile(() => this.CoinController.Coins < 20);

            foreach (var zombie in new[]{ firstZombie, secondZombie })
            {
                zombie.StopWalking();
                zombie.StartFollowing(player);
            }
        }

        public void ZombieEatPlayer()
        {

            var coins = this.CoinController.Coins;
            Debug.Log("Zombie eat you!");
        }
    }
}