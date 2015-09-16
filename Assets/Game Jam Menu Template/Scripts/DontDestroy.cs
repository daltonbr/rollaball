using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {
    public GameController gameController;

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
