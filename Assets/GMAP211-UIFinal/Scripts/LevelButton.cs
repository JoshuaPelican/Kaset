using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Level level;

    public TextMeshProUGUI songName;
    public TextMeshProUGUI songDuration;
    public TextMeshProUGUI difficulty;
    public TextMeshProUGUI highscore;
    public Image weaponIcon;

    void Start()
    {
        songName.text = level.cassetteName;

        int minutes = Mathf.FloorToInt(level.levelMusic.length/60);
        float seconds = Mathf.CeilToInt(level.levelMusic.length - (minutes * 60));

        songDuration.text = "Duration: " + minutes + ":" + seconds;

        string diffString = "";
        for (int i = 0; i < level.difficulty; i++)
        {
            diffString += "* ";
        }

        difficulty.text = "Difficulty: " + diffString;

        string format = "00000000";
        highscore.text = "Highscore: " + PlayerPrefs.GetInt(level.name + " HighScore").ToString(format);

        weaponIcon.sprite = level.weaponIcon;
    }
}
