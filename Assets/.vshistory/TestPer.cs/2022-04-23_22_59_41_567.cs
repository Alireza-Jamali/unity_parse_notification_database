using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPer : MonoBehaviour
{
    private void Start()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
        GetComponent<Text>().text = "" + PlayerPrefs.GetInt("quit", 0);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("quit", PlayerPrefs.GetInt("quit", 0) + 1);
    }


}
