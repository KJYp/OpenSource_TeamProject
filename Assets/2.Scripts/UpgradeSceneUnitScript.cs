using UnityEngine;
using UnityEngine.UI;

public class UpgradeSceneUnitScript : MonoBehaviour
{
    [Header("Unit Model Image")]
    public Image previewImage;

    [Header("Animation Sprites")]
    public Sprite[] currentSprites;
    
    [Header("Animation")]
    public float animationSpeed = 1.5f;

    private int spriteIndex = 0;
    private float timer = 0f;

    void Update()
    {
        if (currentSprites.Length == 0)
            return;

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

        previewImage.sprite = currentSprites[0];
    }
}