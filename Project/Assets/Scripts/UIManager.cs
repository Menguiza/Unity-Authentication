using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField logUserInput, logPasswordInput;
    [SerializeField] private TMP_Text logError, logSuccess;
    
    [SerializeField] private TMP_InputField regUserInput, regEmailInput, regPasswordInput, regConfirmInput;
    [SerializeField] private TMP_Text regError, regSuccess;

    [SerializeField] private GameObject log, reg, score;

    [SerializeField] private Button logButton, regButton;

    public static UIManager instance;
    
    #region Accesores

    public TMP_InputField LOGUserInput => logUserInput;

    public TMP_InputField LOGPasswordInput => logPasswordInput;

    public TMP_Text LOGError => logError;

    public TMP_Text LOGSuccess => logSuccess;

    public TMP_InputField RegUserInput => regUserInput;

    public TMP_InputField RegEmailInput => regEmailInput;

    public TMP_InputField RegPasswordInput => regPasswordInput;

    public TMP_InputField RegConfirmInput => regConfirmInput;

    public TMP_Text RegError => regError;

    public TMP_Text RegSuccess => regSuccess;
    
    public Button LOGButton => logButton;

    public Button RegButton => regButton;

    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void ScoreBoard()
    {
        logUserInput.text = "";
        logPasswordInput.text = "";
        logError.text = "";
        logSuccess.text = "";
        
        score.SetActive(true);
        log.SetActive(false);
    }
    
    private void ClearLogin()
    {
        logUserInput.text = "";
        logPasswordInput.text = "";
        logError.text = "";
        logSuccess.text = "";
        
        reg.SetActive(true);
        log.SetActive(false);

        regButton.interactable = true;
    }
    
    public void ClearRegister()
    {
        regUserInput.text = "";
        regEmailInput.text = "";
        regPasswordInput.text = "";
        regConfirmInput.text = "";
        regError.text = "";
        regSuccess.text = "";
        
        reg.SetActive(false);
        score.SetActive(false);
        log.SetActive(true);
        
        logButton.interactable = true;
    }

    public void LogToReg()
    {
        ClearLogin();
    }

    public void RegToLog()
    {
        Invoke("ClearRegister", 1f);
    }
    
    public void LogToScore()
    {
        Invoke("ScoreBoard", 1f);
    }

    public void ScoreToLog()
    {
        ClearRegister();
    }
}
