using UnityEngine;
using System.Collections;

public class RankingDisplay : MonoBehaviour {

	// Armazena todos os rankings de todas as fases
	RankingData[] rd;

	public Transform displayGroup;
	public GameObject rankEntry;

	// Use this for initialization
	void Start () {
		// Deve ser chamado no inicio do menu principal
		// para confirmar a exitencia de arquivos
		IORanking.Init ();

		rd = IORanking.Load ();
		ShowRank ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowRank()
	{
		//rd[level].Ranks[RankId]
		//for (int i = 0; i < IORanking.Ranking.Max; i++)
		//{
		RankingData.Rank rank = rd[(int)Constants.Levels.Level01].Ranks[(int)IORanking.Ranking.BestScore];
		for (int i = 0; i < RankingData.MaxEntries; i++) {
			GameObject rentry = Instantiate (rankEntry) as GameObject;
			RankEntryObject ro = rentry.GetComponent<RankEntryObject> ();
			ro.Pos.text = ""+(i+1);
			ro.Name.text = rank.Name[i];
			ro.Score.text = rank.Score[i].ToString();
			rentry.transform.SetParent(displayGroup);
		}
		//}
	}
}
