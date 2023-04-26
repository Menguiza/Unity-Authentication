using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CurrentUserInfo : MonoBehaviour
{
    public static CurrentUserInfo instance;
    public int id { get; set; }

    private void Awake()
    {
        instance = this;
        StartCoroutine(SetConnection());
    }
    
    IEnumerator SetConnection()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Web.defaultPath + "ConnectionSettings.php"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                print("Connected!");
            }
        }
    }
}
