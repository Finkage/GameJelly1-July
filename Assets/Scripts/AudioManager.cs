using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource soundtrack;

    public bool muteMusic = false;
    public bool muteSounds = false;
    public float volMusic;
    public float volSounds;

    private void Awake()
    {
        // Prevent multiple instances of SaveManager (singleton pattern)
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Prevent this game object from being destroyed upon restart
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void PauseAudio(AudioSource audio)
    {
        audio.Pause();
    }
}
