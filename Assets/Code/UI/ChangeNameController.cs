using Assets.Code.Seralization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    public class ChangeNameController : MonoBehaviour
    {
        public Text Name;
        public MenuController MainMenu;

        public void Save()
        {
            using (var repository = new UserRepository())
            {
                repository.User = new User { Name = this.Name.text };
            }

            this.MainMenu.ShowMainMenu();
        }

        public void UpdateValues()
        {
            using (var repository = new UserRepository())
            {
                this.Name.text = repository.User.Name;
            }
        }
    }
}
