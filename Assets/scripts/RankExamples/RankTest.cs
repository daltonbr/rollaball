using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankTest : MonoBehaviour {
	public InputField nome;
	public int score = 0;
	public float gametime = 0;
	void Awake()
	{
		// Isso deve ser feito no menu principal
		// para conferir a existencia dos arquivos
		IORanking.Init ();
	}

	void OnTriggerEnter(Collider other)
	{
		IORanking.UpdateRank(Constants.Levels.Level01, nome.text, gametime, score);
	}
}
