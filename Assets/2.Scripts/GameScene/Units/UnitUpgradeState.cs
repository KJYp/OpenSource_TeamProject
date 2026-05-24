using UnityEngine;

public class UnitUpgradeState : MonoBehaviour
{
    [Header("Current Grade")]
    public int computerScienceGrade = 1;
    public int climateGrade = 1;
    public int chemistryGrade = 1;
    public int globalSportsGrade = 1;
    public int interpretationGrade = 1;

    public int GetGrade(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.ComputerScience:
                return computerScienceGrade;

            case UnitType.Climate:
                return climateGrade;

            case UnitType.Chemistry:
                return chemistryGrade;

            case UnitType.GlobalSports:
                return globalSportsGrade;

            case UnitType.Interpretation:
                return interpretationGrade;

            default:
                return 1;
        }
    }
}