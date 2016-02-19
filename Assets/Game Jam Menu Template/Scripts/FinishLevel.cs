using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour {

    public ShowPanels showPanels;                      //Reference to the ShowPanels script used to hide and show UI panels
    private bool isFinished;                            //Boolean to check if the game is finished or not
    private StartOptions startScript;                   //Reference to the StartButton script

    //Awake is called before Start()
    void Awake()
    {
        //Get a component reference to ShowPanels attached to this object, store in showPanels variable
        showPanels = GetComponent<ShowPanels>();
        //Get a component reference to StartButton attached to this object, store in startScript variable
        startScript = GetComponent<StartOptions>();
    }

     
    public void FinishScreen()
    {
        //Set isFinished to true
        isFinished = true;
        //Set time.timescale to 0, this will cause animations and physics to stop updating
        //Time.timeScale = 0.5f;
        //call the ShowPausePanel function of the ShowPanels script
        showPanels.ShowFinishLevelPanel();
    }

    public void NextLevel()
    {
        //Set isFinished to false
        isFinished = false;
        //Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
        //Time.timeScale = 1;
        //call the HidePausePanel function of the ShowPanels script
        showPanels.HideFinishLevelPanel();
    }

    public void ReloadLevelButton()
    {
        isFinished = false;
        showPanels.HideFinishLevelPanel();
        int currentScene = SceneManager.GetActiveScene().buildIndex; //grab the current scene index
        SceneManager.LoadScene(currentScene);   //and reloads that scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
