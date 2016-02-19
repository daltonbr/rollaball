using UnityEngine;
using System.Collections;

public class FinishController : MonoBehaviour {

    public GameObject ui;
    private FinishLevel finishLevel;                //Reference to script on UI GameObject
    private GameController gameController;
    void Awake()
    {
        //Get a reference to GameController -  attached to GameController (empty GameObject)
        gameController = FindObjectOfType<GameController>();  // search for the GameController
        if (gameController == null) // in case of the scene was loaded directly (Editor)
        {
            Debug.Log("GameController not founded! Instantiating one!");
            GameObject _GameController = Instantiate(Resources.Load("prefabs/GameController", typeof(GameObject))) as GameObject;
            gameController = FindObjectOfType<GameController>();
        }
        //Get a reference to ShowPanels attached to UI object
        finishLevel = GameController.Instance.showPanels.GetComponent<FinishLevel>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.Player))
        {
            Debug.Log("Voce entrou no BlackHole");

			// Processa o fim da fase
			GameController.Instance.OnLevelEnd();
            gameController.PlayBlackHoleSound();
            finishLevel.FinishScreen();
        }
        //gameController.showPanels.HideMenu();
    }
}
