using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teamscore : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI whoWonText;

    public int scoreTeam1;
    public int scoreTeam2;
    public static string saveScore;
    public static int whoWon;
    public static bool EndScene = false;

    void Start() { }

    private void Update()
    {
        if (EndScene)
        {
            ScoreText.text = saveScore;
            if (whoWon == 1)
            {
                whoWonText.text = "Red Won";
            }
            else
                whoWonText.text = "Blue Won";
        }
        else
            ScoreText.text = string.Format("       {0}  -   {1}", scoreTeam1, scoreTeam2);

        if (scoreTeam1 >= 3 && !EndScene)
        {
            whoWon = 1;
            saveScore = ScoreText.text;
            EndScene = true;
            SceneManager.LoadScene(2);
        }
        if (scoreTeam2 >= 3 && !EndScene)
        {
            whoWon = 2;
            saveScore = ScoreText.text;
            EndScene = true;
            SceneManager.LoadScene(2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("FinishTRed"))
        {
            scoreTeam1++;
        }
        if (collision.collider.CompareTag("finish"))
        {
            scoreTeam2++;
        }
    }
}
