using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action<string> StageChanged;
    public event Action<ulong> GoldChanged;

    [SerializeField] private string stage = "Statge";
    [SerializeField] private ulong gold = 100;

    public CharacterAI player;

    public string CurrentStage => stage;
    public ulong Gold => gold;

    protected override void Initialize()
    {
        base.Initialize();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAI>();
    }
    public void SetStage(string stage)
    {
        this.stage = stage;
        StageChanged?.Invoke(stage);
    }

    public void SetGold(ulong gold)
    {
        this.gold = gold;
        GoldChanged?.Invoke(gold);
    }

}
