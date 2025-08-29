using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constants;

public class GameManager : Singleton<GameManager>
{
    public event Action<StageData> StageChanged;
    public event Action<ulong> GoldChanged;
    public event Action<int> LevelChanged;

    [SerializeField] private CharacterAI playerPrefab;
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private StageData stage;
    [SerializeField] private ulong gold = 100;
    [SerializeField] private int level = 1;

    private CharacterAI player;
    public CharacterAI Player
    {
        get
        {
            if (player == null)
                player = Instantiate(playerPrefab);

            return player;
        }
    }

    public StageData CurrentStage => stage;
    public ulong Gold => gold;
    public int Level => level;

    protected override void Initialize()
    {
        base.Initialize();

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    public void OnActiveSceneChanged(Scene current, Scene next)
    {
        if (next.name != "LobbyScene")
        {
            if (stage != null)
                StartGame();
        }
    }

    public void StartGame()
    {
        if (player == null)
            player = Instantiate(playerPrefab);

        if (player)
            player.gameObject.SetActive(true);

        if (monsterSpawner == null)
            monsterSpawner = FindAnyObjectByType<MonsterSpawner>();

        player.Initialize();
        player.DieAction += GameOver;
        monsterSpawner.StartSpawn(stage);
        DontDestroyOnLoad(player.gameObject);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        monsterSpawner.StopSpawn();
        player.DieAction -= GameOver;
        SceneManager.LoadScene("LobbyScene");
    }

    public void SetStage(StageData stage)
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
}
