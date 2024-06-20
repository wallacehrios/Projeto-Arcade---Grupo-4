using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] audioSource;
    private void Awake()
    {
        instance = this;
    }
}
