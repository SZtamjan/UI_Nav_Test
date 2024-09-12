using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmiesController : MonoBehaviour
{
    [SerializeField] private List<Sprite> AllEnemies;
    [SerializeField] private List<SpawnPoint> SpawnPoints;
    [SerializeField] private GameObject EnemyPrefab;

    private int MaxEnemies = 3;
    private int CurrentEnemies = 0;

    private void Awake()
    {
        ConfigureEnemiesController();
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void OnEnable()
    {
        AttachListeners();
    }

    private void OnDisable()
    {
        DettachListeners();
    }

    private void AttachListeners()
    {
        GameEvents.EnemyKilled += EnemyKilled;
    }

    private void DettachListeners()
    {
        GameEvents.EnemyKilled -= EnemyKilled;
    }

    private void EnemyKilled(Enemy enemy)
    {
        FreeSpawnPoint(enemy.GetEnemyPosition());
        DestroyKilledEnemy(enemy.GetEnemyObject());
        StartCoroutine(SpawnEnemyViaCor());
        
    }

    private void SpawnEnemies()
    {
        while (CurrentEnemies < MaxEnemies)
        {
            SpawnEnemy();
        }
    }

    private IEnumerator SpawnEnemyViaCor()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if(CurrentEnemies >= MaxEnemies)
        {
            Debug.LogError("Max Enemies reached! Kil some to spawn new");
            return;
        }

        int FreeSpawnPointIndex = -1;
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            if (!SpawnPoints[i].IsOccupied)
            {
                FreeSpawnPointIndex = i;
                break;
            }
        }

        if (FreeSpawnPointIndex != -1)
        {
            SpawnPoints[FreeSpawnPointIndex].IsOccupied = true;
            SoulEnemy Enemy = Instantiate(EnemyPrefab, SpawnPoints[FreeSpawnPointIndex].Position.position, Quaternion.identity, transform).GetComponent<SoulEnemy>();
            int SpriteIndex = Random.Range(0, AllEnemies.Count);
            Enemy.SetupEnemy(AllEnemies[SpriteIndex], SpawnPoints[FreeSpawnPointIndex]);
            CurrentEnemies++;
        }
    }

    private void DestroyKilledEnemy(GameObject enemy)
    {
        Destroy(enemy);
    }
    private void FreeSpawnPoint(SpawnPoint spawnPoint)
    {
        for(int i =0;i<SpawnPoints.Count;i++)
        {
            if(spawnPoint == SpawnPoints[i])
            {
                SpawnPoints[i].IsOccupied = false;
                CurrentEnemies--;
                break;
            }
        }
    }


    private void ConfigureEnemiesController()
    {
        if (SpawnPoints != null)
            MaxEnemies = SpawnPoints.Count;
        else
            MaxEnemies = 3;
    }



}


[System.Serializable]
public class SpawnPoint
{
    public Transform Position;
    public bool IsOccupied;
}
