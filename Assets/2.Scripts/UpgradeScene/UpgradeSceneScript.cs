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

    public TMP_Text currentGoldText;

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
        UpdateGoldUI();
    }

    void UpdateGoldUI()
    {
        if (GoldManager.Instance != null)
        {
            currentGoldText.text = GoldManager.Instance.CurrentGold.ToString();
        }
    }

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

        if (currentStats == null) return;

        UnitGradeStats upgradeStats = grade == 4 ? currentStats : UnitBalanceDatabase.Instance.GetStats(unit, grade + 1);
        if (upgradeStats == null) return;

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

        // ★ 변경점: 4학년일 때는 가격 대신 MAX 표시
        if (grade >= 4)
        {
            unitUpgradeGoldText.text = "MAX";
        }
        else
        {
            unitUpgradeGoldText.text = currentStats.upgradeCost.ToString();
        }

        unitType = unit;
    }

    public void UpgradeBtnClick()
    {
        // ★ 변경점: 4학년이면 업그레이드 중단
        int currentGrade = UnitUpgradeState.Instance.GetGrade(unitType);
        if (currentGrade >= 4)
        {
            Debug.Log("이미 최고 학년(4학년)입니다!");
            return;
        }

        int upgradeGold = int.Parse(unitUpgradeGoldText.text);

        if (GoldManager.Instance != null && GoldManager.Instance.UseGold(upgradeGold))
        {
            UnitUpgradeState.Instance.UpgradeGrade(unitType);
            SetUnitStat(unitType);
            UpdateGoldUI();
        }
        else
        {
            Debug.Log("골드 부족");
        }
    }

    public void MeleeUnitBtnClick()
    {
        unitDescriptionText.text = "균형잡힌 성능의 컴퓨터공학과 유닛입니다. \n저렴한 비용으로 빠르게 전선을 형성할 수 있습니다.";
        unitScript.ChangeAnimation(meleeUnitSprites);
        SetUnitStat(UnitType.ComputerScience);
    }

    public void TankUnitBtnClick()
    {
        unitDescriptionText.text = "전열을 담당하는 글로벌스포츠산업학과 유닛입니다. \n높은 체력으로 적의 공격을 버티며 아군을 보호합니다.";
        unitScript.ChangeAnimation(tankUnitSprites);
        SetUnitStat(UnitType.GlobalSports);
    }

    public void RangedUnitBtnClick()
    {
        unitDescriptionText.text = "지원 사격을 담당하는 기후변화융합학과 유닛입니다. \n안전한 거리에서 지속적으로 공격을 해줍니다.";
        unitScript.ChangeAnimation(rangedUnitSprites);
        SetUnitStat(UnitType.Climate);
    }

    public void DamageUnitBtnClick()
    {
        unitDescriptionText.text = "화력을 담당하는 화학과 유닛입니다. \n 체력은 낮지만 다중 공격으로 적들을 빠르게 제압합니다.";
        unitScript.ChangeAnimation(damageUnitSprites);
        SetUnitStat(UnitType.Chemistry);
    }

    public void HealerUnitBtnClick()
    {
        unitDescriptionText.text = "전장을 지원하는 통번역학과 유닛입니다. \n부상당한 아군을 치유하여 전선 유지에 기여합니다.";
        unitScript.ChangeAnimation(healerUnitSprites);
        SetUnitStat(UnitType.Interpretation);
    }
}
