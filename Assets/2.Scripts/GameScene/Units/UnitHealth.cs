using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    private UnitSounds sounds;
    private UnitStats stats;
    private float currentHp;

    private void Awake()
    {
        sounds = GetComponent<UnitSounds>();
        stats = GetComponent<UnitStats>();
        currentHp = stats.maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (stats.isDead)
        {
            return;
        }

        currentHp -= damage;

        Debug.Log($"{gameObject.name} took {damage} damage. Current HP: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        stats.isDead = true;

        // ★ [추가됨] 적군이 죽을 때 마나 보상 지급 실행!
        GiveKillReward();

        UnitMovement movement = GetComponent<UnitMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }

        UnitCombat combat = GetComponent<UnitCombat>();
        if (combat != null)
        {
            combat.enabled = false;
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        UnitAnimationController animationController = GetComponent<UnitAnimationController>();
        if (animationController != null)
        {
            animationController.PlayDie();
        }

        Debug.Log($"{gameObject.name} died.");

        Destroy(gameObject, 1.5f);
    }

    // ========================================================
    // ★ [추가됨] 마나 보상 계산 및 지급 로직
    // ========================================================
    private void GiveKillReward()
    {
        // 1. 아군이 죽었을 때는 마나를 주면 안 되므로, 이름에 "Enemy"가 포함되어 있는지 확인합니다.
        if (!gameObject.name.Contains("Enemy")) return;

        // 2. 게임 씬 매니저 찾기
        GameSceneScript gameScene = FindAnyObjectByType<GameSceneScript>();
        if (gameScene == null) return;

        // 3. 유닛 종류별 기본 처치 마나 세팅 (기획안 수치 적용)
        int baseMana = 15;
        string unitName = stats.unitType.ToString(); // 유닛 타입 이름 가져오기

        // (스펠링이 다를 수 있어 포함 단어로 넉넉하게 매칭되도록 처리했습니다)
        if (unitName.Contains("Melee") || unitName.Contains("Computer")) baseMana = 12; // 컴공과
        else if (unitName.Contains("Ranged") || unitName.Contains("Climate")) baseMana = 14; // 기후학과
        else if (unitName.Contains("Damage") || unitName.Contains("Chemistry")) baseMana = 15; // 화학과
        else if (unitName.Contains("Tank") || unitName.Contains("Global") || unitName.Contains("Sports")) baseMana = 18; // 글스산
        else if (unitName.Contains("Healer") || unitName.Contains("Translation")) baseMana = 16; // 통번역학과

        // 4. 학년 보너스 (적 유닛 스탯에 학년 정보가 연결되어 있다면 여기서 적용)
        int gradeBonus = 0;
        // (적의 학년 데이터를 가져올 수 있는 구조라면: 2학년=+1, 3학년=+2, 4학년=+4)

        // 5. 스테이지 보정치
        // (기획안에 구체적인 소수점 수치가 안 보여서, 임시로 스테이지가 오를 때마다 1.1배, 1.2배가 되도록 설정)
        float stageMultiplier = 1.0f + (gameScene.stageParameter * 0.1f);

        // 6. 최종 공식: (기본 + 학년) * 스테이지 보정치 후 소수점 버림
        int finalMana = Mathf.FloorToInt((baseMana + gradeBonus) * stageMultiplier);

        // 마나 지급!
        gameScene.AddMana(finalMana);
        Debug.Log($"적 처치! 마나 획득: +{finalMana} (기본:{baseMana}, 보너스:{gradeBonus}, 보정치:{stageMultiplier})");
    }
    // ========================================================

    public void Heal(float amount)
    {
        if (stats.isDead)
        {
            return;
        }

        currentHp += amount;

        if (currentHp > stats.maxHp)
        {
            currentHp = stats.maxHp;
        }

        Debug.Log($"{gameObject.name} healed {amount}. Current HP: {currentHp}");
    }

    public float GetCurrentHp()
    {
        return currentHp;
    }

    public float GetHpRatio()
    {
        if (stats == null || stats.maxHp <= 0)
        {
            return 0f;
        }

        return Mathf.Clamp01(currentHp / stats.maxHp);
    }

    public bool IsFullHp()
    {
        return currentHp >= stats.maxHp;
    }

    public void ResetHealthToMax()
    {
        currentHp = stats.maxHp;
    }
}
