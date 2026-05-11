using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Unit Prefabs")]
    public GameObject allyUnitPrefab;
    public GameObject enemyUnitPrefab;

    [Header("Spawn Points")]
    public Transform allySpawnPoint;
    public Transform enemySpawnPoint;

    public void SpawnAllyUnit()
    {
        Debug.Log("SpawnAllyUnit 버튼 클릭됨");

        if (allyUnitPrefab == null || allySpawnPoint == null)
        {
            Debug.LogWarning("Ally Unit Prefab 또는 Ally Spawn Point가 설정되지 않았습니다.");
            return;
        }

        Instantiate(allyUnitPrefab, allySpawnPoint.position, Quaternion.identity);
    }

    public void SpawnEnemyUnit()
    {
        Debug.Log("SpawnEnemyUnit 버튼 클릭됨");

        if (enemyUnitPrefab == null || enemySpawnPoint == null)
        {
            Debug.LogWarning("Enemy Unit Prefab 또는 Enemy Spawn Point가 설정되지 않았습니다.");
            return;
        }

        Instantiate(enemyUnitPrefab, enemySpawnPoint.position, Quaternion.identity);
    }
}