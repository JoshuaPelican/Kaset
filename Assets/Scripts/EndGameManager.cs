using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    #region Singleton

    public static EndGameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    public RewindTape tape;
    public AudioSource source;
    private float duration = 100;
    public float durationOffset;
    public GameObject levelCompletePanel;
    public Level level;

    private TextMeshProUGUI timerText;

    public bool completed;
    private bool ending;

    private void Start()
    {
        duration = source.clip.length + durationOffset;
        levelCompletePanel.SetActive(false);

        timerText = GameObject.FindGameObjectWithTag("Timer").GetComponent<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (!tape.rewinding)
        {
            duration -= Time.fixedDeltaTime;
            int minutes = Mathf.FloorToInt(duration / 60);
            string seconds = Mathf.RoundToInt(duration % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }

        if (duration <= 0 && ending == false)
        {
            EndGame(true);
            ending = true;
        }
    }

    public void EndGame(bool completedLevel)
    {
        completed = completedLevel;
        ScoreManager.instance.AddScore(0);
        GameObject.FindWithTag("Player").GetComponent<PlayerInput>().enabled = false;

        if (completed)
        {
            ScoreManager.instance.GameEndScoreBonus();
        }

        if (PlayerPrefs.GetInt(level.name + " HighScore") < ScoreManager.instance.totalScore)
        {
            PlayerPrefs.SetInt(level.name + " HighScore", ScoreManager.instance.totalScore);
        }

        levelCompletePanel.SetActive(true);
    }
}
