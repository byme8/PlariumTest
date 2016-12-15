using Assets.Code.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class CoinController : MonoBehaviour
    {
        public int TakedCoins;

        public void CoinEntity_OnTake(object sender, EventArgs e)
        {
            this.TakedCoins++;
            Debug.Log("Coins: " + this.TakedCoins);
        }
    }
}
