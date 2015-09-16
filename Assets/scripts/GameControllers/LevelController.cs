using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	public static LevelController Instance;

	public Constants.Levels currentLevel;
	private float gameTime;
	private int score;

	// Use this for initialization
	void Start () {
		gameTime = 0;
		score = 0;

		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		gameTime += Time.deltaTime;
	}

	/// <summary>
	/// Chamando quando o jogador recolhe um PickUp
	/// </summary>
	/// <param name="pickup">Pickup.</param>
	public void OnPickUpGet(GameObject pickup)
	{
		// Desabilita o pickup
		pickup.SetActive (false);

		// Acessa os dados deste PickUp (classe PickUpObject)
		// e adiciona a pontuaçao
		PickUpObject data = pickup.GetComponent<PickUpObject> ();
		this.score += data.value;
	}

	/// <summary>
	/// Chamado quando a fase termina,
	/// faz as atualizacoes do ranking
	/// </summary>
	public void OnLevelEnd()
	{
		// TODO : Adicionar nome do jogador
		IORanking.UpdateRank(currentLevel, "nome", this.gameTime, this.score);
	}
}
