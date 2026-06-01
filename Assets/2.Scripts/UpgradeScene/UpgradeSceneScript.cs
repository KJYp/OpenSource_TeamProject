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

        int grade = UnitUpgradeState.Instance.GetGrade(unit);
        UnitGradeStats currentStats = UnitBalanceDatabase.Instance.GetStats(unit, grade);
        UnitGradeStats upgradeStats = grade == 4 ? currentStats : UnitBalanceDatabase.Instance.GetStats(unit, grade+1);


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
        unitDescriptionText.text = "컴공과 유닛입니다.";
        unitScript.ChangeAnimation(meleeUnitSprites);
        SetUnitStat(UnitType.ComputerScience);
    }

    //글스산 유닛 선택
    public void TankUnitBtnClick()
    {
        unitDescriptionText.text = "글스산 유닛입니다.";
        unitScript.ChangeAnimation(tankUnitSprites);
        SetUnitStat(UnitType.GlobalSports);
    }

    //기후학과 유닛 선택
    public void RangedUnitBtnClick()
    {
        unitDescriptionText.text = "기후학과 유닛입니다.";
        unitScript.ChangeAnimation(rangedUnitSprites);
        SetUnitStat(UnitType.Climate);
    }

    //화학과 유닛 선택
    public void DamageUnitBtnClick()
    {
        unitDescriptionText.text = "화학과 유닛입니다.";
        unitScript.ChangeAnimation(damageUnitSprites);
        SetUnitStat(UnitType.Chemistry);
    }

    //통번역과 유닛 선택
    public void HealerUnitBtnClick()
    {
        unitDescriptionText.text = "통번역과 유닛입니다.";
        unitScript.ChangeAnimation(healerUnitSprites);
        SetUnitStat(UnitType.Interpretation);
    }
}
