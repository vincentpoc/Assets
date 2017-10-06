using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameMngr : MonoBehaviour {

	public float JumpSpeed = 10.0f;
	public float JumpHeight = 0.5f;
	public float JumpSquash = 0.25f;
	public float AttSpeed = 10.0f;
	public float SpawnTime = 10f;

	public GameObject[] MonstreLettre;
	public GameObject cloudPuff;
	public GameObject Tower;
	public GameObject TowerMask;
	
	private List <string> SpawMonster;

	private float timeLeft;
	private int monsterID;
	private int TowerFall;

	void Start(){
		
		SpawMonster = new List<string> ();
		timeLeft = 0f;
		monsterID = 0;
		TowerFall = 0;
	}

	void Update () {
		
		timeLeft -= Time.deltaTime;

		if ( timeLeft < 0 ){
			
			GameObject MonsterObject = Instantiate (MonstreLettre[Random.Range(0,MonstreLettre.Length)], new Vector3 (0f, 2.8f, 0f), Quaternion.identity);
			MonsterObject.name += monsterID;
			SpawMonster.Add(MonsterObject.name);

			timeLeft = SpawnTime;
			monsterID++;
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

							if (isUnique && gName.ToLower () == letter.ToLower ()) {
								
								GameObject TargetLetter = GameObject.Find (SpawMonster [i]);

								if (TargetLetter != null) {
									
									for (int z = 1; z < 6; z++) {
										Instantiate (cloudPuff, TargetLetter.transform.position + new Vector3(0f,2f,0f), Quaternion.identity);
									}

									SpawMonster.RemoveAt (i);
									Destroy (TargetLetter);

									GameObject keyHelper = GameObject.Find(gName + "_key");
									keyHelper.GetComponent<SpriteRenderer> ().color = new Color (1f,1f,1f);

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
			TowerFall -= 1;
		}
	}
	void OnTriggerEnter2D( Collider2D MonstreEntry){
		
		int MonsterID = SpawMonster.IndexOf(MonstreEntry.name);

		SpawMonster.RemoveAt (MonsterID);
		Destroy (MonstreEntry.gameObject);

		string letter = MonstreEntry.name [0].ToString ();
		GameObject keyHelper = GameObject.Find(letter + "_key");
		keyHelper.GetComponent<SpriteRenderer> ().color = new Color (1f,1f,1f);

		for (int z = 1; z < 10; z++) {
			Instantiate (cloudPuff, TowerMask.transform.position + new Vector3(z - 5f, 0f, 0f), Quaternion.identity);
		}
		TowerFall = 10;
	}
}
