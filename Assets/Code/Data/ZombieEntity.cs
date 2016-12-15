using Assets.Code;
using Assets.Code.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieEntity : MonoBehaviour
{
    void Start()
    {
        this.StartCoroutine(this.StartFollowingCoroutine());
    }

    public void StartWalk()
    {

    }

    private IEnumerator StartWalkCoroutine()
    {
        throw new NotImplementedException();
    }

    private IEnumerator StartFollowingCoroutine()
    {
        throw new NotImplementedException();
    }

}
