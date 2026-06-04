using UnityEngine;
using UnityEngine.UI;

public class UpgradeSceneUnitScript : MonoBehaviour
{
    [Header("Unit Model Image")]
    public Image previewImage;

    [Header("Animation Sprites")]
    public Sprite[] currentSprites;

    [Header("Animation")]
    public float animationSpeed = 0.2f;

    private int spriteIndex = 0;
    private float timer = 0f;

    private void Update()
    {
        if (previewImage == null)
        {
            return;
        }

        if (currentSprites == null || currentSprites.Length == 0)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= animationSpeed)
        {
            timer = 0f;

            spriteIndex++;

            if (spriteIndex >= currentSprites.Length)
            {
                spriteIndex = 0;
            }

            previewImage.sprite = currentSprites[spriteIndex];
        }
    }

    public void ChangeAnimation(Sprite[] newSprites)
    {
        currentSprites = newSprites;
        spriteIndex = 0;
        timer = 0f;

        if (previewImage == null)
        {
            Debug.LogWarning("Preview Image가 연결되지 않았습니다.");
            return;
        }

        if (currentSprites == null || currentSprites.Length == 0)
        {
            previewImage.sprite = null;
            return;
        }

        previewImage.sprite = currentSprites[0];
    }
}