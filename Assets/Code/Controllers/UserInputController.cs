using Assets.Code.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class UserInputController : MonoBehaviour
    {
        public PlayerEntity Player;

        private void FixedUpdate()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            this.Player.UpdateInputAxes(horizontal, vertical);
        }
    }
}
