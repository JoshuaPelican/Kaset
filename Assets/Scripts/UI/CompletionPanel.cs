using UnityEngine;
using UnityEngine.UI;

public class CompletionPanel : MonoBehaviour
{
    public Text titleText;
    public Text scoreText;
    public Text highscoreText;
    public Text highestComboText;

    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponentInChildren<LineRenderer>().enabled = false;
        GetComponent<AudioSource>().Play();
        Level level = EndGameManager.instance.level;
        scoreManager = ScoreManager.instance;
        if (EndGameManager.instance.completed)
        {
            titleText.text = "Song Complete!";
        }
        else
        {
            titleText.text = "Song Failed!";
        }
        scoreText.text = "Final Score\n" + scoreManager.totalScore;
        highscoreText.text = "Highscore\n" + PlayerPrefs.GetInt(level.name + " HighScore");
        highestComboText.text = "Best Combo\n" + scoreManager.highestCombo;
    }
}
