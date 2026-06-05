using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeSceneScript : MonoBehaviour
{
    public UnitType unitType;

    public UpgradeSceneUnitScript unitScript;

    public GoldManager goldManager;
    public TMP_Text currentGoldText;

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

        SetAPSprite(false); // [����] APSprite[0] ���� ���� ����, ���� �Լ� ���

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

        if (unitScript != null && emptyImage != null && emptyImage.Length > 0) // [����] �迭 ������� üũ
        {
            unitScript.ChangeAnimation(emptyImage);
        }

        if (hidePanel != null) // [�߰�] null ���
        {
            hidePanel.SetActive(true);
        }

        UpdateGoldUI();
    }

    void Update()
    {
    }

    void UpdateGoldUI()
    {
        if (goldManager != null && currentGoldText != null)
        {
            currentGoldText.text = goldManager.currentGold.ToString();
        }
    }

    void SetAPSprite(bool isHealer) // [�߰�] APSprite �ε��� ���� ó���� �Լ�
    {
        if (APSprite == null || APSprite.Length == 0) return;

        int index = isHealer ? 1 : 0;

        if (index >= APSprite.Length) // [�߰�] 1�� �ε��� ������ 0������ ��ü
        {
            index = 0;
        }

        if (beforeAPImage != null)
        {
            beforeAPImage.sprite = APSprite[index];
        }

        if (afterAPImage != null)
        {
            afterAPImage.sprite = APSprite[index];
        }
    }

    void ChangeUnitAnimation(Sprite[] sprites) // [�߰�] �ִϸ��̼� �迭 ���� ó���� �Լ�
    {
        if (unitScript != null && sprites != null && sprites.Length > 0)
        {
            unitScript.ChangeAnimation(sprites);
        }
    }

    public void BackBtnClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SetUnitStat(UnitType unit)
    {
        if (hidePanel != null) // [�߰�] null ���
        {
            hidePanel.SetActive(false);
        }

        SetAPSprite(unit == UnitType.Interpretation); // [����] APSprite ���� ���� ����

        if (UnitUpgradeState.Instance == null || UnitBalanceDatabase.Instance == null)
        {
            Debug.LogError("UnitUpgradeState �Ǵ� UnitBalanceDatabase Instance�� �����ϴ�.");
            return;
        }

        int grade = UnitUpgradeState.Instance.GetGrade(unit);

        UnitGradeStats currentStats = UnitBalanceDatabase.Instance.GetStats(unit, grade);


        if (currentStats == null)
        {
            Debug.LogError($"{unit} {grade}�г� ���� �����͸� ã�� �� �����ϴ�.");
            return;
        }

        UnitGradeStats upgradeStats = grade == 4
            ? currentStats
            : UnitBalanceDatabase.Instance.GetStats(unit, grade + 1);

        if (upgradeStats == null)
        {
            Debug.LogError($"{unit} {grade + 1}�г� ���� �����͸� ã�� �� �����ϴ�.");
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

        unitUpgradeGoldText.text = grade >= 4 ? "MAX" : currentStats.upgradeCost.ToString();
        unitType = unit;
    }

    public void UpgradeBtnClick()
    {
        if (!int.TryParse(unitUpgradeGoldText.text, out int upgradeGold))

            if (goldManager != null && goldManager.UseGold(upgradeGold))
            {
                UnitUpgradeState.Instance.UpgradeGrade(unitType);

                SetUnitStat(unitType);
                UpdateGoldUI();
            }
            else
            {
                Debug.Log("��� ����");
            }
    }

    public void MeleeUnitBtnClick()
    {

        unitDescriptionText.text = "�������� ������ ��ǻ�Ͱ��а� �����Դϴ�. \n������ ������� ������ ������ ������ �� �ֽ��ϴ�.";
        unitScript.ChangeAnimation(meleeUnitSprites);

        unitDescriptionText.text = "�İ��� �����Դϴ�.";
        ChangeUnitAnimation(meleeUnitSprites); // [����] ���� �Լ� ���

        SetUnitStat(UnitType.ComputerScience);
    }

    public void TankUnitBtnClick()
    {

        unitDescriptionText.text = "������ ����ϴ� �۷ι�����������а� �����Դϴ�. \n���� ü������ ���� ������ ��Ƽ�� �Ʊ��� ��ȣ�մϴ�.";
        unitScript.ChangeAnimation(tankUnitSprites);

        SetUnitStat(UnitType.GlobalSports);
    }

    public void RangedUnitBtnClick()
    {

        unitDescriptionText.text = "���� ����� ����ϴ� ���ĺ�ȭ�����а� �����Դϴ�. \n������ �Ÿ����� ���������� ������ ���ݴϴ�.";
        unitScript.ChangeAnimation(rangedUnitSprites);
        unitDescriptionText.text = "�����а� �����Դϴ�.";
        ChangeUnitAnimation(rangedUnitSprites); // [����] ���� �Լ� ���

        SetUnitStat(UnitType.Climate);
    }

    public void DamageUnitBtnClick()
    {

        unitDescriptionText.text = "ȭ���� ����ϴ� ȭ�а� �����Դϴ�. \n ü���� ������ ���� �������� ������ ������ �����մϴ�.";
        unitScript.ChangeAnimation(damageUnitSprites);
        unitDescriptionText.text = "ȭ�а� �����Դϴ�.";
        ChangeUnitAnimation(damageUnitSprites); // [����] ���� �Լ� ���

        SetUnitStat(UnitType.Chemistry);
    }

    public void HealerUnitBtnClick()
    {

        unitDescriptionText.text = "������ �����ϴ� ������а� �����Դϴ�. \n�λ���� �Ʊ��� ġ���Ͽ� ���� ������ �⿩�մϴ�.";
        unitScript.ChangeAnimation(healerUnitSprites);
        unitDescriptionText.text = "������� �����Դϴ�.";
        ChangeUnitAnimation(healerUnitSprites); // [����] ���� �Լ� ���

        SetUnitStat(UnitType.Interpretation);
    }
}

