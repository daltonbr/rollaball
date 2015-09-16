using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScreen : MonoBehaviour {

	public Text pickupText;
	public Text scoreText;
	public Text timeText; 

	/// <summary>
	/// Atualiza a HUD
	/// </summary>
	/// <param name="time">Tempo de jogo (em segundos)</param>
	/// <param name="pickUpsGet">Pick ups recolhidos.</param>
	/// <param name="pickUpsTotal">Total de pickups.</param>
	public void UpdateGameScreen(int time, int pickUpsGet, int pickUpsTotal, int score)
	{
		pickupText.text = pickUpsGet + " / " + pickUpsTotal;
		timeText.text = time.ToString();
		scoreText.text = score.ToString ();
	}
}
