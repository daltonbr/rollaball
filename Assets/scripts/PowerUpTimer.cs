using UnityEngine;
using System.Collections;

public class PowerUpTimer : MonoBehaviour {

    float deltaTime;  //pega do script do PowerUp em questao
    GameObject player;
    private GameController gameController;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if (!player) Debug.Log("GameObject Player nao encontrado");

        gameController = FindObjectOfType<GameController>(); //reference for playing sound
    }

    void OnTriggerEnter(Collider other) //algo colide com o PowerUp
    {
        if (other.gameObject.CompareTag("Player"))  //soh ativa o powerUp qdo o Player o pega
        {
            gameController.PlayMagnetSound();
            deltaTime = player.GetComponent<MagnetPowerUp>().duration;
            player.GetComponent<MagnetPowerUp>().enabled = true;  //habilita o Script Magnet no playerRB
            this.GetComponent<MeshRenderer>().enabled = false;
            player.GetComponent<MagnetPowerUp>().particlesPowerUp.Play();
            StartCoroutine( TimeUp() );   
        }
    }

    IEnumerator TimeUp()  // to WaitForSeconds we need this function to be iEnumerator
    {
        float elapsed = 0;
        while(elapsed < deltaTime)
        {
            yield return null;
            elapsed += Time.deltaTime;
        }
        // yield return new WaitForSeconds(deltaTime);  // time that the PowerUp is active
        Debug.Log("time's up!");
        TurnOffMagnet();
    }

    void TurnOffMagnet()
    {
        player.GetComponent<MagnetPowerUp>().particlesPowerUp.Stop();
        player.GetComponent<MagnetPowerUp>().enabled = false;  //desabilita o Script Magnet no playerRB
        Destroy(this.gameObject);  //destroi o PowerUp
    }
}
