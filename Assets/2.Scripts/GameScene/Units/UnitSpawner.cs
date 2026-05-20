using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Units")]
    public GameObject meleeUnitPrefab;
    public GameObject tankUnitPrefab;
    public GameObject rangedUnitPrefab;
    public GameObject healerUnitPrefab;
    public GameObject damageUnitPrefab;

    [Header("Spawn Point")]
    public Transform allySpawnPoint;

    public void SpawnMelee()
    {
        SpawnUnit(meleeUnitPrefab);
    }

    public void SpawnTank()
    {
        SpawnUnit(tankUnitPrefab);
    }

    public void SpawnRanged()
    {
        SpawnUnit(rangedUnitPrefab);
    }

    public void SpawnHealer()
    {
        SpawnUnit(healerUnitPrefab);
    }

    public void SpawnDamage()
    {
        SpawnUnit(damageUnitPrefab);
    }

    private void SpawnUnit(GameObject prefab)
    {
        if (prefab == null || allySpawnPoint == null)
        {
            Debug.LogWarning("Prefab 또는 SpawnPoint가 비어 있음");
            return;
        }

        Instantiate(prefab, allySpawnPoint.position, Quaternion.identity);
    }
}