using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Mobcast.Coffee.UI;

public class SoapSelect : MonoBehaviour {

	[SerializeField] private AtlasImage soapImage;

	[SerializeField] private Button rightArrowBtn;
	[SerializeField] private Button leftArrowBtn;

	[SerializeField] private Sprite activeArrowImage;
	[SerializeField] private Sprite inActiveArrowImage;

	public UnityAction selectOn;

	// Use this for initialization
	void Start () {
		SoapImageReset ();
		ArrowActiveReset ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SoapImageReset()
	{
		int soapId = SaveDataManager.Instance.soapId;

		string imageName = "";

		if (SaveDataManager.Instance.CheckHave (soapId) == false) 
		{
			soapImage.spriteName = "Soap_Lock";
		} 
		else 
		{
			string spriteName = string.Format ("Soap_{0}", soapId);
			soapImage.spriteName = spriteName;
		}
	}

	public void ArrowClickOn(bool left)
	{
		int addValue = left ? -1 : 1;

		int newSoapId = SaveDataManager.Instance.soapId + addValue;
		bool selectSaveOn = SaveDataManager.Instance.SelectSoap (newSoapId);

		if(selectSaveOn == false)
			return;
		
		ArrowActiveReset ();
		SoapImageReset ();
		selectOn ();
	}

	private void ArrowActiveReset()
	{
		int allCount = SaveDataManager.Instance.AllSoapCount();
		int soapId = SaveDataManager.Instance.soapId;

		if(soapId == 0)
		{
			BtnActiveSet (leftArrowBtn, false);
			BtnActiveSet (rightArrowBtn, true);
		}
		else if(soapId >= allCount -1)
		{
			BtnActiveSet (leftArrowBtn, true);
			BtnActiveSet (rightArrowBtn, false);
		}
		else
		{
			BtnActiveSet (leftArrowBtn, true);
			BtnActiveSet (rightArrowBtn, true);
		}

	}

	private void BtnActiveSet(Button button , bool active)
	{
		button.image.sprite = active ? activeArrowImage : inActiveArrowImage;
		button.interactable = active;
	}


}
