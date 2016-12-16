using Assets.Code.Controllers;
using Assets.Code.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Spawners
{
    [RequireComponent(typeof(CoinController))]
    public class CoinSpawner : MonoBehaviour
    {
        public GameObject Coin;
        public GameObject CoinRoot;

        public Vector2[] GroundCells;
        public List<CoinEntity> Coins;
        public CoinController CoinController;

        private Coroutine CoinGeneratorCoroutine;

        private void Start()
        {
            this.CoinController = this.GetComponent<CoinController>();
        }

        public void StartCoinGeneration()
        {
            this.CoinGeneratorCoroutine = this.StartCoroutine(this.GenerateCoinsCoroutine());
        }

        private IEnumerator GenerateCoinsCoroutine()
        {
            var wait = new WaitForSeconds(5);

            while (true)
            {
                yield return wait;
                if (this.Coins.Count > 9)
                    continue;

                Vector2 coinCoords = this.CalculateCoinCoords();
                this.CreateCoin(coinCoords);
            }
        }

        private Vector2 CalculateCoinCoords()
        {
            var freeCells = this.GroundCells.Except(this.Coins.Select(o => o.Coords)).ToArray();
            var coinCoords = freeCells.GetRandom();
            return coinCoords;
        }

        private void CreateCoin(Vector2 coinCoords)
        {
            var coin = this.Coin.Create<CoinEntity>(coinCoords, this.CoinRoot.transform);
            coin.Coords = coinCoords;
            coin.OnTake += this.CoinEntity_OnTake;
            coin.OnTake += this.CoinController.CoinEntity_OnTake;

            this.Coins.Add(coin);
        }

        private void CoinEntity_OnTake(object sender, EventArgs e)
        {
            var coin = sender as CoinEntity;
            coin.OnTake -= this.CoinEntity_OnTake;
            coin.OnTake -= this.CoinController.CoinEntity_OnTake;

            this.Coins.Remove(coin);

            GameObject.Destroy(coin.gameObject);
        }
    }
}
