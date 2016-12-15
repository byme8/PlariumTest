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
        public List<Vector2> UsedGroundCells;
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
                if (this.UsedGroundCells.Count > 9)
                    continue;

                Vector2 coinCoords = this.CalculateCoinCoords();
                this.CreateCoin(coinCoords);
            }
        }

        private Vector2 CalculateCoinCoords()
        {
            var freeCells = this.GroundCells.Except(this.UsedGroundCells).ToArray();
            var coinCoords = freeCells.GetRandom();
            this.UsedGroundCells.Add(coinCoords);
            return coinCoords;
        }

        private void CreateCoin(Vector2 coinCoords)
        {
            var coin = this.Coin.Create<CoinEntity>(coinCoords, this.CoinRoot.transform);
            coin.OnTake += this.CoinEntity_OnTake;
            coin.OnTake += this.CoinController.CoinEntity_OnTake;
        }

        private void CoinEntity_OnTake(object sender, EventArgs e)
        {
            var coin = sender as CoinEntity;
            coin.OnTake -= this.CoinEntity_OnTake;
            coin.OnTake -= this.CoinController.CoinEntity_OnTake;

            this.UsedGroundCells.Remove(new Vector2(coin.transform.position.x, coin.transform.position.y));

            GameObject.Destroy(coin.gameObject);
        }
    }
}
