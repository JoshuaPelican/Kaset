using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region Singleton

    public static ScoreManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    public int totalScore;
    private int comboScore;
    private int addCount;
    private bool inCombo;
    private bool addingScore;

    private int scoreMulti;
    public int highestCombo = 0;

    public int scoreDigits;
    public float comboDelay;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI multiText;
    public Image comboTimerBar;

    private void Start()
    {
        scoreText.text = "";
        for (int i = 0; i < scoreDigits; i++)
        {
            scoreText.text += "0";
        }
        comboText.text = "";
        multiText.text = "";
        comboTimerBar.fillAmount = 0;
    }

    public void AddScore(int score)
    {
        if (!inCombo)
        {
            if (addingScore)
            {
                StopCoroutine("DisplayScore");
                totalScore += comboScore;
                addingScore = false;
            }

            if(addCount > highestCombo)
            {
                highestCombo = addCount;
            }

            comboScore = 0;
            addCount = 0;

            StartCoroutine("ScoreCombo");

            StartCoroutine("ComboTimer");
        }

        if(score == 0)
        {
            StopCoroutine("DisplayScore");
            totalScore += comboScore;
            addingScore = false;
        }

        StopCoroutine("ComboTimer");
        StartCoroutine("ComboTimer");

        comboScore += score;
        comboText.text = comboScore.ToString();
        addCount++;

        scoreMulti = Mathf.RoundToInt(Mathf.Sqrt(addCount));
        multiText.text = "X" + scoreMulti;

        StopCoroutine("ScoreCombo");
        StartCoroutine("ScoreCombo");
    }

    public IEnumerator ScoreCombo()
    {
        UpdateDisplay();

        inCombo = true;

        yield return new WaitForSeconds(comboDelay);
        comboScore = comboScore * scoreMulti;

        StartCoroutine("DisplayScore");

        scoreMulti = 1;
        multiText.text = "";
        inCombo = false;
    }

    public IEnumerator DisplayScore()
    {
        addingScore = true;
        int oldCombo = comboScore;
        for (int i = 0; i < oldCombo/10; i++)
        {
            totalScore += 10;
            comboScore -= 10;
            UpdateDisplay();
            yield return new WaitForSeconds(.00001f);
        }
        comboText.text = "";
        addingScore = false;
    }

    public IEnumerator ComboTimer()
    {
        float timeLeft = comboDelay;
        while(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            comboTimerBar.fillAmount = timeLeft/comboDelay;
            yield return new WaitForSeconds(.001f);
        }

        yield return null;
    }

    public void UpdateDisplay()
    {
        string _totalScore = "";
        for (int i = 0; i < scoreDigits - totalScore.ToString().Length; i++)
        {
            _totalScore += "0";
        }
        scoreText.text = _totalScore + totalScore.ToString();
        comboText.text = "+ " + comboScore;
    }

    public void GameEndScoreBonus()
    {
        float remainingHealth = GameObject.FindWithTag("Player").GetComponent<Health>().currentHealth;
        totalScore += ((int)remainingHealth * 100) + 10000;
    }
}
