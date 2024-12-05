using Jesper;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI whoWonText;
    private int _scoreTeam1;
    private int _scoreTeam2;

    private static string _saveScore;
    private static bool _team1Won;

    void Start()
    {
        _scoreTeam1 = GameManager.Instance.Team1Score;
        _scoreTeam2 = GameManager.Instance.Team2Score;
        _team1Won = GameManager.Instance.Team1Won;
        _saveScore = _team1Won
            ? GameManager.Instance.Team1Score.ToString()
            : GameManager.Instance.Team2Score.ToString();
    }

    private void Update()
    {
        scoreText.text =  _saveScore;
        whoWonText.text = _team1Won ? "Red Won" : "Blue Won";
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
