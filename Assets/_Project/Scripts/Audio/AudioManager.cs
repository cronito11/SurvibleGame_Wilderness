using UnityEngine;
using System;
using System.Collections;
using Surviblewilderness;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    [SerializeField] private AudioClip[] mainMenuThemeClips;
    [SerializeField] private AudioClip[] dayAmbienceClips;
    [SerializeField] private AudioClip[] nightAmbienceClips;

    public static AudioManager Instance;

    private int currentMainMenuThemeAudioClipIndex;
    private int currentDayAmbienceThemeAudioClipIndex;
    private int currentNightAmbienceThemeAudioClipIndex;

    private Sound mainMenuThemeSound;
    private Sound dayAmbienceSound;
    private Sound nightAmbienceSound;

    private Coroutine audioCoroutine;

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

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            s.audioSource.loop = s.loop;

            s.audioSource.volume = s.volume;    
            s.audioSource.pitch = s.pitch;
        }
        
    }

    private void OnEnable()
    {
        TimeController.OnChangeTimeOfDay += OnChangeTimeOfDay;
        UiManager.OnButtonClick += PlayButtonClickSound;
    }

   

    private void OnDisable()
    {
        UiManager.OnButtonClick -= PlayButtonClickSound;
    }

    private void OnChangeTimeOfDay(TimeOfDay day)
    {
        if (day == TimeOfDay.Day)
        {
            StopMainMenuThemeAudio();
            StopDayAmbienceThemeAudio();
            StopNightAmbienceThemeAudio();
            
            PlayDayAmbienceSound();
        }
        else
        {
            StopMainMenuThemeAudio();
            StopDayAmbienceThemeAudio();

            PlayNightAmbienceSound();
        }
    }

    public void PlayButtonClickSound()
    {
        PlaySoundOnce("BUTTONCLICK");
    }

    public IEnumerator PlayWaveAlertSound()
    {
        Sound s1 = Array.Find(sounds, sound => sound.name == "WaveAlert");
        Sound s2 = Array.Find(sounds, sound => sound.name == "WaveSpawn");

        if (s1 is null || s2 is null)
            Debug.LogWarning("Either " + s1.name + " or " + s2.name + " audio source is null");

        // Play the Alert audio clip
        s1.audioSource.Play();
        yield return new WaitForSecondsRealtime(s1.audioSource.clip.length);

        // Play the Spawn audio clip
        s2.audioSource.Play();
        yield return new WaitForSecondsRealtime(s2.audioSource.clip.length);
    }

    public void PlaySoundOnce(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s is null)
        {
            Debug.LogWarning("Cound find the sound with name " + name);
            return;
        }
        s.audioSource.Play();   
    }

    public void PlayMainMenuTheme()
    {
        mainMenuThemeSound = Array.Find(sounds, sound => sound.name == "MAINMENUTHEME");

        if(mainMenuThemeSound is null)
        {
            Debug.LogWarning("Cound find the Audio Source with name " + "MAINMENUTHEME");
            return;
        }

        audioCoroutine = StartCoroutine(PlayClipsInSequence(mainMenuThemeSound, mainMenuThemeClips,currentMainMenuThemeAudioClipIndex));   

    }

    public void PlayDayAmbienceSound()
    {
        dayAmbienceSound = Array.Find(sounds, sound => sound.name == "DAYAMBIENCESOUND");

        if(dayAmbienceSound is null)
        {
            Debug.LogWarning("Cound find the sound with name " + "DAYAMBIENCESOUND");
            return;
        }

        audioCoroutine = StartCoroutine(PlayClipsInSequence(dayAmbienceSound, dayAmbienceClips, currentDayAmbienceThemeAudioClipIndex));   

    }

    public void PlayNightAmbienceSound()
    {
        nightAmbienceSound = Array.Find(sounds, sound => sound.name == "NIGHTAMBIENCESOUND");

        if (nightAmbienceSound is null)
        {
            Debug.LogWarning("Cound find the sound with name " + "NIGHTAMBIENCESOUND");
            return;
        }

        audioCoroutine = StartCoroutine(PlayClipsInSequence(nightAmbienceSound, nightAmbienceClips, currentNightAmbienceThemeAudioClipIndex));

    }

    private IEnumerator PlayClipsInSequence(Sound sound, AudioClip[] audios,int index)
    {
        Debug.Log("Mainmenu bg audio is playing");
        while (true)
        {
            sound.audioSource.clip = audios[index];
            sound.audioSource.Play();
            Debug.Log("Clip "+sound.audioSource.clip.name+" is playing");

            // Wait until the current clip finishes playing
            yield return new WaitForSecondsRealtime(sound.audioSource.clip.length);

            // Move to the next clip or loop back to the first one
            index = (index + 1) % audios.Length;
        }
    }

    public void StopMainMenuThemeAudio()
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);  // Stop the current coroutine
            audioCoroutine = null;  // Clear the reference
        }
        mainMenuThemeSound.audioSource.Stop();
        mainMenuThemeSound.audioSource.clip = null;
    }  


    public void StopDayAmbienceThemeAudio()
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);  // Stop the current coroutine
            audioCoroutine = null;  // Clear the reference
        }
        if (dayAmbienceSound is null)
            return;
        dayAmbienceSound.audioSource.Stop();
        dayAmbienceSound.audioSource.clip = null;
    }
    public void StopNightAmbienceThemeAudio()
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);  // Stop the current coroutine
            audioCoroutine = null;  // Clear the reference
        }
        if (nightAmbienceSound is null)
            return;
        
        nightAmbienceSound.audioSource.Stop();
        nightAmbienceSound.audioSource.clip = null;

        
    }
}
