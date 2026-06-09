using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [Header("치트 설정")]
    public int cheatAmount = 1000; // 버튼을 누를 때마다 들어올 돈의 양

    // 치트 버튼을 눌렀을 때 실행될 함수
    public void OnClickCheatButton()
    {
        // 아까 만든 GoldManager가 씬에 존재한다면
        if (GoldManager.Instance != null)
        {
            // GoldManager의 AddGold 함수를 불러와서 돈을 추가합니다!
            GoldManager.Instance.AddGold(cheatAmount);
        }
        else
        {
            Debug.LogWarning("맵에 GoldManager가 없습니다! GoldManager 스크립트가 있는지 확인해주세요.");
        }
    }
}
