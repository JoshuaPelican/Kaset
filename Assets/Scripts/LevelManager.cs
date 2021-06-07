using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Level level;
    public AudioSampler sampler;
    private AudioSource source;
    public SpriteRenderer rend;
    public float delay;

    public GameObject pauseButton;
    public GameObject unpauseButton;

    public GameObject pausePanel;

    void Awake()
    {
        level = LevelLoader.levelToLoad;
        GetComponent<EndGameManager>().level = level;
        sampler.level = level;
        source = GetComponent<AudioSource>();
        source.clip = level.levelMusic;
        GameObject.FindWithTag("Player").GetComponent<Shoot>().weapon = level.weapon;
        rend.sprite = level.skin;
        StartCoroutine("DelayMusic");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pausePanel.activeSelf == false)
            {
                pausePanel.SetActive(true);
                pauseButton.SetActive(false);
                unpauseButton.SetActive(true);
                Camera.main.GetComponent<Animator>().SetTrigger("Pause");
            }
            else
            {
                pausePanel.GetComponent<PauseMenu>().Unpause();
                pausePanel.SetActive(false);
                pauseButton.SetActive(true);
                unpauseButton.SetActive(false);
                Camera.main.GetComponent<Animator>().SetTrigger("Unpause");
            }
        }
    }

    public IEnumerator DelayMusic()
    {
        yield return new WaitForSeconds(delay);
        source.Play();
    }
}
