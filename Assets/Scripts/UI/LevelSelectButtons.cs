using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtons : MonoBehaviour
{
    public Level level;
    public GameObject panel;
    public Text buttonText;
    public Text highscoreText;

    private void Start()
    {
        buttonText.text = level.cassetteName;
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt(level.name + " HighScore");
        panel.SetActive(false);
    }

    private void OnMouseEnter()
    {
        panel.SetActive(true);
    }

    private void OnMouseExit()
    {
        panel.SetActive(false);
    }
}
