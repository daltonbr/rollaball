using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	public GameController gameController;

    void Awake()
    {
        //Get a reference to GameController -  attached to GameController (empty GameObject)
        gameController = FindObjectOfType<GameController>();  // search for the GameController
        if (gameController == null) // in case of the scene was loaded directly (Editor)
        {
            Debug.Log("GameController not founded! Instantiating one!");
            GameObject _GameController = Instantiate(Resources.Load("prefabs/GameController", typeof(GameObject))) as GameObject;
            gameController = FindObjectOfType<GameController>();
            gameController.showPanels.HideMenu();
        }
    }
    //If the player gets out of the boundary...
    void OnTriggerExit(Collider other){
		if (other.tag=="Player"){
			//Die function is called.
			gameController.Die();
		}
	}
}
