using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monstre_script : MonoBehaviour {

	//public string KeyID;
	//public int offset;

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

	private int JumpHoldMax;

	//private GameObject keyHelper;
	//private Color keyHelperColor = new Color (1f, 1f, 1f);

	void Start () {

		//GameObject Castle_GameMngr = GameObject.Find("Castle_GameMngr");
		//GameMngr MonstreValue = Castle_GameMngr.GetComponent<GameMngr>();

		//string letter = this.name [0].ToString ();
		//letter = letter.ToUpper() + "_key";
		//keyHelper = GameObject.Find(letter);

		float mutation = Random.Range (0.9f, 1.2f);
		JumpSpeed = GlobalValue.instance.JumpSpeed;
		JumpHeight = GlobalValue.instance.JumpHeight * mutation;
		//JumpHeight = this.transform.localScale.x;
		JumpSquash = GlobalValue.instance.JumpSquash * mutation;
		AttSpeed = GlobalValue.instance.AttSpeed * -1f;
		JumpHoldMax = 1;

		posInitY = 4f;
		ScaleInit = this.transform.localScale.x;
		JumpHold = true;
		JumpHoldTime = GlobalValue.instance.JumpTime;
		this.transform.position = new Vector3 (this.transform.position.x, posInitY, 0f);
	}


	// Update is called once per frame
	void Update () {

		float sinF = Mathf.Sin (Time.fixedTime * JumpSpeed);
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
			//keyHelper.GetComponent<SpriteRenderer> ().color = keyHelperColor;

		} else {
			JumpHold = true;
			if (JumpHoldTime > JumpHoldMax + GlobalValue.instance.LevelScale * 0.5) { //GlobalValue.instance.JumpTime
				AttSpeedFactor = 1f * GlobalValue.instance.LevelScale;
				JumpHoldTime = 0;
			}
			//keyHelper.GetComponent<SpriteRenderer> ().color = new Color(1f,1f,1f);
		}
		transform.localScale = new Vector3 (ScaleInit, ScaleInit + sinF * JumpSquash * GlobalValue.instance.JumpScale, 1f);
		transform.position = new Vector3( (transform.position.x + AttSpeed * AttSpeedFactor * Time.deltaTime * GlobalValue.instance.LevelScale), posInitY + JumpFactor * AttSpeedFactor * GlobalValue.instance.JumpScale, 0f);
		//transform.rotation(Quaternion.identity);
	}

}
