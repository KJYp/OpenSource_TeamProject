using UnityEngine;

public class UnitUpgradeState : MonoBehaviour
{
    public static UnitUpgradeState Instance { get; private set; }

    [Header("Current Grade")]
    public int computerScienceGrade = 1;
    public int climateGrade = 1;
    public int chemistryGrade = 1;
    public int globalSportsGrade = 1;
    public int interpretationGrade = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

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

    public void UpgradeGrade(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.ComputerScience:
                if (this.computerScienceGrade != 4) { this.computerScienceGrade += 1; }
                break;

            case UnitType.Climate:
                if (this.climateGrade != 4) { this.climateGrade += 1; }
                break;

            case UnitType.Chemistry:
                if (this.chemistryGrade != 4) { this.chemistryGrade += 1; }
                break;

            case UnitType.GlobalSports:
                if (this.globalSportsGrade != 4) { this.globalSportsGrade += 1; }
                break;

            case UnitType.Interpretation:
                if (this.interpretationGrade != 4) { this.interpretationGrade += 1; }
                break;

            default:
                break;
        }
    }
}