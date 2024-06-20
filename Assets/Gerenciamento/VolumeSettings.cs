using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider BarraDeMusica;
    [SerializeField] private Slider VolumeDoJogo;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicaMenuVolume"))
        {
            LoadVolume();
        }
        else
        {
            VolumeMusica();
            VolumeJogo();
        }
    }

    public void VolumeMusica()
    {
        float volume = BarraDeMusica.value;
        mixer.SetFloat("MusicaJogo", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicaJogoVolume", volume);
    }
     public void VolumeJogo()
    {
        float volume = VolumeDoJogo.value;
        mixer.SetFloat("Jogo", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("JogoVolume", volume);
    }
    private void LoadVolume()
    {
        BarraDeMusica.value = PlayerPrefs.GetFloat("MusicaJogoVolume");
        VolumeDoJogo.value = PlayerPrefs.GetFloat("JogoVolume");
        VolumeMusica();
        VolumeJogo();
    }
}
