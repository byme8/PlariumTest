using Assets.Code.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class CoinController : MonoBehaviour
    {
        public GameObject Coin;
        public GameObject CoinRoot;

        public Vector2[] GroundCells;
        public List<Vector2> UsedGroundCells;

        private Coroutine CoinGeneratorCoroutine;

        public void StartCoinGeneration()
        {
            this.CoinGeneratorCoroutine = this.StartCoroutine(this.GenerateCoinsCoroutine());
        }

        private IEnumerator GenerateCoinsCoroutine()
        {
            var rand = new System.Random();
            var wait = new WaitForSeconds(5);

            while (true)
            {
                yield return wait;
                var freeCells = this.GroundCells.Except(this.UsedGroundCells).ToArray();
                var coinCoords = freeCells[rand.Next(0, freeCells.Length - 1)];
                this.UsedGroundCells.Add(coinCoords);

                this.CreateCoin(coinCoords);
            }
        }

        private void CreateCoin(Vector2 coinCoords)
        {
            var coin = GameObject.Instantiate(this.Coin, new Vector3(coinCoords.x, coinCoords.y, 0), Quaternion.identity);
            coin.transform.parent = this.CoinRoot.transform;

            var coinEntity = coin.GetComponent<CoinEntity>();
            coinEntity.OnTake += this.CoinEntity_OnTake;
        }

        private void CoinEntity_OnTake(object sender, EventArgs e)
        {
            var coin = sender as CoinEntity;
            coin.OnTake -= this.CoinEntity_OnTake;

            this.UsedGroundCells.Remove(new Vector2(coin.transform.position.x, coin.transform.position.y));

            GameObject.Destroy(coin.gameObject);
        }
    }
}
