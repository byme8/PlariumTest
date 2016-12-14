using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Data
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerEntity : MonoBehaviour
    {
        public Animator Animator;
        public Rigidbody Rigibody;
        private Vector2 ForceVector;

        private const float MagicFloat = 20.0f;

        private void Start()
        {
            this.Animator = this.GetComponent<Animator>();
        }

        public void UpdateInputAxes(float horizontal, float vertical)
        {
            var walk = horizontal != 0 || vertical != 0;

            // TODO: Rework to avoid setuping equal values
            this.Animator.SetBool("Walk", walk);

            if (walk)
            {
                // Avoid allocations
                this.ForceVector.x = horizontal * MagicFloat;
                this.ForceVector.y = vertical * MagicFloat;
                this.Rigibody.AddForce(this.ForceVector);
            }
        }
    }
}
