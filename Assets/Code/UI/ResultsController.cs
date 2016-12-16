using Assets.Code.Seralization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    public class ResultsController : MonoBehaviour
    {
        public Text CoinsText;
        public Text TimeText;
        public Text Name;
        public MenuController MainMenu;

        private int Coins;
        private float Time;
        private GameEndReason Reason;

        public void SetCoins(int coins)
        {
            this.Coins = coins;
            this.CoinsText.text = string.Format("Coins : {0}", coins.ToString());
        }

        public void SetTime(float time)
        {
            this.Time = time;
            this.TimeText.text = string.Format("Seconds : {0}", time.ToString());
        }

        public void Save()
        {
            using (var repository = new UserRepository())
            {
                repository.Users.Add(new User
                {
                    Name = this.Name.text,
                    Coins = this.Coins,
                    Time = this.Time,
                    LaunchTime = DateTime.Now,
                    Reason = this.Reason
                });
            }

            this.MainMenu.ShowRecords();
        }

        public void SetFinishReason(GameEndReason reason)
        {
            this.Reason = reason;
        }
    }
}
