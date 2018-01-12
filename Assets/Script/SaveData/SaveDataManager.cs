using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveDataManager {

	private static SaveDataManager instance;
	public static SaveDataManager Instance
	{
		get
		{
			if (instance == null) 
			{
				instance = new SaveDataManager ();
			}
			return instance;
		}
	}

	private const string  key_beforeScore = "beforeScore";
	private const string  key_bestScore = "bestScore";

	private const string  key_gold = "gold";
	private const string  key_selectSoap = "selectSoap";
	private const string  key_haveSoap = "haveSoap";

	public int beforeScore;
	public int bestScore;

	public int gold;
	public int soapId;
	List<int> allSoapList = new List<int>();
	List<int> haveSoapList = new List<int>();

	private SaveDataManager()
	{
		DataSet ();
	}

	private void DataSet ()
	{
		beforeScore = PlayerPrefs.GetInt(key_beforeScore, 0);
		bestScore = PlayerPrefs.GetInt(key_bestScore, 0);
		
		allSoapList = new List<int> () 
		{
			0,1,2
		};

		haveSoapList = GetListValue (key_haveSoap);
		gold = PlayerPrefs.GetInt(key_gold, 1000);
		soapId = PlayerPrefs.GetInt(key_selectSoap, 0);
	}

	private List<int> GetListValue(string keyValue)
	{
		List<int> list = new List<int> ();

		string strValue = PlayerPrefs.GetString (keyValue,"0");

		if(strValue != string.Empty)
		{
			string[] values = strValue.Split (',');

			foreach (var value in values) {
				list.Add (int.Parse (value));
			}	
		}
			
		return list;
	}

	private void SetListValue(string keyValue , List<int> listValue )
	{
		string strValue = "";

		for (int i = 0; i < listValue.Count; i++) 
		{
			strValue += listValue [i].ToString();

			if(i < listValue.Count-1)
			{
				strValue += ",";
			}
		}

		PlayerPrefs.SetString (keyValue, strValue);
	}

	public void SaveScore(int score)
	{
		beforeScore = score;
		PlayerPrefs.SetInt (key_beforeScore, beforeScore);

		if( bestScore < beforeScore )
		{
			bestScore = beforeScore;
			PlayerPrefs.SetInt (key_bestScore, bestScore);
		}

	}

	public bool SelectSoap(int soapId)
	{
		if(allSoapList.Contains(soapId) == false)
			return false;
		
		this.soapId = soapId;

		PlayerPrefs.SetInt(key_selectSoap, soapId);

		return true;
	}

	public int AllSoapCount()
	{
		return allSoapList.Count;
	}

	public bool CheckHave(int soapId = -1)
	{
		if(soapId == -1)
			soapId = this.soapId;
		
		return haveSoapList.Contains (soapId);
	}

	public void AddSoap(int soapId)
	{
		if(haveSoapList.Contains(soapId))
			return;

		haveSoapList.Add (soapId);
		SetListValue (key_haveSoap, haveSoapList);

		gold -= 200;
		PlayerPrefs.SetInt (key_gold, gold);

	}
}
