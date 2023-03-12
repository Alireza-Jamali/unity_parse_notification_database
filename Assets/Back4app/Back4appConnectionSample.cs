using UnityEngine;
using System.Collections;
using Parse;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RTLTMPro;
using TMPro;

public class Back4appConnectionSample : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro senderName;
    [SerializeField] RTLTextMeshPro textBody;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject panel;
    Regex regex = new("[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]");
    // private readonly ConcurrentQueue<Action> actions = new();

    AndroidJavaClass unityClass;
    AndroidJavaObject unityActivity;
    AndroidJavaObject notif;
    AndroidJavaObject notifReceiver;

    public static string back4app_server_url = "https://parseapi.back4app.com/";
    public static string firebase_sender_id = "738972861003";

    // public static string back4app_app_id = "0GBQL0a8Qcbt3RuCEtu4JaWGP2OsRgxSPqqzu28t";
    // public static string back4app_client_key = "6EghfU7JkNsydAgj8rAz4QhJT3U8vtWWhdjfHgw2";

    public static string back4app_app_id = "mkVqU6mLkx2NlD2ldO3MoLUibTOBHH3rKax7yBL9";
    public static string back4app_client_key = "SW5nWj7SuTbWQHd4gB8SJOy1OHOTroeT2DLPILS5";

    IEnumerator Start() //get last record every second
    {
        yield return new WaitForSeconds(3);
        var installation = ParseInstallation.CurrentInstallation;

        installation.Channels = new List<string> { "aeza" };
        installation.Add("GCMSenderId", firebase_sender_id);
        installation.SaveAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(task.Exception);
            }
        });
        
        ParsePush.ParsePushNotificationReceived += (sender, args) =>
        {
            AndroidJavaClass parseUnityHelper = new AndroidJavaClass("com.parse.ParseUnityHelper");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("com.unity3d.player.UnityPlayerNativeActivity");

            // Call default behavior.
            parseUnityHelper.CallStatic("handleParsePushNotificationReceived", currentActivity, args.StringPayload);
        };
//--------------------------//for unity local notification, not my use--------------------------
        // var channel = new AndroidNotificationChannel()
        // {
        //     Id = "aeza_chat",
        //     Name = "aeza chat",
        //     Importance = Importance.High,
        //     Description = "for victory, for honor",
        // };
        // AndroidNotificationCenter.RegisterNotificationChannel(channel);
//--------------------------------------------------------------------------


//---------------------------------------for failed attempt on using android only, firebase dependencies won't set deviceToken on old mobiles for unity run apk only, android was ok-----------------------------------
        // unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        // unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        // notif = new AndroidJavaObject("com.aeza.parsenotif.Notif");
        // notifReceiver = new AndroidJavaObject("com.aeza.parsenotif.NotifReceiver");

        // notif.CallStatic("initParse", unityActivity, back4app_app_id, back4app_client_key, back4app_server_url);

        // yield return new WaitForSeconds(5);

        // notif.CallStatic("subscribeToChannel", firebase_sender_id, "aeza");
//--------------------------------------------------------------------------
        // var query = ParseObject.GetQuery("DawnChat");
        // query.OrderByDescending("createdAt").FindAsync().ContinueWith(t =>
        // {
        //     foreach (var po in t.Result)
        //     {
        //         addRow(po.Get<string>("name"), po.Get<string>("msg"));
        //     }
        // });
        // var m_ScrollRect = FindObjectOfType<ScrollRect>();
        // float backup = m_ScrollRect.verticalNormalizedPosition;
        // StartCoroutine(ApplyScrollPosition(m_ScrollRect, 0)); //use back up to scroll a the same amount of added space to bottom
    }

    // IEnumerator RequestNotificationPermission(string name, string title)
    // {
    //     var request = new PermissionRequest();
    //     while (request.Status == PermissionStatus.RequestPending)
    //         yield return null;
    //     if (request.Status == PermissionStatus.Allowed)
    //     {
    //         var notification = new AndroidNotification();
    //         notification.Title = name;
    //         notification.Text = title;
    //         notification.FireTime = DateTime.Now;
    //
    //         AndroidNotificationCenter.SendNotification(notification, "aeza_chat");
    //     }
    // }

    IEnumerator ApplyScrollPosition(ScrollRect sr, float verticalPos)
    {
        yield return new WaitForEndOfFrame();
        sr.verticalNormalizedPosition = verticalPos;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sr.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    void FixedUpdate() //if queue was added, instantiate a row with msg
    {
        // while (actions.Count > 0)
        //     if (actions.TryDequeue(out var action))
        //     {
        //         action?.Invoke();
        //     }
        // Debug.Log("hasMsg: " + notifReceiver.CallStatic<bool>("hasMsg"));
        // if (notifReceiver.CallStatic<bool>("hasMsg"))
        // {
        //     addRow(notifReceiver.GetStatic<string>("title"), notifReceiver.GetStatic<string>("alert"));
        //     notifReceiver.CallStatic("erase");
        // }
    }

    private void addRow(string title, string msg)
    {
        Debug.Log("adding row: " + msg);
        var go = Instantiate(panel, parent.transform);
        var receivedFrom = go.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var receivedTxt = go.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (regex.IsMatch(msg))
        {
            receivedFrom.isRightToLeftText = true;
            receivedTxt.isRightToLeftText = true;
            receivedFrom.alignment = TextAlignmentOptions.TopRight;
            receivedTxt.alignment = TextAlignmentOptions.TopRight;
        }

        receivedFrom.text = title;
        receivedTxt.text = msg;

        // var push = new ParsePush();
        // push.Channels = new List<string> { "aeza" };
        // push.Data = new Dictionary<string, object>
        // {
        //     { "title", title },
        //     { "alert", msg },
        // };
        // push.SendAsync();

        // IDictionary<string, object> param = new Dictionary<string, object>();
        // param.Add("title", title);
        // param.Add("alert", msg);
        // ParseCloud.CallFunctionAsync<IDictionary<string, object>>("push", param).ContinueWith(task =>
        // {
        //     if (task.IsFaulted)
        //     {
        //         Debug.Log(task.Exception);
        //     }
        // });
        // StartCoroutine(RequestNotificationPermission(title, msg));
    }

    public void Click() //send click
    {
        SaveRec();
    }

    public async void SaveRec() //sends data to DB
    {
        ParseObject gameScore = new ParseObject("DawnChat");
        gameScore["name"] = senderName.text;
        gameScore["msg"] = textBody.text;
        // notif.CallStatic("sendMsg", senderName.text, textBody.text, "aeza");
        textBody.GetComponentInParent<TMP_InputField>().text = "";
        await gameScore.SaveAsync();
    }

    public void RightAlignmentListener(String newCharacter)
    {
        textBody.alignment = regex.IsMatch(newCharacter) ? TextAlignmentOptions.TopRight : TextAlignmentOptions.TopLeft;
    }
}

public static class ScrollRectExtensions
{
    public static void ScrollToTop(this ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }

    public static void ScrollToBottom(this ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}