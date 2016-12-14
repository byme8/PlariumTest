using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Data
{
    public class CoinEntity : MonoBehaviour
    {
        public event EventHandler OnTake;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerEntity>())
                this.OnTake(this, EventArgs.Empty);
        }
    }
}
