using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager{

	private static GameDataManager instance;
	public static GameDataManager Instance
	{
		get
		{
			if (instance == null) 
			{
				instance = new GameDataManager ();
			}
			return instance;
		}
	}

	public bool pauseOn;

	private GameDataManager()
	{
	}

	public void SetPause(bool pauseOn)
	{
		this.pauseOn = pauseOn; 

		Time.timeScale = this.pauseOn ? 0 : 1;
	}

}
