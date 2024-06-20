using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolhas : MonoBehaviour
{
    public static Bolhas instance;
    public GameObject[] bolhas;
    private void Awake()
    {
        instance = this;
    }
}
