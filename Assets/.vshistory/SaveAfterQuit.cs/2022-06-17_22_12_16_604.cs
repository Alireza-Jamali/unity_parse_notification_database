using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAfterQuit : MonoBehaviour
{
    [SerializeField] GameObject dis;

    private void Start()
    {
        dis.GetComponent<Text>().text = PlayerPrefs.GetInt("quit", 0);
        PlayerPrefs.SetInt("quit", PlayerPrefs.GetInt("quit", 0) + 1);
    }
}
