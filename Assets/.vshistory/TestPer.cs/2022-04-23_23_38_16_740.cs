using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPer : MonoBehaviour
{
    
    private void OnApplicationQuit()
    {
        Debug.Log($"quit");
    }

    private void OnApplicationFocus(bool focus)
    {
        Debug.Log($"quit");
        
    }
    private void OnApplicationPause(bool pause)
    {
        
        Debug.Log($"quit");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

}
