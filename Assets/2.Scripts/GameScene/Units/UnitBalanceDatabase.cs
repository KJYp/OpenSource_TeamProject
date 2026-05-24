using System.Collections.Generic;
using UnityEngine;

public class UnitBalanceDatabase : MonoBehaviour
{
    [Header("Unit Grade Stats Table")]
    public List<UnitGradeStats> unitStatsTable = new List<UnitGradeStats>();

    public UnitGradeStats GetStats(UnitType unitType, int grade)
    {
        foreach (UnitGradeStats stats in unitStatsTable)
        {
            if (stats.unitType == unitType && stats.grade == grade)
            {
                return stats;
            }
        }

        Debug.LogWarning($"스탯 데이터를 찾을 수 없습니다. UnitType: {unitType}, Grade: {grade}");
        return null;
    }
}