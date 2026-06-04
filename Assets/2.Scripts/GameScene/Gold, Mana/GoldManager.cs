using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [Header("°ñµå ¼³Á¤")]
    public int currentGold = 0;

    public void AddGold(int amount)
    {
        currentGold += amount;
        Debug.Log($"[GoldManager] °ñµå È¹µæ: +{amount}, ÇöÀç °ñµå: {currentGold}");
    }

    public bool UseGold(int amount)
    {
        if (currentGold < amount)
        {
            Debug.Log($"[GoldManager] °ñµå ºÎÁ·! ÇÊ¿ä={amount}, ÇöÀç={currentGold}");
            return false;
        }

        currentGold -= amount;
        Debug.Log($"[GoldManager] °ñµå »ç¿ë: -{amount}, ÇöÀç °ñµå: {currentGold}");
        return true;
    }
}
