using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public static class IORanking
{
	/// <summary>
	/// Versao do formato de arquivo
	/// -> eh usado para determinar se
	///    o arquivo deve passar por 
	///    conversoes antes de ser
	///    carregado.
	/// </summary>
	public const int FileVersion = 1;

	public enum Ranking : int
	{
		BestScore = 0,
		BestTime = 1,
		BestScoreTime = 2,
		
		Max // Deve sempre ser o ultimo
	}

	public static void Init()
	{
		if (!Directory.Exists("Data/"))
			Directory.CreateDirectory("Data/");
		
		// SE o arquivo de rankings nao existe, cria um novo
		if (!File.Exists ("Data/ranking.dat")) {
			Save(null);
		}
		
		int ver = 0;
		
		using (BinaryReader br = new BinaryReader(File.OpenRead("Data/ranking.dat"))) {
			ver = br.ReadInt32();
			br.Close();
		}
		
		if (ver != FileVersion)
			Convert(ver);
	}

	/// <summary>
	/// Carrega o arquivo de ranking (Data/ranking.dat)
	/// </summary>
	public static RankingData[] Load()
	{
		RankingData[] rd = new RankingData[(int)Constants.Levels.Max];

		using (BinaryReader br = new BinaryReader(File.OpenRead("Data/ranking.dat")))
		{
			int ver = br.ReadInt32();
			if (ver != FileVersion) {
				Debug.Log("Trying to load an outdated file version.");
				return null;
			}

			// levelRank[LevelCount]
			for (int level = 0; level < (int)Constants.Levels.Max; level++)
			{
				rd[level] = new RankingData();

				// Score Entries [MaxEntries]
				for (int i = 0; i < RankingData.MaxEntries; i++)
				{
					rd[level].Ranks[(int)Ranking.BestScore].Name[i] = StringTool.GetString(br.ReadBytes(20));
					rd[level].Ranks[(int)Ranking.BestScore].Score[i] = br.ReadSingle();
				}

				// Time Entries [MaxEntries]
				for (int i = 0; i < RankingData.MaxEntries; i++)
				{
					rd[level].Ranks[(int)Ranking.BestTime].Name[i] = StringTool.GetString(br.ReadBytes(20));
					rd[level].Ranks[(int)Ranking.BestTime].Score[i] = br.ReadSingle();
				}

				// ScoreTime Entries [MaxEntries]
				for (int i = 0; i < RankingData.MaxEntries; i++)
				{
					rd[level].Ranks[(int)Ranking.BestScoreTime].Name[i] = StringTool.GetString(br.ReadBytes(20));
					rd[level].Ranks[(int)Ranking.BestScoreTime].Score[i] = br.ReadSingle();
				}
			}

			br.Close();
		}

		return rd;
	}

	static RankingData LoadLevel (Constants.Levels level)
	{
		RankingData rd = new RankingData ();

		using (BinaryReader br = new BinaryReader(File.OpenRead("Data/ranking.dat")))
		{
			int ver = br.ReadInt32();
			if (ver != FileVersion) {
				Debug.Log("Trying to load an outdated file version.");
				return null;
			}
			
			br.ReadBytes((int)level * 24);

			// Score Entries [MaxEntries]
			for (int i = 0; i < RankingData.MaxEntries; i++)
			{
				rd.Ranks[(int)Ranking.BestScore].Name[i] = StringTool.GetString(br.ReadBytes(20));
				rd.Ranks[(int)Ranking.BestScore].Score[i] = br.ReadSingle();
			}
			
			// Time Entries [MaxEntries]
			for (int i = 0; i < RankingData.MaxEntries; i++)
			{
				rd.Ranks[(int)Ranking.BestTime].Name[i] = StringTool.GetString(br.ReadBytes(20));
				rd.Ranks[(int)Ranking.BestTime].Score[i] = br.ReadSingle();
			}
			
			// ScoreTime Entries [MaxEntries]
			for (int i = 0; i < RankingData.MaxEntries; i++)
			{
				rd.Ranks[(int)Ranking.BestScoreTime].Name[i] = StringTool.GetString(br.ReadBytes(20));
				rd.Ranks[(int)Ranking.BestScoreTime].Score[i] = br.ReadSingle();
			}
			
			br.Close();
		}

		return rd;
	}

	public static void UpdateRank(Constants.Levels level, string playerName, float gameTime, int score)
	{
		RankingData rd = LoadLevel (level);
		bool needSave = false;
		int curRank = RankingData.MaxEntries-1;
		int newRank = RankingData.MaxEntries;

		RankingData.Rank bestScore = rd.Ranks[(int)Ranking.BestScore];
		RankingData.Rank bestTime = rd.Ranks[(int)Ranking.BestTime];
		RankingData.Rank bestScoreTime = rd.Ranks[(int)Ranking.BestScoreTime];

		for (int i = RankingData.MaxEntries -1; i >= 0; i--) {
			if (bestScore.Name[i].Equals(playerName)) {
				curRank = i;
			}
			if (score > bestScore.Score[i]) {
				newRank = i;
			}
		}

		Debug.Log (curRank + " ; " + newRank);

		// Nao foi melhor que seu melhor
		if (newRank > curRank) {
			return;
		} else if (newRank == curRank && score < bestScore.Score [curRank]) {
			return;
		}

		// Melhorou a pontuacao mas nao mudou de posicao
		else if (newRank == curRank) {
			rd.Ranks [(int)Ranking.BestScore].Score [newRank] = (float)score;
			needSave = true;
		}
		// Subiu de ranking
		else {
			// Move os jogadores para baixo
			for (int i = curRank-1; i >= newRank; i--) {
				needSave = true;
				string tName = rd.Ranks [(int)Ranking.BestScore].Name [i];
				float tScore = rd.Ranks [(int)Ranking.BestScore].Score [i];

				rd.Ranks [(int)Ranking.BestScore].Name [i+1] = tName;
				rd.Ranks [(int)Ranking.BestScore].Score [i+1] = tScore;
			}
			rd.Ranks [(int)Ranking.BestScore].Name [newRank] = playerName;
			rd.Ranks [(int)Ranking.BestScore].Score[newRank] = score;
		}

		Debug.Log (needSave);
		if (needSave)
			SaveLevel (level, rd);
	}

	private static void Convert(int fromVersion)
	{
		Debug.Log("Converting Ranking Data. From " + fromVersion + " to " + FileVersion);
	}

	private static void Save(RankingData[] data)
	{
		if (data == null)
		{
			using (BinaryWriter bw = new BinaryWriter(File.Create("Data/ranking.dat")))
			{
				bw.Write (FileVersion);
				for (int level = 0; level < (int)Constants.Levels.Max; level++)
				{
					for (int i = 0; i < RankingData.MaxEntries; i++) {
						bw.Write (new byte[20]);
						bw.Write (-1f);
					}

					for (int i = 0; i < RankingData.MaxEntries; i++) {
						bw.Write (new byte[20]);
						bw.Write (-1f);
					}

					for (int i = 0; i < RankingData.MaxEntries; i++) {
						bw.Write (new byte[20]);
						bw.Write (-1f);
					}
				}

				bw.Close ();
			}
		}
		else
		{
			using (BinaryWriter bw = new BinaryWriter(File.Create("Data/ranking.dat")))
			{
				bw.Write (FileVersion);
				for (int level = 0; level < (int)Constants.Levels.Max; level++)
				{
					for (int i = 0; i < RankingData.MaxEntries; i++)
					{
						string name = StringTool.Truncate (data [level].Ranks [(int)IORanking.Ranking.BestScore].Name [i], 20);
						bw.Write (Encoding.ASCII.GetBytes (name));
						bw.Write (new byte[20 - name.Length]);
						bw.Write (data [level].Ranks [(int)IORanking.Ranking.BestScore].Score [i]);
					}
					
					for (int i = 0; i < RankingData.MaxEntries; i++)
					{
						string name = StringTool.Truncate (data [level].Ranks [(int)IORanking.Ranking.BestTime].Name [i], 20);
						bw.Write (Encoding.ASCII.GetBytes (name));
						bw.Write (new byte[20 - name.Length]);
						bw.Write (data [level].Ranks [(int)IORanking.Ranking.BestTime].Score [i]);
					}
					
					for (int i = 0; i < RankingData.MaxEntries; i++)
					{
						string name = StringTool.Truncate (data [level].Ranks [(int)IORanking.Ranking.BestScoreTime].Name [i], 20);
						bw.Write (Encoding.ASCII.GetBytes (name));
						bw.Write (new byte[20 - name.Length]);
						bw.Write (data [level].Ranks [(int)IORanking.Ranking.BestScoreTime].Score [i]);
					}
				}
				
				bw.Close ();
			}
		}

		return;
	}

	private static void SaveLevel (Constants.Levels level, RankingData data)
	{
		using (BinaryWriter bw = new BinaryWriter(File.OpenWrite("Data/ranking.dat")))
		{
			bw.Seek(24 * (int)level + 4, SeekOrigin.Begin);

			for (int i = 0; i < RankingData.MaxEntries; i++)
			{
				string name = StringTool.Truncate (data.Ranks [(int)IORanking.Ranking.BestScore].Name [i], 20);
				bw.Write (Encoding.ASCII.GetBytes (name));
				bw.Write (new byte[20 - name.Length]);
				bw.Write (data.Ranks [(int)IORanking.Ranking.BestScore].Score [i]);
			}
			
			for (int i = 0; i < RankingData.MaxEntries; i++)
			{
				string name = StringTool.Truncate (data.Ranks [(int)IORanking.Ranking.BestTime].Name [i], 20);
				bw.Write (Encoding.ASCII.GetBytes (name));
				bw.Write (new byte[20 - name.Length]);
				bw.Write (data.Ranks [(int)IORanking.Ranking.BestTime].Score [i]);
			}
			
			for (int i = 0; i < RankingData.MaxEntries; i++)
			{
				string name = StringTool.Truncate (data.Ranks [(int)IORanking.Ranking.BestScoreTime].Name [i], 20);
				bw.Write (Encoding.ASCII.GetBytes (name));
				bw.Write (new byte[20 - name.Length]);
				bw.Write (data.Ranks [(int)IORanking.Ranking.BestScoreTime].Score [i]);
			}
			
			bw.Close ();
		}
		
		return;
	}
}