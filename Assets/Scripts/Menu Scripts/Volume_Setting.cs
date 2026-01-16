using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume_Setting : MonoBehaviour
{
    [SerializeField] private AudioMixer Music_Mixer;
    [SerializeField] private Slider Music_Slider;

    public void Set_Music_Volume()
    {
        float volume = Music_Slider.value;
        Music_Mixer.SetFloat("Music", volume);
    }
   
}
