using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {
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
    void Start()
	{
		//Causes UI object not to be destroyed when loading a new scene. If you want it to be destroyed, destroy it manually via script.
		GameObject[] gc = GameObject.FindGameObjectsWithTag (Constants.Tags.GController);
		if (gc.Length > 1) {
			Debug.LogWarning ("Mais de um objeto com a Tag GameController encontrado. Deve-se existir apenas um.");
		} else if (gc.Length <= 0) {
			Debug.LogError ("Nenhum objeto com a Tag GameController encontrado.");
			return;
		}

		gameController = gc [0].GetComponent<GameController> ();
		if (gameController == null) {
			Debug.LogError("GameController nao encontrado.");
			return;
		}

		DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(gameController.gameObject);
	}

	

}
