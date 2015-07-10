using UnityEngine;
using UnityEngine.UI;  // for using Ui Text's
using System.Collections;

public class GameController : MonoBehaviour {

    //public float timeIncresase = 2;
    public bool timeElapsed = false;
    private int pickUpsRemaining;
    private int pickUpsTotal;
    public float timeRemaining;

    public Text winText;
    
    private int count;

    //private QuitApplication quitApplication;
    private ShowPanels showPanels;
    
    public GameObject ui;

    public GameController gameController;

    void Awake ()
    {
        //Get a reference to QuitApplication attached to UI object
        //quitApplication = ui.GetComponent<QuitApplication>();

        //Get a reference to ShowPanels attached to UI object
        showPanels = ui.GetComponent<ShowPanels>();

        //ui = GetComponents<UI>;

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
            gameController = GetComponent<GameController>();
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
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Remaining PickUps: " + pickUpsRemaining + " from " + pickUpsTotal);
    }

    public void UpdatePickUpCount()  // public pq sera acessado por outros scripts
    {
        GameObject[] pickUps = GameObject.FindGameObjectsWithTag("PickUp") as GameObject[];  // retorna NULL se nao achou nada com a TAG
        pickUpsRemaining = pickUps.Length;
        if (pickUpsRemaining == 0)
        {
            winText.text = "You Win!";
			LoadNext();
        }
    }  
}
