using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume_Setting : MonoBehaviour
{
    [SerializeField] private AudioMixer Main_Mixer;
    [SerializeField] private Slider Music_Slider, SFX_Slider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Music_Volume"))
        {
            LoadVolume();
        }
        else
        {
           Set_Music_Volume();
            Set_SFX_Volume();
        }

    }
    public void Set_Music_Volume()
    {
        float volume = Music_Slider.value;
        Main_Mixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("Music_Volume", volume);
    }

    public void Set_SFX_Volume()
    {
        float volume = SFX_Slider.value;
        Main_Mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }

    private void LoadVolume()
    {        
        Music_Slider.value=PlayerPrefs.GetFloat("Music_Volume");
        SFX_Slider.value = PlayerPrefs.GetFloat("SFX_Volume");

        Set_Music_Volume();
        Set_SFX_Volume();
    }
}
