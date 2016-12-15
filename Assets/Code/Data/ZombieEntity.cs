using Assets.Code.Controllers;
using Assets.Code.Data;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Code.Data
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieEntity : MonoBehaviour
    {
        public NavMeshAgent Agent;
        public float Speed = 1;

        // Only for debug 
        public Vector3 CurrentPosition;
        public Vector3 CurrentDestination;


        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerEntity>())
                this.OnPlayerCollide();
        }

        protected virtual void OnPlayerCollide()
        {
            this.GetComponentInParent<GameController>().ZombieEatPlayer();
        }

        public void StartWalk(Level level)
        {
            this.StartCoroutine(this.StartWalkCoroutine(level));
        }

        public void StartFollowing(PlayerEntity player)
        {
            this.StartCoroutine(this.StartFollowingCoroutine(player));
        }

        public void StopWalking()
        {
            this.StopAllCoroutines();
        }

        private IEnumerator StartWalkCoroutine(Level level)
        {
            var navigationPath = new NavMeshPath();
            var wait = new WaitForFixedUpdate();
            while (true)
            {
                var cell = level.GroundCells.GetRandom();
                var coord = new Vector3(cell.x, 0, cell.y);
                this.Agent.CalculatePath(coord, navigationPath);

                foreach (var point in navigationPath.corners)
                {
                    this.CurrentDestination = point;
                    while (Vector3.Distance(point, this.transform.position) > this.Speed * 0.01f)
                    {
                        this.transform.Translate((point - this.transform.position).normalized * Time.fixedDeltaTime * this.Speed);
                        this.CurrentPosition = this.transform.position;
                        yield return wait;
                    }

                    this.transform.position = point;
                }
            }
        }

        private IEnumerator StartFollowingCoroutine(PlayerEntity player)
        {
            var navigationPath = new NavMeshPath();
            var wait = new WaitForFixedUpdate();
            while (true)
            {
                var coord = player.transform.position;
                this.Agent.CalculatePath(coord, navigationPath);
                this.transform.Translate((navigationPath.corners[1] - this.transform.position).normalized * Time.fixedDeltaTime * this.Speed);

                yield return wait;
            }
        }
    }
}
