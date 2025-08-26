using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform player;

    protected override void Initialize()
    {
        base.Initialize();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

}
