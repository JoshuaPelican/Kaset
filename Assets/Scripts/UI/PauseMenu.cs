using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI highestComboText;

    private ScoreManager scoreManager;

    private void OnEnable()
    {
        EndGameManager.instance.gameObject.GetComponent<AudioSource>().Pause();
        scoreText.text = "Current Score\n" + ScoreManager.instance.totalScore;
        highestComboText.text = "Best Combo\n" + ScoreManager.instance.highestCombo;
        Pause();
    }

    private void OnDisable()
    {
        Unpause();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        EndGameManager.instance.gameObject.GetComponent<AudioSource>().Play();
    }

    void Start()
    {
        scoreManager = ScoreManager.instance;
        Level level = EndGameManager.instance.level;
        highscoreText.text = "Highscore\n" + PlayerPrefs.GetInt(level.name + " HighScore");
        scoreText.text = "Current Score\n" + scoreManager.totalScore;
        highestComboText.text = "Best Combo\n" + scoreManager.highestCombo;
    }
}
