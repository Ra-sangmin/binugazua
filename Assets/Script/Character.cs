using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mobcast.Coffee.UI;

public class Character : MonoBehaviour {

	[SerializeField] Rigidbody2D rigid;
	[SerializeField] Camera cam;
	[SerializeField] private AtlasImage soapImage;
	public GameObject soapObj;
	private GameObject inTriggerObj;
	bool startOn;
	bool beforeLandOnValue;
	float saveValue;

	public bool gameOverOn;

	// Use this for initialization
	void Start () {
		
		ImageSet ();
		StartCoroutine (StartDoing ());
	}

	private void ImageSet ()
	{
		int soapId = SaveDataManager.Instance.soapId;
		string spriteName = string.Format ("Soap_{0}", soapId);
		soapImage.spriteName = spriteName;
	}

	IEnumerator StartDoing()
	{
		yield return new WaitForSeconds(1.5f);
		rigid.AddForce (new Vector2(1,0) * 10000);
		rigid.drag = 1;

		yield return new WaitForSeconds(1f);

		rigid.drag = 0;
		saveValue = 0.01f;

		yield return new WaitForSeconds(3f);

		startOn = true;

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Space))
		{
			GameDataManager.Instance.SetPause (true);
		}
		if(Input.GetKey(KeyCode.A))
		{
			GameDataManager.Instance.SetPause (false);
		}
			
		if(GameDataManager.Instance.pauseOn || gameOverOn)
			return;
		
		CamMove ();
		InputCheck ();
		InLandCheck ();
	}

	private void CamMove ()
	{
		Vector2 pos = rigid.transform.localPosition;
		cam.transform.localPosition = new Vector3 (pos.x, pos.y, -750);
	}

	private void InputCheck()
	{
		if(Input.GetKey(KeyCode.Mouse0) && InLand() == false)
		{
			soapObj.transform.Rotate(Vector3.back* 10);
			saveValue = 0;
		}
	}

	private void InLandCheck ()
	{
		rigid.freezeRotation = !InLand ();

		if(startOn == false)
			return;

		if (InLand () != beforeLandOnValue) {
			IsLandChangeOn ();
		} else if (InLand ()){

			float addValue = rigid.drag > 2 ? 1 : 0.01f;
			rigid.drag += addValue;

			if(rigid.drag > 100)
			{
				GameOverOn();
			}
		}
	}

	void IsLandChangeOn()
	{
		beforeLandOnValue = InLand ();

		if (InLand () && startOn) {
			rigid.drag = saveValue;
			if(saveValue == 0)
			{
				rigid.AddForce (new Vector2(1,-1) * 5000);
				Debug.LogWarning ("buster On");
			}
		} 
		else 
		{
			saveValue = rigid.drag;
			rigid.drag = 1;
		}	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "GameOverZone")
		{
			GameOverOn ();
			return;
		}

		inTriggerObj = col.gameObject;
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(inTriggerObj == col.gameObject)
		{
			inTriggerObj = null;
		}
	}

	private bool InLand()
	{
		return inTriggerObj != null;
	}

	private void GameOverOn()
	{
		gameOverOn = true;

		StartCoroutine (GameOverEffectDoing ());
	}

	IEnumerator GameOverEffectDoing()
	{
		int nowFOV = (int)cam.fieldOfView;
		int maxFOV = 55;

		for (int i = nowFOV; i > maxFOV; i--) 
		{
			cam.fieldOfView = i;
			yield return null;
		}
		
		yield return new WaitForSeconds (0.5f);

		GameObject.FindObjectOfType<GameController> ().GameOverOn ();
	}
}
