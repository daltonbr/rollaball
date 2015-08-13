using UnityEngine;
using System.Collections;

public class Constants {
	public class Tags {
		public const string PickUps = "Pick Up";
		public const string SC = "Status Change";
		public const string GController = "GameController";
	}

	public const string MainMenu = "MainMenu";
	public const string LevelSelect = "LevelSelector";
	public const string Shop = "Shop";

	public enum Levels {
		Level01 = 0,
		Level02 = 1,
		Level03 = 2,

		Max // Deve sempre ser o ultimo
	}
}

public class RankingData
{
	public const int MaxEntries = 10;
	
	public class Rank {
		public string[] Name = new string[MaxEntries];
		public float[] Score = new float[MaxEntries];
	}
	
	public Rank[] Ranks { get; set; }

	public RankingData()
	{
		this.Ranks = new Rank[(int)IORanking.Ranking.Max];
		for (int i = 0; i < (int)IORanking.Ranking.Max; i++)
		{
			this.Ranks[i] = new Rank();
		}
	}
}