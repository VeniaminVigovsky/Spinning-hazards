using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class MenuSceneScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    SpinningHazards inputActions;

    [SerializeField] PlayerInput playerInput;
    private void Awake()
    {
        inputActions = new SpinningHazards();
        
    }

    private void OnEnable()
    {
        inputActions.UI.Submit.performed += ProcessSubmitInput;
        inputActions.UI.PauseQuit.performed += ProcessPauseQuitInput;
        
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Submit.performed -= ProcessSubmitInput;
        inputActions.UI.PauseQuit.performed -= ProcessPauseQuitInput;
        
        inputActions.Disable();
    }

    void ProcessSubmitInput(InputAction.CallbackContext ctxt)
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {

            ReplayTheGame();

        }
        else LoadFirstScene();
    }
    void ProcessPauseQuitInput(InputAction.CallbackContext ctxt)
    {
        Quit();
    }
    private void Start()
    {
        playerInput?.DisableInput();
        Cursor.visible = true;

        if (scoreText != null & SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {

            if (Score.hitCount == 0)
            {
                scoreText.text = $"You didn't die EVEN ONCE!" + $"\n" + $"\nYour score: 100/100!";

                scoreText.fontSize = 21;

            }

            else if (Score.hitCount == 1)
            {
                scoreText.text = $"You DIED: {Score.hitCount} time" + $"\n" + $"\nYour score: {Score.CalculateScore()}/100!";

                scoreText.fontSize = 21;

            }
            else if (Score.hitCount >= 25)
            {

                scoreText.text = $"You Died: {Score.hitCount} times" + $"\n" + "\nYour score: 0/100!" + $"\n" + $"\nLet's try again!";

                scoreText.fontSize = 21;
            }

            else
            {
                scoreText.text = $"You Died: {Score.hitCount} times" + $"\n" + $"\nYour SCORE: {Score.CalculateScore()}/100";

                scoreText.fontSize = 21;

            }





        }
        else return;
    }


    public void LoadFirstScene()
    {

        SceneManager.LoadScene(1);

    }

    public void Quit()
    {
        //Application.Quit();
        Application.OpenURL("about:blank");

    }


    public void ReplayTheGame()
    {
        SceneManager.LoadScene(1);
        Score.hitCount = 0;
        

    }
}
