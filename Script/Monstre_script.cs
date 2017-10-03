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

	void Start () {

		GameObject GameManager = GameObject.Find("GameMngr");
		GameMngr MonstreValue = GameManager.GetComponent<GameMngr>();

		float mutation = Random.Range (0.9f, 1.125f);
		JumpSpeed = MonstreValue.JumpSpeed * mutation;
		JumpHeight = MonstreValue.JumpHeight * mutation;
		JumpSquash = MonstreValue.JumpSquash * mutation;
		AttSpeed = MonstreValue.AttSpeed;

		Vector3 MonsterPos = new Vector3 (-2.0f, 0f, 0f);
		//Right side
		foreach(string c in new string[]{"Y","U","I","O","P","H","J","K","L","B","N","M"}){
			if (this.name[0].ToString() == c){
				AttSpeed *= -1;
				MonsterPos.x = 36.2f;
				break;
			}
		}
		//Air
		foreach (string c in new string[]{"Q","W","E","R","T","Y","U","I","O","P"}) {
			if (this.name [0].ToString () == c) {
				MonsterPos.y = 15f;
				MonsterHeight = 3;
				break;
			}
		}
		//Ground
		foreach (string c in new string[]{"A","S","D","F","G","H","J","K","L"}) {
			if (this.name [0].ToString () == c) {
				MonsterPos.y = 5f;
				MonsterHeight = 2;
				break;
			}
		}
		//Water
		foreach (string c in new string[]{"Z","X","C","V","B","N","M"}) {
			if (this.name [0].ToString () == c) {
				MonsterPos.y = 0.7f;
				MonsterHeight = 1;
				break;
			}
		}
		JumpHold = true;
		this.transform.position = MonsterPos;
		posInitY = MonsterPos.y;
	}


	// Update is called once per frame
	void Update () {
		
		float sinF = Mathf.Sin (Time.fixedTime * JumpSpeed);
		//Jumping
		if (MonsterHeight != 1)
			JumpFactor = sinF * JumpHeight;
		else
			JumpFactor = 0;

		if (MonsterHeight != 3) {
			transform.localScale = new Vector3 (1f, 1f + sinF * JumpSquash, 1f);
		}

		//Moving forward
		if (sinF <= 0f) 
		{
			if (MonsterHeight != 3) {
				JumpFactor = 0f;
			}
			AttSpeedFactor = 0f;
			if (JumpHold == true) 
			{
				JumpHold = false;
				JumpHoldTime++;
			}

		} else {
			JumpHold = true;
			if (JumpHoldTime > 2) 
			{
				AttSpeedFactor = AttSpeed;
				JumpHoldTime = 0;
			}	
		}

		transform.position = new Vector3( (transform.position.x + AttSpeedFactor * Time.deltaTime), posInitY + JumpFactor, 0f);

	}

	void OnTriggerEnter2D( Collider2D other){
		Destroy(this.gameObject);
	}


}
