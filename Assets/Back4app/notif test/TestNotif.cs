using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNotif : MonoBehaviour
{
    AndroidJavaClass unityClass;
    AndroidJavaObject unityActivity;
    AndroidJavaObject pluginInstance;
    
    public static string back4app_server_url = "https://parseapi.back4app.com/";
    public static string firebase_sender_id = "738972861003";

    // public static string back4app_app_id = "0GBQL0a8Qcbt3RuCEtu4JaWGP2OsRgxSPqqzu28t";
    // public static string back4app_client_key = "6EghfU7JkNsydAgj8rAz4QhJT3U8vtWWhdjfHgw2";

    public static string back4app_app_id = "mkVqU6mLkx2NlD2ldO3MoLUibTOBHH3rKax7yBL9";
    public static string back4app_client_key = "SW5nWj7SuTbWQHd4gB8SJOy1OHOTroeT2DLPILS5";

    IEnumerator Start()
    {
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        pluginInstance = new AndroidJavaObject("com.aeza.parsenotif.Notif");
        
        pluginInstance.CallStatic("initParse", unityActivity, back4app_app_id, back4app_client_key, back4app_server_url);
        
        yield return new WaitForSeconds(5);
        
        pluginInstance.CallStatic("subscribeToChannel", firebase_sender_id);
        
        yield return new WaitForSeconds(10);
        
        pluginInstance.CallStatic("sendMsg", "ali", "i'm a billionair, child");
        
    }
}
