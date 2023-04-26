using UnityEngine;
using TMPro;

public class ScoreBoardInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text username, score;

    public TMP_Text Username
    {
        get => username;
        set => username = value;
    }

    public TMP_Text Score
    {
        get => score;
        set => score = value;
    }
}
