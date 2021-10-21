using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class MenuHandler : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    public GameObject nextLevelOpener;
    public static bool nextLevelSelection;
    
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI replayText;
    public TextMeshProUGUI quitText;
    public TextMeshProUGUI levelSelectionTitleText;


    [SerializeField]PlayerInput playerInput;

    int hitCountOnThisLevelStart;
    int thisLevelScore;

    SpinningHazards inputActions;
    private void Awake()
    {
        inputActions = new SpinningHazards();
    }

    private void OnEnable()
    {
        inputActions.UI.Submit.performed += ProcessSubmitInput;
        inputActions.UI.PauseQuit.performed += ProcessPauseQuitInput;
        inputActions.UI.Replay.performed += ProcessReplayInput;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Submit.performed -= ProcessSubmitInput;
        inputActions.UI.PauseQuit.performed -= ProcessPauseQuitInput;
        inputActions.UI.Replay.performed -= ProcessReplayInput;
        inputActions.Disable();
    }


    void Start()
    {
        

        HideCursor();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Score.hitCount = 0;
        }

        hitCountOnThisLevelStart = Score.hitCount;
    }

    void HideCursor()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            Cursor.visible = true;
        }
        Cursor.visible = false;
    }

    void ProcessSubmitInput(InputAction.CallbackContext ctxt)
    {
        if (nextLevelSelection)
        {
            NextLevel();
        }
    }

    void ProcessPauseQuitInput(InputAction.CallbackContext ctxt)
    {
        if (!nextLevelSelection)
        {
            if (isPaused)
            {
                ResumeTheGame();
            }
            else PauseTheGame();
        }
        else QuitTheGame();
    }

    void ProcessReplayInput(InputAction.CallbackContext ctxt)
    {
        if (nextLevelSelection)        
        {
            Replay();
        }
    }

    void Update()
    {

        if (isPaused || nextLevelSelection)
        {

            Cursor.visible = true;
        }
        else HideCursor();

        CalculateScoreOnThisLevel();

        if (nextLevelSelection)
        {
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 2)
            {

                nextLevelText.text = "SCORE";
            }
            else
            {
                nextLevelText.text = "Next level";

            }

            replayText.text = "Replay";
            quitText.text = "Quit";
            nextLevelText.fontSize = 18;
            replayText.fontSize = 18;
            quitText.fontSize = 18;


        }        
    }



    void PauseTheGame()
    {
        playerInput?.DisableInput();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;       

    }

    public void ResumeTheGame()
    {
        playerInput?.EnableInput();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitTheGame()
    {

        //Application.Quit();
        Screen.fullScreen = false;
        Application.OpenURL("https://veniaminvigovsky.itch.io/");
    }


    public void NextLevel()
    {
        Time.timeScale = 1f;
        playerInput?.EnableInput();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        nextLevelOpener.SetActive(false);
        nextLevelSelection = false;
    }

    public void Replay()
    {
        playerInput?.EnableInput();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        nextLevelOpener.SetActive(false);
        nextLevelSelection = false;
        Score.hitCount = hitCountOnThisLevelStart;
    }

    private void CalculateScoreOnThisLevel()
    {
        thisLevelScore = Score.hitCount - hitCountOnThisLevelStart;
    }


}
