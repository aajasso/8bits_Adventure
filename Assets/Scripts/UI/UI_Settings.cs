using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mixermultiplier = 25;
    [Header("SFX Settings")]
    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI sfxSliderText;
    [SerializeField] private string sfxParameter;

    [Header("BGM Settings")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private TextMeshProUGUI bgmSliderText;
    [SerializeField] private string bgmParameter;

    public void SFXSliderValue(float value)
    {
        sfxSliderText.text = Mathf.RoundToInt(value * 100) + "%";
        float newValue = Mathf.Log10(value) * mixermultiplier;
        audioMixer.SetFloat(sfxParameter, newValue);
    }

    public void BgmSliderValue(float value) 
    {
        bgmSliderText.text = Mathf.RoundToInt(value * 100) + "%";
        float newValue = Mathf.Log10(value) * mixermultiplier;
        audioMixer.SetFloat(bgmParameter, newValue);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(sfxParameter, slider.value);
        PlayerPrefs.SetFloat(bgmParameter, bgmSlider.value);

    }

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat(sfxParameter, .7f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParameter, .7f);


    }
}
