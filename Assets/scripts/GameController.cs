﻿using UnityEngine;
using UnityEngine.UI;  // for using Ui Text's
using System; // Enum.GetValues
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public static GameController Instance;
	public GameObject UIPrefab;

	// Armazena a fase atual (apenas fases de jogo)
	[HideInInspector] public Constants.Levels currentLevel;

    //public float timeIncresase = 2;
    private bool timeElapsed = false;
    private float timeRemaining;
	//public Text winText;

    private int pickUpsRemaining;

	private int pickUpsTotal, pickUpsGet;
	private int gameTime;
	private int score;
    private int count;

    public AudioClip pickUpSound;
    public AudioClip jumpSound;
    public AudioClip magnetSound;
    public AudioClip blackHoleSound;
    public AudioClip dieSound;
    public AudioClip[] explosionsSound;

    private AudioSource source;

    public float volLowRange = 0.4f;
    public float volHighRange = 1.0f;

    private GameObject[] pickUps;  // retorna NULL se nao achou nada com a TAG

	// Acesso para leitura e publico,
	// escrita e privado.
	public ShowPanels showPanels { get; private set; }

	void Awake()
	{
        source = GetComponent<AudioSource>(); //get the AudioSource from the GameController

        if (UIPrefab == null )
        {
            Debug.Log("UIPrefab not found! Probably loading direct the scene!");

        }
		OnLevelLoad ();
	}

	void OnLevelWasLoaded(int level)
	{
		OnLevelLoad ();
	}

	/// <summary>
	/// Chamada quando uma cena e carregada
	/// Nota: Esta funcao e chamada pelo Awake e OnLevelWasLoaded,
	/// e nao faz parte do Unity
	/// </summary>
	void OnLevelLoad()
    {
		// Gera uma nova UI, isso deve acontecer no menu principal
		if (showPanels == null) {
			GameObject ui = GameObject.Instantiate(UIPrefab);
			showPanels = ui.GetComponent<ShowPanels>();
			showPanels.ShowMenu();
		}

		// Atualiza qual fase o jogo esta
		currentLevel = GetLevelEnum ();
		if (currentLevel != Constants.Levels.None) {
			// retorna NULL se nao achou nada com a TAG
			GameObject[] pickUps = GameObject.FindGameObjectsWithTag (Constants.Tags.PickUps) as GameObject[];
			pickUpsTotal = pickUps.Length; //conta o total de pickups na fase, de acordo com o vetor pickUps
			if (pickUps == null || pickUps.Length == 0) {
				Debug.LogWarning ("Nenhum pickup encontrado");
				return;
			}

			pickUpsTotal = pickUps.Length;
			gameTime = 0;
			score = 0;
			
			pickUpsGet = 0;

			// Ativa o HUD
			//showPanels.ShowGameScreen ();

			// Inicializa um relogio interno
			InvokeRepeating ("GameTimer", 0f, 1f);
		} else {
			// confirma que o HUD esta desativado
			showPanels.HideGameScreen();
		}
        
		count = 0;
		Instance = this;
    }

    public void Die()
    {
        Debug.Log("Morreu!");
        source.PlayOneShot(dieSound, 1F);
        Scene currentSceneIndex = SceneManager.GetActiveScene();  // get the actual scene and reload it
        SceneManager.LoadScene(currentSceneIndex.buildIndex);
    }

	/// <summary>
	/// Adiciona 1 segundo ao tempo de jogo
	/// </summary>
	void GameTimer() {
		// TODO : Se esta pausado nao deve atualizar
		// TODO : possivel exploit se pausar dentro do 1 segundo
		this.gameTime++;
		this.UpdateUI ();
	}

	/// <summary>
	/// Chamado quando o jogador recolhe um PickUp
	/// </summary>
	/// <param name="pickup">Pickup.</param>
	public void OnPickUpGet(GameObject pickup)
	{

        source.PlayOneShot(pickUpSound, 1f);
		// Desabilita o pickup e soma aos pickups recolhidos
		pickup.SetActive (false);
		this.pickUpsGet++;
		
		// Acessa os dados deste PickUp (classe PickUpObject)
		// e adiciona a pontuaçao
		PickUpObject data = pickup.GetComponent<PickUpObject> ();
		this.score += data.value;
		
		// Atualiza a UI
		this.UpdateUI ();
	}

	/// <summary>
	/// Atualiza o HUD
	/// </summary>
	private void UpdateUI()
	{
		showPanels.gameScreen.UpdateGameScreen (this.gameTime, this.pickUpsGet, this.pickUpsTotal, this.score);
	}

    // Chamado quando termina a fase

    public void PlayMagnetSound()
    {
        source.PlayOneShot(magnetSound, 1f);
    }

    public void PlayBlackHoleSound()
    {
        source.PlayOneShot(blackHoleSound, 1f);
    }

    public void PlayBombSound()
    {
        int length = explosionsSound.Length;  //plays a random Explosion sound - according to a sound array in GameController
        int rand = UnityEngine.Random.Range(0, length - 1);
        source.PlayOneShot(explosionsSound[rand], 1f);
    }

    public void PlayJumpSound()
    {
        float vol = UnityEngine.Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(jumpSound, vol);
    }

    public void LoadNext()  
	{
        //Invoke("PlayNewMusic", fadeAlphaAnimationClip.length);
        showPanels.ShowFinishLevelPanel();
	}

    public void UpdatePickUpCount()  // public pq sera acessado por outros scripts
    {
		// TODO : Valor dos PickUps
		/*count++;
        pickUps = GameObject.FindGameObjectsWithTag("PickUp") as GameObject[];  // retorna NULL se nao achou nada com a TAG
        pickUpsRemaining = pickUps.Length;
        if (pickUpsRemaining == 0)
        {
            winText.text = "You Win!";
			LoadNext();
        }*/
    }

	/// <summary>
	/// Chamado quando a fase termina,
	/// faz as atualizacoes do ranking
	/// </summary>
	public void OnLevelEnd()
	{
		// TODO : Adicionar nome do jogador
		
		showPanels.HideGameScreen ();
		IORanking.UpdateRank(currentLevel, "nome", this.gameTime, this.score);
	}

	/* *********************************
	 * 		Metodos Staticos
	 * *********************************/

	/// <summary>
	/// Busca a fase no enum baseado no nome da cena
	/// </summary>
	/// <returns>O enum da fase.</returns>
	public static Constants.Levels GetLevelEnum()
	{
		string levelName = Application.loadedLevelName;
		foreach (Constants.Levels l in Enum.GetValues(typeof(Constants.Levels))){
			if (Constants.LevelName[l] == levelName) {
				return l;
			}
		}
		return Constants.Levels.None;
	}
}
