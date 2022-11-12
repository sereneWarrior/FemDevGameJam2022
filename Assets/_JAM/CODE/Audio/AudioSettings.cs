using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider, soundSlider;
    [SerializeField] private TextMeshProUGUI musicPercentage, soundPercentage; 

    private void Start()
    {
        if(PlayerPrefs.HasKey("MusicVolume"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("SoundVolume"))
            soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");

        if(soundPercentage != null)
            soundPercentage.text = Mathf.RoundToInt((soundSlider.value * 100)).ToString() + "%";
        if(musicPercentage != null)
            musicPercentage.text = Mathf.RoundToInt((musicSlider.value * 100)).ToString() + "%";
    }

    public void SetSoundSlider(float volume)
    {
        if (volume <= 0)
        {
            mixer.SetFloat("sounds", -80);
            PlayerPrefs.SetFloat("SoundVolume", volume);

            return;
        }

        mixer.SetFloat("sounds", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SoundVolume", volume);
        if (soundPercentage != null)
            soundPercentage.text = Mathf.RoundToInt((volume*100)).ToString() + "%";
    }

    public void SetMusicSlider(float volume)
    {
        if(volume <= 0)
        {
            mixer.SetFloat("music", -80);
            PlayerPrefs.SetFloat("MusicVolume", volume);
            return;
        }

        mixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        if (musicPercentage != null)
            musicPercentage.text = Mathf.RoundToInt((volume * 100)).ToString() + "%";
    }
}
