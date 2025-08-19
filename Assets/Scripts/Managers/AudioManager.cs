using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private int bgmIndex;

    // NEW: state + persistence
    public bool MusicOn { get; private set; } = true;
    private const string MusicOnPrefKey = "musicOn";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // restore (default = ON). If you want “always ON” ignore saved value and call SetMusicOn(true, false)
            //bool saved = PlayerPrefs.GetInt(MusicOnPrefKey, 1) == 1;
            SetMusicOn(true, save: true); // would be saved instead of true, if we want to save user preference and save:false

            PlayRandomBGM(); // <-- Start music immediately
            InvokeRepeating(nameof(PlayMusicIfNeeded), 0f, 2f);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // NEW: public API for UI
    public void SetMusicOn(bool on, bool save = true)
    {
        MusicOn = on;

        if (!MusicOn)
        {
            // stop any bgm currently playing
            for (int i = 0; i < bgm.Length; i++)
                if (bgm[i] != null) bgm[i].Stop();
        }
        else
        {
            PlayMusicIfNeeded();
        }

        if (save) PlayerPrefs.SetInt(MusicOnPrefKey, MusicOn ? 1 : 0);
    }

    public void ToggleMusic() => SetMusicOn(!MusicOn);

    public void PlayMusicIfNeeded()
    {
        if (!MusicOn) return;                 // respect state
        if (bgm.Length == 0) return;

        if (bgmIndex < 0 || bgmIndex >= bgm.Length) bgmIndex = 0;

        if (bgm[bgmIndex] == null || bgm[bgmIndex].isPlaying == false)
            PlayRandomBGM();
    }

    public void PlayRandomBGM()
    {
        if (!MusicOn) return;
        if (bgm.Length == 0) return;

        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    private void PlayBGM(int bgmToPlay)
    {
        if (!MusicOn) return;
        if (bgm.Length == 0) return;

        for (int i = 0; i < bgm.Length; i++)
            if (bgm[i] != null) bgm[i].Stop();

        bgmIndex = Mathf.Clamp(bgmToPlay, 0, bgm.Length - 1);
        if (bgm[bgmIndex] != null) bgm[bgmIndex].Play();
    }

    public void PlaySFX(int sfxToPlay, bool randomPitch = true)
    {
        if (sfxToPlay < 0 || sfxToPlay >= sfx.Length) return;
        if (sfx[sfxToPlay] == null) return;

        if (randomPitch)
            sfx[sfxToPlay].pitch = Random.Range(.9f, 1.1f);

        sfx[sfxToPlay].Play();
    }

    public void StopSFX(int sfxToStop)
    {
        if (sfxToStop < 0 || sfxToStop >= sfx.Length) return;
        if (sfx[sfxToStop] == null) return;
        sfx[sfxToStop].Stop();
    }
}

/*
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource[] sfx;

    [SerializeField] private AudioSource[] bgm;

    [SerializeField] private int bgmIndex;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        InvokeRepeating(nameof(PlayMusicIfNeeded), 0, 2);

    }

    
    public void PlayMusicIfNeeded()
    {
        if (bgm[bgmIndex].isPlaying == false)
            PlayRandomBGM();
    }
    
    
    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }
    
    private void PlayBGM(int bgmToPlay)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }

        bgmIndex = bgmToPlay;
        bgm[bgmToPlay].Play();
    }
    
    

    public void PlaySFX(int sfxToPlay, bool randomPitch =true)
    {
        if (sfxToPlay >= sfx.Length)
            return;

        if (randomPitch)
            sfx[sfxToPlay].pitch = Random.Range(.9f, 1.1f);

        sfx[sfxToPlay].Play();

    }

    public void StopSFX(int sfxToStop) => sfx[sfxToStop].Stop();
}

*/
