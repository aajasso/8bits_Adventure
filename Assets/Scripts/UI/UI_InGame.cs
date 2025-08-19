using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    public static UI_InGame instance;
    public UI_FadeEffect fadeEffect { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitText;
    [SerializeField] private GameObject pauseUI;

    [Header("Music Button")]
    [SerializeField] private Button btnMusic;           // assign in Inspector
    [SerializeField] private GameObject iconMusicOn;    // shown when music is ON
    [SerializeField] private GameObject iconMusicOff;   // shown when music is OFF

    private bool isPaused;

    private void Awake()
    {
        instance = this;
        fadeEffect = GetComponentInChildren<UI_FadeEffect>();

        if (btnMusic != null)
            btnMusic.onClick.AddListener(OnMusicButtonClicked);
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1);

        // Start state = whatever AudioManager says (default ON)
        bool musicOn = AudioManager.instance == null ? true : AudioManager.instance.MusicOn;
        ApplyMusicIcons(musicOn);
    }

    private void OnDestroy()
    {
        if (btnMusic != null)
            btnMusic.onClick.RemoveListener(OnMusicButtonClicked);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PauseButton();
    }

    public void PauseButton()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
    }

    public void GoToMainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void UpdateFruitUI(int collectedFruits, int totalFruits)
    {
        fruitText.text = collectedFruits + "/" + totalFruits;
    }

    public void UpdateTimerUI(float timer)
    {
        timerText.text = timer.ToString("00") + " s";
    }

    // --- Button handler ---
    public void OnMusicButtonClicked()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ToggleMusic();
            ApplyMusicIcons(AudioManager.instance.MusicOn);
        }
    }

    private void ApplyMusicIcons(bool musicOn)
    {
        if (iconMusicOn != null) iconMusicOn.SetActive(musicOn);
        if (iconMusicOff != null) iconMusicOff.SetActive(!musicOn);
    }
}

/*
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_InGame : MonoBehaviour
{
    public static UI_InGame instance;
    public UI_FadeEffect fadeEffect { get; private set; } // read-only

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitText;

    [SerializeField] private GameObject pauseUI;
    private bool isPaused;

    private void Awake()
    {
        instance = this;

        fadeEffect = GetComponentInChildren<UI_FadeEffect>();
    }

    private void Start()
    {
        fadeEffect.ScreenFade(0, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PauseButton();
    }

    public void PauseButton()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
    }

    public void GoToMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateFruitUI(int collectedFruits, int totalFruits)
    {
        fruitText.text = collectedFruits + "/" + totalFruits;
    }

    public void UpdateTimerUI(float timer)
    {
        timerText.text = timer.ToString("00") + " s";
    }
}

*/
