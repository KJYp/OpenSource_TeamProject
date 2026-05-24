using System.Collections;
using UnityEngine;

public class EnemyAutoSpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] enemyUnitPrefabs;

    [Header("Spawn Point")]
    public Transform enemySpawnPoint;

    [Header("Spawn Settings")]
    public float spawnInterval = 5f;
    public int spawnCountPerUnitType = 2;
    public float spawnSpacing = 0.4f;
    public bool spawnOnStart = true;

    [Header("Balance Data")]
    public UnitBalanceDatabase balanceDatabase;
    public int enemyGrade = 1;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        if (spawnOnStart)
        {
            StartAutoSpawn();
        }
    }

    public void StartAutoSpawn()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    public void StopAutoSpawn()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnAllEnemyTypes();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnAllEnemyTypes()
    {
        if (enemyUnitPrefabs == null || enemyUnitPrefabs.Length == 0)
        {
            Debug.LogWarning("EnemyAutoSpawner에 Enemy Prefab이 설정되지 않았습니다.");
            return;
        }

        if (enemySpawnPoint == null)
        {
            Debug.LogWarning("EnemyAutoSpawner에 Enemy Spawn Point가 설정되지 않았습니다.");
            return;
        }

        foreach (GameObject enemyPrefab in enemyUnitPrefabs)
        {
            if (enemyPrefab == null)
            {
                continue;
            }

            for (int i = 0; i < spawnCountPerUnitType; i++)
            {
                Vector3 spawnPosition = enemySpawnPoint.position + new Vector3(i * spawnSpacing, 0f, 0f);

                GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                ApplyEnemyStats(enemyObject);
            }
        }
    }

    private void ApplyEnemyStats(GameObject enemyObject)
    {
        UnitStats unitStats = enemyObject.GetComponent<UnitStats>();

        if (unitStats == null)
        {
            Debug.LogWarning($"{enemyObject.name}에 UnitStats가 없습니다.");
            return;
        }

        unitStats.team = UnitTeam.Enemy;

        if (balanceDatabase != null)
        {
            UnitGradeStats data = balanceDatabase.GetStats(unitStats.unitType, enemyGrade);

            if (data != null)
            {
                unitStats.ApplyBalanceData(data);

                UnitHealth unitHealth = enemyObject.GetComponent<UnitHealth>();

                if (unitHealth != null)
                {
                    unitHealth.ResetHealthToMax();
                }
            }
        }

        Debug.Log($"{unitStats.unitType} 적 유닛 {enemyGrade}학년 생성");
    }
}