using Assets.Code.Data;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieEntity : MonoBehaviour
{
    public NavMeshAgent Agent;

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
                while (Vector3.Distance(point, this.transform.position) > 0.01)
                {
                    this.transform.Translate((point - this.transform.position).normalized * Time.fixedDeltaTime);
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
            this.transform.Translate((navigationPath.corners[1] - this.transform.position).normalized * Time.fixedDeltaTime);

            yield return wait;
        }
    }

}
