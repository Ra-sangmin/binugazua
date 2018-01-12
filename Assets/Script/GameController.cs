using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField] private RectTransform pausePanel;
	[SerializeField] private RectTransform resultPanel;

	public Character character;
	Vector2 firstPos;
	public int score;
	[SerializeField] private Text scoreText;
	[SerializeField] private Text godlText;


	// Use this for initialization
	void Start () {
		GameDataManager.Instance.SetPause (false);

		firstPos = character.transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
		ScoreCheck ();
	}

	private void ScoreCheck ()
	{
		if(GameDataManager.Instance.pauseOn || character.gameOverOn)
			return;
		
		float value = character.transform.parent.position.x - firstPos.x;

		int newValue = (int)(value / 100);

		if(score < newValue)
		{
			score = newValue;
			scoreText.text = score.ToString();	
		}
	}

	public void pauseOn(bool pauseValue)
	{
		pausePanel.gameObject.SetActive (pauseValue);
		GameDataManager.Instance.SetPause (pauseValue);
	}
	public void GameOverOn()
	{
		GameDataManager.Instance.SetPause (true);
		resultPanel.gameObject.SetActive (true);
	}

	public void ReStartOn()
	{
		SaveDataManager.Instance.SaveScore (score);
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void GoHomeOn()
	{
		SaveDataManager.Instance.SaveScore (score);
		SceneManager.LoadScene ("Main");
	}
}
