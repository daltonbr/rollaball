using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	public static LevelController Instance;

	public Constants.Levels currentLevel;
	public ShowPanels showPanels;

	private int gameTime;
	private int score;
	private int pickUpsTotal, pickUpsGet;

	// Use this for initialization
	void Start () {
		gameTime = 0;
		score = 0;

		pickUpsGet = 0;

		// Conta quantos pickups existem no total
		GameObject[] pickUps = GameObject.FindGameObjectsWithTag(Constants.Tags.PickUps) as GameObject[];
		if (pickUps == null) {
			Debug.LogWarning("Nenhum pickup encontrado");
			return;
		}
		pickUpsTotal = pickUps.Length;

		// Ativa o HUD
		showPanels.ShowGameScreen ();

		InvokeRepeating ("GameTimer", 0f, 1f);

		Instance = this;
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
	/// Chamando quando o jogador recolhe um PickUp
	/// </summary>
	/// <param name="pickup">Pickup.</param>
	public void OnPickUpGet(GameObject pickup)
	{
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

	private void UpdateUI()
	{
		showPanels.gameScreen.UpdateGameScreen (this.gameTime, this.pickUpsGet, this.pickUpsTotal, this.score);
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
}
