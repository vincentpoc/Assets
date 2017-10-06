using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monstre_script : MonoBehaviour {

	public string KeyID;

	private float JumpSpeed;
	private float JumpHeight;
	private float JumpSquash;
	private float AttSpeed;

	private float posInitY;
	private float JumpFactor;
	private bool JumpHold;
	private int JumpHoldTime;
	private float AttSpeedFactor;
	private int MonsterHeight;

	private GameObject keyHelper;
	private Color keyHelperColor = new Color (1f, 1f, 1f);

	void Start () {

		GameObject Castle_GameMngr = GameObject.Find("Castle_GameMngr");
		GameMngr MonstreValue = Castle_GameMngr.GetComponent<GameMngr>();

		string letter = this.name [0].ToString () + "_key";
		keyHelper = GameObject.Find(letter);

		float mutation = Random.Range (0.9f, 1.125f);
		JumpSpeed = MonstreValue.JumpSpeed * mutation;
		JumpHeight = MonstreValue.JumpHeight * mutation;
		JumpSquash = MonstreValue.JumpSquash * mutation;
		AttSpeed = MonstreValue.AttSpeed * -1f;

		posInitY = 5f;
		JumpHold = true;
		this.transform.position = new Vector3 (36f, posInitY, 0f);
	}


	// Update is called once per frame
	void Update () {

		float sinF = Mathf.Sin (Time.fixedTime * JumpSpeed);
		//Jumping
		JumpFactor = sinF * JumpHeight;
		transform.localScale = new Vector3 (1f, 1f + sinF * JumpSquash, 1f);

		//Moving forward
		if (sinF <= 0f) 
		{
			JumpFactor = 0f;
			AttSpeedFactor = 0f;
			if (JumpHold == true) 
			{
				JumpHold = false;
				JumpHoldTime++;
			}
			keyHelper.GetComponent<SpriteRenderer> ().color = keyHelperColor;

		} else {
			JumpHold = true;
			if (JumpHoldTime > 2) 
			{
				AttSpeedFactor = AttSpeed;
				JumpHoldTime = 0;
			}
			keyHelper.GetComponent<SpriteRenderer> ().color = new Color(1f,1f,1f);
		}

		transform.position = new Vector3( (transform.position.x + AttSpeedFactor * Time.deltaTime), posInitY + JumpFactor, 0f);

	}

	void OnTriggerEnter2D(Collider2D collision) {
		//Debug.Log (collision);
		if (collision.gameObject.tag == "warningZone"){
			keyHelperColor = new Color (0f, 1f, 0f);
		}
	}
}
