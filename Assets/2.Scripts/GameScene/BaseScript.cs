using UnityEngine;

public class baseScript : MonoBehaviour
{
    public GameSceneScript GameSceneScript;

    public UnitTeam team;
    public float maxHp = 1000;
    public float currentHp;
    public bool isDestroyed = false;

    void Awake()
    {
        currentHp = maxHp;
    }

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