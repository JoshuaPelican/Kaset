using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject levelSelectScreen;

    public Animator fadePanel;

    public EventSystem eventSystem;
    private GameObject selected;

    public Button rewindButton;
    public Button fireButton;
    public Animator joystickAnim;

    float c = 0;

    public RectTransform viewPort;

    public GameObject levelButtonContainer;
    private List<Button> levelButtons = new List<Button>();

    public LevelLoader loader;

    private void Start()
    {
        //Get all levels and thier respective buttons
        Button[] tempButtons = levelButtonContainer.GetComponentsInChildren<Button>();
        foreach(Button button in tempButtons)
        {
            levelButtons.Add(button);
        }
    }

    private void Update()
    {
        //Keeps track of last selected object
        if(eventSystem.currentSelectedGameObject != null) 
        {
            selected = eventSystem.currentSelectedGameObject;
        }
        //Prevents object deselection
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            eventSystem.SetSelectedGameObject(selected);
        }
        //Rewind button keyboard version
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RewindButton();
        }
        //Fire button keyboard version
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FireButton();
        }
        //Joystick keyboard version
        float horiz = Input.GetAxisRaw("Horizontal");

        if (horiz != 0)
        {
            if (horiz > 0)
            {
                joystickAnim.SetFloat("Direction", horiz);
                StopCoroutine("JoystickReturn");
                StartCoroutine("JoystickReturn");
                Joystick(.5f);
            }
            else if (horiz < 0)
            {
                joystickAnim.SetFloat("Direction", horiz);
                StopCoroutine("JoystickReturn");
                StartCoroutine("JoystickReturn");
                Joystick(-.5f);
            }
        }

        //Delay on joystick
        c -= 1 * Time.deltaTime;

    }

    //Rewind button performs unique actions depending on what screen is currently on
    public void RewindButton()
    {
        //Play Animation of button
        StartCoroutine("ButtonPress", rewindButton);

        if (mainMenuScreen.activeSelf)
        {
            //Using Animation
        }
        else if (levelSelectScreen.activeSelf)
        {
            //Activate level button
            if (selected.GetComponent<LevelButton>())
            {
                loader.LoadLevel(selected.GetComponent<LevelButton>());
                Camera.main.GetComponent<Animator>().SetTrigger("StartGame");
                fadePanel.SetTrigger("End");
            }
        }
    }

    //Fire button performs unique actions depending on what screen is currently on
    public void FireButton()
    {
        //Play Animation of button
        StartCoroutine("ButtonPress", fireButton);

        if (mainMenuScreen.activeSelf)
        {
            //Close Game
            fadePanel.SetTrigger("Quit");

        }
        else if (levelSelectScreen.activeSelf)
        {
            //Open main menu screen
            mainMenuScreen.SetActive(true);
            levelSelectScreen.SetActive(false);
            viewPort.localPosition = new Vector3(176.8461f, viewPort.localPosition.y, 0);
        }
    }

    public void Joystick(float direction)
    {
        //If delay is up
        if (c <= 0)
        {
            //Determines what screen is up and how the joystick was moved to determine which button to select
            if (levelSelectScreen.activeSelf && !mainMenuScreen.activeSelf)
            {
                if (direction >= .5f && levelButtons.IndexOf(selected.GetComponent<Button>()) < levelButtons.Count - 1)
                {
                    levelButtons[levelButtons.IndexOf(selected.GetComponent<Button>()) + 1].Select();
                    //Broke for some reason
                    viewPort.localPosition = viewPort.transform.localPosition - new Vector3(300, 0, 0);
                    //
                    c = .5f;
                }
                else if (direction <= -.5f && levelButtons.IndexOf(selected.GetComponent<Button>()) > 0)
                {
                    levelButtons[levelButtons.IndexOf(selected.GetComponent<Button>()) - 1].Select();
                    //Broke
                    viewPort.localPosition = viewPort.transform.localPosition + new Vector3(300, 0, 0);
                    //
                    c = .5f;
                }
            }
        }      
    }

    public void TitleFlashEnd()
    {
        //Open level select and set selection
        mainMenuScreen.SetActive(false);
        levelSelectScreen.SetActive(true);

        levelButtons[0].Select();
    }

    private IEnumerator ButtonPress(Button button)
    {
        Image image = button.GetComponent<Image>();
        image.sprite = button.spriteState.pressedSprite;
        yield return new WaitForSeconds(.2f);
        image.sprite = button.spriteState.selectedSprite;

    }

    private IEnumerator JoystickReturn()
    {
        yield return new WaitForSeconds(.05f);
        joystickAnim.SetFloat("Direction", 0.0f);
    }
}
