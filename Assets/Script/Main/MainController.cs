using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

	[SerializeField] private SoapSelect soapSelect;
	[SerializeField] private Button statBtn;
	[SerializeField] private Button soapBuyBtn;
	[SerializeField] private Text goldText;

	[SerializeField] private Text beforeScoreText;
	[SerializeField] private Text bestScoreText;

	// Use this for initialization
	void Start () {

		EventSet ();
		DataSet ();
	}
		
	private void EventSet ()
	{
		soapSelect.selectOn = SoapSelectOn;
		GameDataManager.Instance.SetPause (false);
	}
		
	private void SoapSelectOn()
	{
		BtnStateReset ();
	}
	private void BtnStateReset()
	{
		bool haveFlag = SaveDataManager.Instance.CheckHave ();;
		statBtn.gameObject.SetActive (haveFlag);
		soapBuyBtn.gameObject.SetActive (!haveFlag);
	}
	private void DataSet ()
	{
		GoldReset ();
		BtnStateReset ();
		ScoreSet ();
	}

	private void GoldReset()
	{
		goldText.text = SaveDataManager.Instance.gold.ToString ();;
	}

	public void SoapBuyClickOn()
	{
		int soapId = SaveDataManager.Instance.soapId;
		SaveDataManager.Instance.AddSoap (soapId);

		BtnStateReset ();
		GoldReset ();
		soapSelect.SoapImageReset ();
	}

	private void ScoreSet ()
	{
		beforeScoreText.text = SaveDataManager.Instance.beforeScore.ToString();
		bestScoreText.text = SaveDataManager.Instance.bestScore.ToString();
	}

	public void StartGame()
	{
		SceneManager.LoadScene ("InGame");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
