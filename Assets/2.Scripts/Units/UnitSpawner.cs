using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Unit Prefabs")]
    public GameObject allyUnitPrefab;
    public GameObject enemyUnitPrefab;
    public GameObject healerUnitPrefab;

    [Header("Spawn Points")]
    public Transform allySpawnPoint;
    public Transform enemySpawnPoint;

    public void SpawnAllyUnit()
    {
        if (allyUnitPrefab == null || allySpawnPoint == null)
        {
            Debug.LogWarning("Ally Unit Prefab 또는 Ally Spawn Point가 설정되지 않았습니다.");
            return;
        }

        Instantiate(allyUnitPrefab, allySpawnPoint.position, Quaternion.identity);
    }

    public void SpawnEnemyUnit()
    {
        if (enemyUnitPrefab == null || enemySpawnPoint == null)
        {
            Debug.LogWarning("Enemy Unit Prefab 또는 Enemy Spawn Point가 설정되지 않았습니다.");
            return;
        }

        Instantiate(enemyUnitPrefab, enemySpawnPoint.position, Quaternion.identity);
    }

    public void SpawnHealerUnit()
    {
        if (healerUnitPrefab == null || allySpawnPoint == null)
        {
            Debug.LogWarning("Healer Unit Prefab 또는 Ally Spawn Point가 설정되지 않았습니다.");
            return;
        }

        Instantiate(healerUnitPrefab, allySpawnPoint.position, Quaternion.identity);
    }
}