using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constants;

public class GameManager : Singleton<GameManager>
{
    public event Action<string> StageChanged;
    public event Action<ulong> GoldChanged;
    public event Action<int> LevelChanged;

    [SerializeField] private string stage = "Statge";
    [SerializeField] private ulong gold = 100;
    [SerializeField] private int level = 1;

    public CharacterAI player;

    public string CurrentStage => stage;
    public ulong Gold => gold;
    public int Level => level;

    protected override void Initialize()
    {
        base.Initialize();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAI>();

        player.DieAction += GameOver;
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

    public void AddGold(ulong amount)
    {
        gold += amount;
        GoldChanged?.Invoke(gold);
    }
    public void SetLevel(int level)
    {
        this.level = level;
        LevelChanged?.Invoke(level);
    }

    public void AddExperience(float plusExp)
    {
        var expStat = player.Stats.GetResourceStat(StatType.Experience);
        int levelUpCount = 0;
        float current = expStat.CurrentValue + plusExp;
        float final = expStat.FinalValue;

        while (current >= final)
        {
            current -= final;
            levelUpCount++;
        }

        expStat.SetCurrentValue(current);
        if (levelUpCount > 0)
        {
            SetLevel(Level + levelUpCount);
            Debug.Log($"Level Up! New Level: {Level}");
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("LobbyScene");
    }

}
