using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    [Header("°ñµå ¼³Á¤")]
    public int currentGold = 0;

    [Header("°ñµå UI")]
    public TMP_Text goldText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentGold = PlayerPrefs.GetInt("CurrentGold", 0);
    }

    private void Start()
    {
        UpdateGoldUI();
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        SaveGold();
        UpdateGoldUI();
        Debug.Log($"°ñµå È¹µæ: +{amount}, ÇöÀç °ñµå: {currentGold}");
    }

    public bool UseGold(int amount)
    {
        if (currentGold < amount)
        {
            Debug.Log($"°ñµå ºÎÁ·! ÇÊ¿ä: {amount}, ÇöÀç: {currentGold}");
            return false;
        }

        currentGold -= amount;
        SaveGold();
        UpdateGoldUI();
        Debug.Log($"°ñµå »ç¿ë: -{amount}, ÇöÀç °ñµå: {currentGold}");
        return true;
    }

    public void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = currentGold.ToString();
        }
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt("CurrentGold", currentGold);
        PlayerPrefs.Save();
    }
}
