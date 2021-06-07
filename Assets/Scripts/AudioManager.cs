using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider musicSlider;
    public AudioSource musicTune;
    public Slider effectsSlider;
    public AudioSource effectsTune;

    private void Start()
    {
        mixer.SetFloat("MusicVolume", (PlayerPrefs.GetFloat("MusicVolume", 1) - 1) * 40);
        mixer.SetFloat("EffectsVolume", (PlayerPrefs.GetFloat("EffectsVolume", 1) - 1) * 40);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1);
    }

    public void UpdateMusicAudio(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", (sliderValue - 1) * 40);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        if (!musicTune.isPlaying)
        {
            musicTune.Play();
        }
    }

    public void UpdateEffectsAudio(float sliderValue)
    {
        mixer.SetFloat("EffectsVolume", (sliderValue - 1) * 40);
        PlayerPrefs.SetFloat("EffectsVolume", sliderValue);
        if (!effectsTune.isPlaying)
        {
            effectsTune.Play();
        }
    }
}
