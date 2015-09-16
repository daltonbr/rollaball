using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Constants {

	// Tags devem ser adicionadas aqui
	// como constants para evitar erros
	// de digitacao
	public class Tags {
		public const string Player = "Player";
		public const string PickUps = "PickUp";
		public const string GController = "GameController";
		public const string Magnet = "Magnet";
		public const string Bomb = "Bomb";
	}

	// Adicione as fases neste enum
	[System.Serializable]
	public enum Levels {
		None = -1,
		Level01,
		Level02,

		// Deve sempre ser o ultimo
		Max
	}

	// Nomes de cenas normais
	public const string MainMenu = "introScene";
	// Fases
	public static Dictionary<Levels, string> LevelName = new Dictionary<Levels, string> ()
	{
		{ Levels.Level01, "scene1-Dalton" },
		{ Levels.Level02, "Level2" },

		// Fases para valores especiais
		{ Levels.None, "" },
		{ Levels.Max, "" }
	};
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