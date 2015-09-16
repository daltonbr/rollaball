using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Confirma que os arquivos de ranking existem
		IORanking.Init ();
	}
}
