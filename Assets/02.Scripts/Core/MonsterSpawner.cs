using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private float maxSpawnRange = 20f;
    private bool isSpawning = false;


    public void StartSpawn(StageData stageData)
    {
        if (isSpawning) return;
        isSpawning = true;

        foreach (var spawnData in stageData.spawnDatas)
            StartCoroutine(Spawn(spawnData));
    }

    public void StopSpawn()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    IEnumerator Spawn(SpawnData spawnData)
    {
        while (true)
        {
            for (int i = 0; i < spawnData.spawnCount; i++)
            {
                var spawnPosition = GetRandomSpawnPosition(maxSpawnRange);
                Instantiate(spawnData.monsterPrefab, spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnData.spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition(float maxSpawnRange)
    {
        float x = Random.Range(-maxSpawnRange, maxSpawnRange);
        float z = Random.Range(-maxSpawnRange, maxSpawnRange);
        return new Vector3(x, 0, z);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(maxSpawnRange * 2, 1, maxSpawnRange * 2));
    }

}
