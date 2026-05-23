using UnityEngine;

public class UnitSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Unit Sounds")]
    public AudioClip attackSound;
    public AudioClip dieSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void PlayAttackSound()
    {
        PlaySound(attackSound);
    }

    public void PlayDieSound()
    {
        PlaySound(dieSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        audioSource.PlayOneShot(clip);
    }
}
