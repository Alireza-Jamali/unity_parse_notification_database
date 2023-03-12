using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAfterQuit : MonoBehaviour
{
    [SerializeField] GameObject dis;

    private void Start()
    {
        PlayerPrefs.SetInt("quit", PlayerPrefs.GetInt("quit", 0) + 1);
    }
}
