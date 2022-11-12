using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GeneralSettings : MonoBehaviour
{
    [Header("Screen")]
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;

    [Header("Sound")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider, soundSlider;
    [SerializeField] private TextMeshProUGUI musicPercentage, soundPercentage;

    float screenRatio;
    static bool hasSetDisplay = false;
    static bool isFullscreen = false;
    int currentResolution = 1080;
    int savedScreenResolution = 3;

    private void Awake()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        screenRatio = screenWidth / screenHeight;
        Resolution[] possibleScreenResolutions = Screen.resolutions;
        Resolution maxResolution = possibleScreenResolutions[possibleScreenResolutions.Length - 1];
        float exactScreenRatio = (float)maxResolution.width / (float)maxResolution.height;
        screenRatio = exactScreenRatio;

        PlayerInput.onEscapePressed += OpenCloseWindow;
    }

    private void Start()
    {
        //Loading Screen Settings
        if (PlayerPrefs.HasKey("ScreenResolution"))
        {
            savedScreenResolution = PlayerPrefs.GetInt("ScreenResolution");
            SetResolution(savedScreenResolution);
            resolutionDropdown.value = savedScreenResolution;
            if (!hasSetDisplay)
            {
                hasSetDisplay = true;
                Screen.SetResolution(Mathf.RoundToInt((float)currentResolution * screenRatio), currentResolution, isFullscreen);
            }
        }

        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            int tempFullscreen = PlayerPrefs.GetInt("Fullscreen");
            if (tempFullscreen == 0)
                SetFullscreen(false);
            else
                SetFullscreen(true);

            if (tempFullscreen != 0)
                fullscreenToggle.isOn = true;

            Screen.fullScreen = isFullscreen;
            Screen.SetResolution(Mathf.RoundToInt((float)currentResolution * screenRatio), currentResolution, isFullscreen);
        }

        //Loading Sound Settings
        if (PlayerPrefs.HasKey("MusicVolume"))
            musicSlider.value = PlayerPrefs.GetInt("MusicVolume");
        if (PlayerPrefs.HasKey("SoundVolume"))
            soundSlider.value = PlayerPrefs.GetInt("SoundVolume");

        if (soundPercentage != null)
            soundPercentage.text = Mathf.RoundToInt(soundSlider.value).ToString() + "%";
        if (musicPercentage != null)
            musicPercentage.text = Mathf.RoundToInt(musicSlider.value).ToString() + "%";

        gameObject.SetActive(false);
    }

    public void SetResolution(int newScreenResolution)
    {
        savedScreenResolution = newScreenResolution;
        switch ((ScreenResolution)newScreenResolution)
        {
            case ScreenResolution.UltraHD:
                currentResolution = 2160;
                break;
            case ScreenResolution.WQHD:
                currentResolution = 1440;
                break;
            case ScreenResolution.FullHD:
                currentResolution = 1080;
                break;
            case ScreenResolution.HDReady:
                currentResolution = 720;
                break;
            default:
                currentResolution = 1080;
                break;
        }
        Screen.SetResolution(Mathf.RoundToInt((float)currentResolution * screenRatio), currentResolution, isFullscreen);
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("ScreenResolution", savedScreenResolution);
    }

    public void SetFullscreen(bool newIsFullscreen)
    {
        isFullscreen = newIsFullscreen;
        Screen.fullScreen = isFullscreen;
        Screen.SetResolution(Mathf.RoundToInt((float)currentResolution * screenRatio), currentResolution, isFullscreen);
        if (isFullscreen)
            PlayerPrefs.SetInt("Fullscreen", 0);
        else
            PlayerPrefs.SetInt("Fullscreen", 1);
    }

    public void SetSoundSlider(float volume)
    {
        if (volume <= 0)
        {
            mixer.SetFloat("Sounds", -80);
            PlayerPrefs.SetFloat("SoundVolume", volume);

            return;
        }

        mixer.SetFloat("Sounds", Mathf.Log10((float)volume * 0.01f) * 20);
        PlayerPrefs.SetInt("SoundVolume", Mathf.RoundToInt(volume));
        if (soundPercentage != null)
            soundPercentage.text = Mathf.RoundToInt(volume).ToString() + "%";
    }

    public void SetMusicSlider(float volume)
    {
        if (volume <= 0)
        {
            mixer.SetFloat("Music", -80);
            PlayerPrefs.SetFloat("MusicVolume", volume);
            return;
        }

        mixer.SetFloat("Music", Mathf.Log10((float)volume * 0.01f) * 20);
        PlayerPrefs.SetInt("MusicVolume", Mathf.RoundToInt(volume));
        if (musicPercentage != null)
            musicPercentage.text = Mathf.RoundToInt(volume).ToString() + "%";
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    void OpenCloseWindow()
    {
        if (gameObject.activeInHierarchy)
            SaveSettings();
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    private void OnDestroy()
    {
        PlayerInput.onEscapePressed -= OpenCloseWindow;
    }
}

public enum ScreenResolution
{
    FullHD,
    UltraHD,
    WQHD,
    HDReady,
}
