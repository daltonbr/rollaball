using UnityEngine;
using UnityEngine.UI;  // for using Ui Text's
using System.Collections;

public class GameController : MonoBehaviour {
	public static GameController Instance;

    //public float timeIncresase = 2;
    private bool timeElapsed = false;
    private float timeRemaining;
    public Text winText;
    public GameObject ui;
    //    public GameController gameController;

    private int pickUpsRemaining;
    private int pickUpsTotal;
    private int count;

    private GameObject[] pickUps;  // retorna NULL se nao achou nada com a TAG

    //private QuitApplication quitApplication;
    private ShowPanels showPanels;

    void Awake()
    {
        //Get a reference to QuitApplication attached to UI object
        //quitApplication = ui.GetComponent<QuitApplication>();

        //Get a reference to ShowPanels attached to UI object
        GameObject[] pickUps = GameObject.FindGameObjectsWithTag("PickUp") as GameObject[];  // retorna NULL se nao achou nada com a TAG
        pickUpsTotal = pickUps.Length; //conta o total de pickups na fase, de acordo com o vetor pickUps

        showPanels = ui.GetComponent<ShowPanels>();
        //ui = GetComponents<UI>;

		count = 0;
		Instance = this;
    }

    public void Die()
    {
        
        Debug.Log("Morreu!");
        Application.LoadLevel(Application.loadedLevel);  //recarrega o nivel atual
    }


    
    // Chamado quando termina a fase
	public void LoadNext()  
	{
        //Invoke("PlayNewMusic", fadeAlphaAnimationClip.length);
        showPanels.ShowFinishLevelPanel();
        
        //Application.LoadLevel(1); // hardcoded
        //Invoke("")
	}

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            Debug.Log("Scene " + level + " Loaded");
        }

    }

    void Start ()
    {
        UpdatePickUpCount();
        pickUpsTotal = pickUpsRemaining;  // somente no Start;
        winText.text = "";
        // BroadcastMessage("Start Timer", timeRemaining);  // chama o metodo Start Timer, com o parametro timeRemaining
    }

    void OnGUI()
    {
        //GUI.Box(new Rect(0, 0, Screen.width/2, Screen.height/2), "Remaining PickUps: " + pickUpsRemaining + " from " + pickUpsTotal);
    }

    public void UpdatePickUpCount()  // public pq sera acessado por outros scripts
    {
		// TODO : Valor dos PickUps
		count++;
        pickUps = GameObject.FindGameObjectsWithTag("PickUp") as GameObject[];  // retorna NULL se nao achou nada com a TAG
        pickUpsRemaining = pickUps.Length;
        if (pickUpsRemaining == 0)
        {
            winText.text = "You Win!";
			LoadNext();
        }
    }
}
