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
	private float ScaleInit;

	private GameObject keyHelper;
	private Color keyHelperColor = new Color (1f, 1f, 1f);

	void Start () {

		GameObject Castle_GameMngr = GameObject.Find("Castle_GameMngr");
		GameMngr MonstreValue = Castle_GameMngr.GetComponent<GameMngr>();

		string letter = this.name [0].ToString ();
		letter = letter.ToUpper() + "_key";
		keyHelper = GameObject.Find(letter);

		float mutation = Random.Range (0.9f, 1.125f);
		JumpSpeed = MonstreValue.JumpSpeed * mutation;
		JumpHeight = MonstreValue.JumpHeight * mutation;
		//JumpHeight = this.transform.localScale.x;
		JumpSquash = MonstreValue.JumpSquash * mutation;
		AttSpeed = MonstreValue.AttSpeed * -1f;

		posInitY = 5f;
		ScaleInit = this.transform.localScale.x;
		JumpHold = true;
		this.transform.position = new Vector3 (36f, posInitY, 0f);
	}


	// Update is called once per frame
	void Update () {

		float sinF = Mathf.Sin (Time.fixedTime * JumpSpeed);
		//Jumping
		JumpFactor = sinF * JumpHeight;


		//Moving forward
		if (sinF <= 0f) 
		{
			AttSpeedFactor = 0f;
			if (JumpHold == true) 
			{
				JumpHold = false;
				JumpHoldTime++;
			}
			keyHelper.GetComponent<SpriteRenderer> ().color = keyHelperColor;

		} else {
			JumpHold = true;
			if (JumpHoldTime > 2) {
				AttSpeedFactor = 1f;
				JumpHoldTime = 0;
			}
			keyHelper.GetComponent<SpriteRenderer> ().color = new Color(1f,1f,1f);
		}
		transform.localScale = new Vector3 (ScaleInit, ScaleInit + sinF * JumpSquash, 1f);
		transform.position = new Vector3( (transform.position.x + AttSpeed * AttSpeedFactor * Time.deltaTime), posInitY + JumpFactor * AttSpeedFactor, 0f);

	}

	void OnTriggerEnter2D(Collider2D collision) {
		//Debug.Log (collision);
		if (collision.gameObject.tag == "warningZone"){
			keyHelperColor = new Color (0f, 1f, 0f);
		}
	}
}
