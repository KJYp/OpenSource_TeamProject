using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeSceneScript : MonoBehaviour
{
    public UnitType unitType;

    public UpgradeSceneUnitScript unitScript;

    public GameObject hidePanel;
    public GameObject[] unitPrefabs;

    public TMP_Text beforeUpgradeHPText;
    public TMP_Text beforeUpgradeAPText;
    public TMP_Text beforeUpgradeACText;
    public TMP_Text beforeUpgradeMSText;
    public TMP_Text beforeUpgradeMCText;

    public TMP_Text afterUpgradeHPText;
    public TMP_Text afterUpgradeAPText;
    public TMP_Text afterUpgradeACText;
    public TMP_Text afterUpgradeMSText;
    public TMP_Text afterUpgradeMCText;

    public TMP_Text unitDescriptionText;
    public TMP_Text unitUpgradeGoldText;

    public Image beforeAPImage;
    public Image afterAPImage;
    public Sprite[] APSprite;

    public Sprite[] emptyImage;
    public Sprite[] meleeUnitSprites;
    public Sprite[] tankUnitSprites;
    public Sprite[] rangedUnitSprites;
    public Sprite[] damageUnitSprites;
    public Sprite[] healerUnitSprites;
    void Start()
    {
        Time.timeScale = 1f;

        PlayerPrefs.SetInt("isMainPanel", 1);
        PlayerPrefs.Save();

        beforeAPImage.sprite = APSprite[0];
        afterAPImage.sprite = APSprite[0];

        beforeUpgradeHPText.text = "0";
        beforeUpgradeAPText.text = "0";
        beforeUpgradeACText.text = "0";
        beforeUpgradeMSText.text = "0";
        beforeUpgradeMCText.text = "0";

        afterUpgradeHPText.text = "0";
        afterUpgradeAPText.text = "0";
        afterUpgradeACText.text = "0";
        afterUpgradeMSText.text = "0";
        afterUpgradeMCText.text = "0";

        unitUpgradeGoldText.text = "0";
        unitScript.ChangeAnimation(emptyImage);

        hidePanel.SetActive(true);
    }

    void Update()
    {
        
    }

    //뒤로가기 버튼
    public void BackBtnClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SetUnitStat(UnitType unit)
    {
        hidePanel.SetActive(false);

        if (unit == UnitType.Interpretation)
        {
            beforeAPImage.sprite = APSprite[1];
            afterAPImage.sprite = APSprite[1];
        }
        else
        {
            beforeAPImage.sprite = APSprite[0];
            afterAPImage.sprite = APSprite[0];
        }

        if (UnitUpgradeState.Instance == null || UnitBalanceDatabase.Instance == null)
        {
            Debug.LogError("UnitUpgradeState 또는 UnitBalanceDatabase Instance가 없습니다.");
            return;
        }

        int grade = UnitUpgradeState.Instance.GetGrade(unit);

        UnitGradeStats currentStats = UnitBalanceDatabase.Instance.GetStats(unit, grade);

        if (currentStats == null)
        {
            Debug.LogError($"{unit} {grade}학년 스탯 데이터를 찾을 수 없습니다.");
            return;
        }

        UnitGradeStats upgradeStats = grade == 4
            ? currentStats
            : UnitBalanceDatabase.Instance.GetStats(unit, grade + 1);

        if (upgradeStats == null)
        {
            Debug.LogError($"{unit} {grade + 1}학년 스탯 데이터를 찾을 수 없습니다.");
            return;
        }


        beforeUpgradeHPText.text = currentStats.maxHp.ToString();
        beforeUpgradeAPText.text = unit == UnitType.Interpretation ? currentStats.healPower.ToString() : currentStats.attackPower.ToString();
        beforeUpgradeACText.text = currentStats.attackCooldown.ToString();
        beforeUpgradeMSText.text = currentStats.moveSpeed.ToString();
        beforeUpgradeMCText.text = currentStats.manaCost.ToString();

        
        afterUpgradeHPText.text = upgradeStats.maxHp.ToString();
        afterUpgradeAPText.text = unit == UnitType.Interpretation ? upgradeStats.healPower.ToString() : upgradeStats.attackPower.ToString();
        afterUpgradeACText.text = upgradeStats.attackCooldown.ToString();
        afterUpgradeMSText.text = upgradeStats.moveSpeed.ToString();
        afterUpgradeMCText.text = upgradeStats.manaCost.ToString();

        
        unitUpgradeGoldText.text = currentStats.upgradeCost.ToString();
        unitType = unit;
    }

    public void UpgradeBtnClick()
    {
        int currentGold = 500;
        int upgradeGold = int.Parse(unitUpgradeGoldText.text);

        if (currentGold >= upgradeGold)
        {
            currentGold -= upgradeGold;

            UnitUpgradeState.Instance.UpgradeGrade(unitType);

            SetUnitStat(unitType);
        } 
        else
        {
            Debug.Log("엥?");
        }
    }

    //컴공과 유닛 선택
    public void MeleeUnitBtnClick()
    {
        unitDescriptionText.text = "균형잡힌 성능의 컴퓨터공학과 유닛입니다. \n저렴한 비용으로 빠르게 전선을 형성할 수 있습니다.";
        unitScript.ChangeAnimation(meleeUnitSprites);
        SetUnitStat(UnitType.ComputerScience);
    }

    //글스산 유닛 선택
    public void TankUnitBtnClick()
    {
        unitDescriptionText.text = "전열을 담당하는 글로벌스포츠산업학과 유닛입니다. \n높은 체력으로 적의 공격을 버티며 아군을 보호합니다.";
        unitScript.ChangeAnimation(tankUnitSprites);
        SetUnitStat(UnitType.GlobalSports);
    }

    //기후학과 유닛 선택
    public void RangedUnitBtnClick()
    {
        unitDescriptionText.text = "지원 사격을 담당하는 기후변화융합학과 유닛입니다. \n안전한 거리에서 지속적으로 공격을 해줍니다.";
        unitScript.ChangeAnimation(rangedUnitSprites);
        SetUnitStat(UnitType.Climate);
    }

    //화학과 유닛 선택
    public void DamageUnitBtnClick()
    {
        unitDescriptionText.text = "화력을 담당하는 화학과 유닛입니다. \n 체력은 낮지만 다중 공격으로 적들을 빠르게 제압합니다.";
        unitScript.ChangeAnimation(damageUnitSprites);
        SetUnitStat(UnitType.Chemistry);
    }

    //통번역과 유닛 선택
    public void HealerUnitBtnClick()
    {
        unitDescriptionText.text = "전장을 지원하는 통번역학과 유닛입니다. \n부상당한 아군을 치유하여 전선 유지에 기여합니다.";
        unitScript.ChangeAnimation(healerUnitSprites);
        SetUnitStat(UnitType.Interpretation);
    }
}
