using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private ScoreBoardInfo scoreBoardPrefab;
    [SerializeField] private TMP_Text success, error;
    [SerializeField] private TMP_InputField score;
    
    #region Methods

    public void LoadScoreBoard()
    {
        StartCoroutine(GetUsers());
    }

    public void LoadAsync()
    {
        success.text = "";
        error.text = "";
        ClearChilds(content);
        Invoke("LoadScoreBoard", 1f);
    }

    public void Upload()
    {
        StartCoroutine(UpdateScore(int.Parse(score.text)));
    }
    
    void ClearChilds(RectTransform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion
    
    #region Coroutines

    IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Web.defaultPath + "GetUsers.php"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                ClearChilds(content);
                error.text = "";
                success.text = "";
                
                if (www.downloadHandler.text != "0")
                {
                    string json = www.downloadHandler.text;

                    json = "{\"Items\":" + json + "}";

                    ScoreBoardElement[] array = JsonHelper.FromJson<ScoreBoardElement>(json);
                
                    List<ScoreBoardElement> order = array.OrderByDescending(u => u.score).ToList();

                    foreach (ScoreBoardElement element in order)
                    {
                        ScoreBoardInfo info = Instantiate(scoreBoardPrefab, content);

                        info.Username.text = element.username;
                        info.Score.text = element.score.ToString();
                    }

                    success.text = "Users Loaded!";
                }
                else
                {
                    error.text = "No users found!";
                }
            }
        }
    }

    IEnumerator UpdateScore(int scoreToUpdate)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", CurrentUserInfo.instance.id);
        form.AddField("score", scoreToUpdate);

        using (UnityWebRequest www = UnityWebRequest.Post(Web.defaultPath + "UpdateScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                switch (www.downloadHandler.text)
                {
                    case "200":
                        success.text = "Score Update Success!";
                        yield return new WaitForSeconds(1f);
                        StartCoroutine(GetUsers());
                        break;
                    case "0":
                        error.text = "Error while updating score!";
                        break;
                }
            }
        }
    }

    #endregion
}

[Serializable]
public class ScoreBoardElement
{
    public string username;
    public int score;
}
