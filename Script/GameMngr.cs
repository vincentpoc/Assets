using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameMngr : MonoBehaviour {

	public float JumpSpeed = 10.0f;
	public float JumpHeight = 2f;
	public float JumpSquash = 0.2f;
	public float AttSpeed = 4f;
	public float SpawnTime = 4f;

	public GameObject[] MonstreLettre;
	public GameObject cloudPuff;
	public GameObject Tower;
	public GameObject TowerMask;
	
	private List <string> SpawMonster = new List<string> ();
	private List <string> MonstreIndex = new List<string>();
	private string WordDict = "qwe TY asd GH zxc VB Edouard Querbes Aksel Eloit";
	private int WordIndex = 0;

	private float timeLeft = 0f;
	private int monsterID = 0;
	private int TowerFall = 0;

	void Start(){
		
		//SpawMonster = new List<string> ();
		//timeLeft = 0f;
		//monsterID = 0;
		//TowerFall = 0;
		foreach (GameObject n in MonstreLettre) {
			MonstreIndex.Add (n.name [0].ToString ());
		}
	}

	void Update () {
		
		timeLeft -= Time.deltaTime;

		if ( timeLeft < 0 ){

			int letterID = MonstreIndex.IndexOf (WordDict [WordIndex].ToString ());
			if (letterID > 0) {
				GameObject MonsterObject = Instantiate (MonstreLettre [letterID], new Vector3 (0f, 2.8f, 0f), Quaternion.identity);

				MonsterObject.name += monsterID;
				MonsterObject.GetComponent<SpriteRenderer> ().color = new Color (Random.Range (0.25f, 1f), Random.Range (0.25f, 1f), Random.Range (0.25f, 1f));
				SpawMonster.Add (MonsterObject.name);
			}

			WordIndex++;
			if (WordIndex >= WordDict.Length){
				WordIndex = 0;
			}

			monsterID++;
			timeLeft = SpawnTime + Random.Range(0f,1f);

		}

		if (Input.anyKeyDown)
		{
			bool isUnique = true;
			if (SpawMonster.Count > 0) {
				foreach (char c in Input.inputString) {
					
					if (isUnique && System.Char.IsLetter (c)) {
						
						for (int i = 0; i < SpawMonster.Count; i++) {
							
							string gName = SpawMonster [i] [0].ToString ();
							string letter = c.ToString ();

							if (isUnique && gName == letter) {
								
								GameObject TargetLetter = GameObject.Find (SpawMonster [i]);

								if (TargetLetter != null) {

									for (int z = 1; z < 6; z++) {
										GameObject cloudPuffInst = Instantiate (cloudPuff, TargetLetter.transform.position + new Vector3(0f,2f,0f), Quaternion.identity);
										SpriteRenderer cloudPuffColor = cloudPuffInst.GetComponent<SpriteRenderer>();
										cloudPuffColor.color = TargetLetter.GetComponent<SpriteRenderer> ().color;
									}

									SpawMonster.RemoveAt (i);
									Destroy (TargetLetter);

									//REset keyboard helper
									GameObject keyHelper = GameObject.Find(gName.ToUpper() + "_key");
									keyHelper.GetComponent<SpriteRenderer> ().color = new Color (1f,1f,1f);

									//AttSpeed += 0.1;
									//SpawnTime *= 0.9;

									isUnique = false;
									break;
								}
							}
						}
						//-----------------------------------------------//
						if (!isUnique)
							break;
					}
				}
			}
		}

		if ( TowerFall > 0 ){
			Tower.transform.Translate (Vector3.down * 0.25f);		
			Tower.transform.Translate (Vector3.left * Mathf.Sin(TowerFall * 1.125f) * 0.4f);
			TowerFall --;
		}
	}

	//Monster hit the tower
	void OnTriggerEnter2D( Collider2D MonstreEntry){
		
		int MonsterID = SpawMonster.IndexOf(MonstreEntry.name);

		string letter = MonstreEntry.name [0].ToString ();
		GameObject keyHelper = GameObject.Find(letter.ToUpper() + "_key");
		keyHelper.GetComponent<SpriteRenderer> ().color = new Color (1f,1f,1f);

		//Tower puff explosion
		for (int z = 1; z < 10; z++) {
			Instantiate (cloudPuff, TowerMask.transform.position + new Vector3(z - 5f, 0f, 0f), Quaternion.identity);
		}

		SpawMonster.RemoveAt (MonsterID);
		Destroy (MonstreEntry.gameObject);

		TowerFall = 10;
	}
}
