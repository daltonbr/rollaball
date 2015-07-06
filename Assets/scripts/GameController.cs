using UnityEngine;
using UnityEngine.UI;  // for using Ui Text's
using System.Collections;

public class GameController : MonoBehaviour {

    //public float timeIncresase = 2;
    public bool timeElapsed = false;
    private int pickUpsRemaining;
    private int pickUpsTotal;
    public float timeRemaining;

    public Text countText;
    public Text winText;
    
    private int count;

    void Start ()
    {
        UpdatePickUpCount();
        pickUpsTotal = pickUpsRemaining;  // somente no no Start;
        winText.text = "";
        // BroadcastMessage("Start Timer", timeRemaining);  // chama o metodo Start Timer, com o parametro timeRemaining
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Remaining PickUps: " + pickUpsRemaining);
    }

    public void UpdatePickUpCount()  // public pq sera acessado por outros scripts
    {
        GameObject[] pickUps = GameObject.FindGameObjectsWithTag("PickUp") as GameObject[];  // retorna NULL se nao achou nada com a TAG
        pickUpsRemaining = pickUps.Length;
        if (pickUpsRemaining == 0)
        {
            winText.text = "You Win!";
        }
        SetCountText();
    }
    void SetCountText()   //atualiza a contagem de pontos no UI
    {
        countText.text = "Total PickUps: " + pickUpsTotal;
    }
    
}
