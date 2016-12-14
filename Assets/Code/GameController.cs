using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelCreator))]
public class GameController : MonoBehaviour
{
    public GameObject LevelRoot;

	void Start ()
    {
        var levelCreator = this.GetComponent<LevelCreator>();
        levelCreator.CreateLevel(this.LevelRoot, 10);
	}
}
