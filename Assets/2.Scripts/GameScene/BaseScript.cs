using UnityEngine;

public class baseScript : MonoBehaviour
{
    public GameSceneScript GameSceneScript;

    // 적과 아군을 구분하기 위한 팀 정보
    public UnitTeam team;
    public float maxHp = 1000;
    public float currentHp;
    public bool isDestroyed = false;

    // BaseScript가 활성화될 때 체력을 최대치로 초기화
    void Awake()
    {
        currentHp = maxHp;
    }

    // 외부에서 호출되는 피해 처리 함수
    public void TakeDamage(float damage)
    {
        if (isDestroyed)
        {
            return;
        }

        currentHp -= damage;

        Debug.Log($"{gameObject.name} took {damage} damage. Current HP: {currentHp}");

        if (currentHp <= 0)
        {
            currentHp = 0;
            DestroyBase();
        }
    }

    // Base가 파괴될 때 호출되는 함수
    private void DestroyBase()
    {
        isDestroyed = true;

        if (team == UnitTeam.Ally)
        {
            GameSceneScript.WinLose(false);
        }
        else
        {
            GameSceneScript.WinLose(true);
        }
        
        gameObject.SetActive(false);
    }
}