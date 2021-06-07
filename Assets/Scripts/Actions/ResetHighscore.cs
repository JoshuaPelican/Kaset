using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHighscore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Level 1 HighScore", 0);
    }
}
