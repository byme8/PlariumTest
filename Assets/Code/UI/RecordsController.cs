using Assets.Code.Seralization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.UI
{
    public class RecordsController : MonoBehaviour
    {
        public GameObject Rows;
        public GameObject Row;
        public MenuController Menu;

        public void SetUsers(Record[] users)
        {
            foreach (Transform row in this.Rows.transform)
                GameObject.Destroy(row.gameObject);

            var position = Vector2.zero;
            foreach (var user in users.OrderByDescending(o => o.LaunchTime))
            {
                var row = GameObject.Instantiate<GameObject>(this.Row, this.Rows.transform);
                var rect = row.GetComponent<RectTransform>();
                rect.anchoredPosition = position;
                position.y -= rect.rect.height;

                var rowData = row.GetComponent<RecordRowController>();
                rowData.Name.text = user.Name;
                rowData.Coins.text = user.Coins.ToString();
                rowData.Time.text = user.Time.ToString();
                rowData.LaunchDate.text = user.LaunchTime.ToShortDateString();
                rowData.Reason.text = user.Reason.ToString();
            }
        }

        public void Quit()
        {
            this.Menu.ShowMainMenu();
        }
    }
}
