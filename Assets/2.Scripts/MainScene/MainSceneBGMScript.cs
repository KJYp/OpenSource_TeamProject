using UnityEngine;

public class MainSceneBGMScript : MonoBehaviour
{
    public AudioSource bgmSource;

    public AudioClip startStoryBGM;
    public AudioClip tutorialMainBGM;
    public AudioClip endBGM;

    public void PlayStartStoryBGM()
    {
        PlayBGM(startStoryBGM, false);
    }

    public void PlayTutorialMainBGM(bool resume)
    {
        PlayBGM(tutorialMainBGM, resume);
    }

    public void PlayEndBGM()
    {
        PlayBGM(endBGM, false);
    }

    private void PlayBGM(AudioClip clip, bool resume)
    {
        if (clip == null || bgmSource == null)
        {
            return;
        }
            

        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
        }
        
        bgmSource.loop = true;

        if (resume)
        {
            float savedTime = PlayerPrefs.GetFloat("BGMTime", 0f);

            if (savedTime < clip.length)
                bgmSource.time = savedTime;
        }

        bgmSource.Play();
    }

    public void SaveMainBGMTime()
    {
        if (bgmSource == null)
            return;

        PlayerPrefs.SetFloat("BGMTime", bgmSource.time);
        PlayerPrefs.Save();
    }
}
