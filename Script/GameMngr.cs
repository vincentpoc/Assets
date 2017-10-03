﻿using System.Collections;
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

	private List <string> SpawMonster;
	private bool isUnique;
	private float timeLeft;
	private int monsterID;

	void Start(){
		SpawMonster = new List<string> ();
		timeLeft = 0f;
		monsterID = 0;
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
			isUnique = true;
			if (SpawMonster.Count > 0) {
				foreach (char c in Input.inputString) {
					if (isUnique && System.Char.IsLetter (c)) {
						//-----------------------------------------------//
						for (int i = 0; i < SpawMonster.Count; i++) {
							string gName = SpawMonster [i] [0].ToString ();
							string letter = c.ToString ();

							if (isUnique && gName.ToLower () == letter.ToLower ()) {
								GameObject TargetLetter = GameObject.Find (SpawMonster [i]);
								if (TargetLetter != null) {
									Destroy (TargetLetter);
									SpawMonster.RemoveAt (i);
								}
								isUnique = false;
								break;
							}
						}
						//-----------------------------------------------//
						if (!isUnique)
							break;
					}
				}
			}
		}
	}

	void OnGUI(){
		

	}

}