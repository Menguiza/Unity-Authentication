using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class Authentication : MonoBehaviour
{
    public UnityEvent Logged, Registered;
    
    #region Methods

    public void Login()
    {
        if (!IsValidEmail(UIManager.instance.LOGUserInput.text))
            StartCoroutine(Login(UIManager.instance.LOGUserInput.text, UIManager.instance.LOGPasswordInput.text));
        else
        {
            StartCoroutine(LoginAlt(UIManager.instance.LOGUserInput.text, UIManager.instance.LOGPasswordInput.text));
        }
    }

    public void Register()
    {
        StartCoroutine(Register(UIManager.instance.RegUserInput.text, UIManager.instance.RegEmailInput.text,
            UIManager.instance.RegPasswordInput.text, UIManager.instance.RegConfirmInput.text));
    }
    
    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith(".")) {
            return false;
        }
        else if (!trimmedEmail.EndsWith(".com"))
        {
            if (!trimmedEmail.EndsWith(".edu.co"))
            {
                return false;
            }
        }
        try {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch {
            return false;
        }
    }

    #endregion
    
    #region Coroutines

    IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username.ToUpper());
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(Web.defaultPath + "Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.EndsWith("200"))
                {
                    char delimiter = ',';
                    string[] myArray = www.downloadHandler.text.Split(delimiter);
                    CurrentUserInfo.instance.id = int.Parse(myArray[0]);

                    UIManager.instance.LOGError.text = "";
                    UIManager.instance.LOGSuccess.text = "Login Success!";
                    Logged.Invoke();
                }
                else
                {
                    switch (www.downloadHandler.text)
                    {
                        case "400":
                            UIManager.instance.LOGSuccess.text = "";
                            UIManager.instance.LOGError.text = "Wrong Credentials!";
                            break;
                        case "404":
                            UIManager.instance.LOGSuccess.text = "";
                            UIManager.instance.LOGError.text = "User not found!";
                            break;
                        default:
                            UIManager.instance.LOGSuccess.text = "";
                            UIManager.instance.LOGError.text = "Login Failure!";
                            break;
                    }
                    
                    UIManager.instance.LOGButton.interactable = true;
                }
            }
        }
    }
    
    IEnumerator LoginAlt(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginEmail", email.ToLower());
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(Web.defaultPath + "LoginAlt.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.EndsWith("200"))
                {
                    char delimiter = ',';
                    string[] myArray = www.downloadHandler.text.Split(delimiter);
                    CurrentUserInfo.instance.id = int.Parse(myArray[0]);
                    
                    UIManager.instance.LOGError.text = "";
                    UIManager.instance.LOGSuccess.text = "Login Success!";
                    Logged.Invoke();
                }
                else
                {
                    switch (www.downloadHandler.text)
                    {
                        case "400":
                            UIManager.instance.LOGSuccess.text = "";
                            UIManager.instance.LOGError.text = "Wrong Credentials!";
                            break;
                        case "404":
                            UIManager.instance.LOGSuccess.text = "";
                            UIManager.instance.LOGError.text = "User not found!";
                            break;
                        default:
                            UIManager.instance.LOGSuccess.text = "";
                            UIManager.instance.LOGError.text = "Login Failure!";
                            break;
                    }
                    
                    UIManager.instance.LOGButton.interactable = true;
                }
            }
        }
    }

    IEnumerator Register(string username, string email, string password, string passwordConfirm)
    {
        if (username == "" || username.Length > 50)
        {
            UIManager.instance.RegError.text = "Invalid username.";
            UIManager.instance.RegButton.interactable = true;
        }
        else if (!IsValidEmail(email))
        {
            UIManager.instance.RegError.text = "Invalid email address.";
            UIManager.instance.RegButton.interactable = true;
        }
        else if (password == "" || password.Length > 50)
        {
            UIManager.instance.RegError.text = "Invalid password.";
            UIManager.instance.RegButton.interactable = true;
        }
        else if (passwordConfirm != password)
        {
            UIManager.instance.RegError.text = "Password mismatch.";
            UIManager.instance.RegButton.interactable = true;
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("registerUser", username.ToUpper());
            form.AddField("registerEmail", email.ToLower());
            form.AddField("registerPass", password);

            using (UnityWebRequest www = UnityWebRequest.Post(Web.defaultPath + "Register.php", form))
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
                            UIManager.instance.RegError.text = "";
                            UIManager.instance.RegSuccess.text = "Register Success!";
                            Registered.Invoke();
                            break;
                        case "401":
                            UIManager.instance.RegSuccess.text = ""; 
                            UIManager.instance.RegError.text = "Username already taken!";
                            UIManager.instance.RegButton.interactable = true;
                            break;
                        case "402":
                            UIManager.instance.RegSuccess.text = ""; 
                            UIManager.instance.RegError.text ="Email already in use!";
                            UIManager.instance.RegButton.interactable = true;
                            break;
                        default:
                            UIManager.instance.RegSuccess.text = ""; 
                            UIManager.instance.RegError.text ="Register Failure!";
                            UIManager.instance.RegButton.interactable = true;
                            break;
                    }
                }
            }
        }
    }

    #endregion
}
