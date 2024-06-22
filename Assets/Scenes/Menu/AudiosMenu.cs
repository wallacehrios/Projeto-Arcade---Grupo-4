using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class AudiosMenu : MonoBehaviour
{
    public AudioSource[] audioSource;
    void Start()
    {
        Test();
    }
    void Test()
    {
        StartCoroutine(MusicaMenu());
    }
    IEnumerator MusicaMenu()
    {
        foreach (AudioSource audio in audioSource)
        {
            audio.Play();
            yield return new WaitForSeconds(audio.clip.length);
        }
    }
}
