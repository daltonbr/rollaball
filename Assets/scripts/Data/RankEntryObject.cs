using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankEntryObject : MonoBehaviour
{
	public Text Pos;
	public Text Name;
	public Text Score;

	public void UpdateDisplay(int pos, string name, string score)
	{
		this.Pos.text = "#" + pos;
		this.Name.text = name;
		this.Score.text = score;
	}
}
