using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = "" + PlayerPrefs.GetInt("quit", 0);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("quit", PlayerPrefs.GetInt("quit", 0) + 1);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

}
