using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameMngr : MonoBehaviour {

	/*
	public float JumpSpeed = 10.0f;
	public float JumpHeight = 2f;
	public float JumpSquash = 0.2f;
	public float AttSpeed = 4f;
	public float SpawnTime = 4f;
	*/
	public GameObject[] MonstreLettre;
	public GameObject[] MonstrePacList;
	public GameObject cloudPuff;
	public GameObject Tower;
	public TextAsset WordDicFile;
	public Text UItext;
	public Text Gameover;
	public Slider mainSlider;
	public Slider jumpSlider;
	public List <Sprite> CakeState;

	private GameObject MonsterPacman;
	private List <string> SpawMonster = new List<string> ();
	private List <string> MonstreIndex = new List<string>();
	private List <string> WordDict = new List<string>();
	private int WordLevel = 0;

	private float timeLeft = 0f;
	private int monsterID = 0;
	private int Life = 4;

	void Start(){

		Gameover.color = Color.clear;

		foreach (GameObject n in MonstreLettre) {
			MonstreIndex.Add (n.name [0].ToString ());
		}

		//Build WordDictionnary
		/*
		string path = "Assets/Resources/WordDict.txt";
		StreamReader reader = new StreamReader(path);

		string WordData;
		do{
			WordData = reader.ReadLine ();
			WordDict.Add(WordData);
		}while(WordData != null);
		*/

		string WordDictText = WordDicFile.text.ToString ();
		string[] WordDictList = WordDictText.Split('#');
		foreach (string w in WordDictList) {
			WordDict.Add (w);
		}
	}

	void Update () 
	{
		timeLeft -= Time.deltaTime;

		//GlobalValue.instance.LevelScale = mainSlider.value;
		GlobalValue.instance.JumpScale = jumpSlider.value;

		if (Life > 0) 
		{
			if (SpawMonster.Count == 0) {
				if (MonsterPacman != null) {
					for (int z = 1; z < 6; z++) {
						Instantiate (cloudPuff, MonsterPacman.transform.position + new Vector3 (0f, 2f, 0f), Quaternion.identity);
						//SpriteRenderer cloudPuffColor = cloudPuffInst.GetComponent<SpriteRenderer> ();
					}
					Destroy (MonsterPacman);
				}

				if ( timeLeft < 0 ){
					
					MonsterPacman = Instantiate (MonstrePacList [Random.Range (0, 7)], new Vector3 (39f, 1.0f, 0f), Quaternion.identity);

					int wordpicker = Random.Range (0, WordDict.Count);
					float pos_X_int = (37f - (WordDict [wordpicker].Length * 1.1f)) / 2.0f;
					for (int LetterIndex = 0; LetterIndex < WordDict [wordpicker].Length; LetterIndex++) 
					{		
						int letterID = MonstreIndex.IndexOf (WordDict [wordpicker] [LetterIndex].ToString ().ToLower());

						if (letterID != -1) 
						{
							GameObject MonsterObject = Instantiate (MonstreLettre [letterID], new Vector3 (pos_X_int + (1.1f * LetterIndex), 16f, 0f), Quaternion.identity);

							MonsterObject.name += monsterID;
							MonsterObject.GetComponent<SpriteRenderer> ().color = new Color (Random.Range (0.25f, 1f), Random.Range (0.25f, 1f), Random.Range (0.25f, 1f));
							SpawMonster.Add (MonsterObject.name);
							monsterID++;
						}
					}
					WordDict.RemoveAt (wordpicker);
					//timeLeft = GlobalValue.instance.SpawnTime + 1f;

					WordLevel++;
					GlobalValue.instance.LevelScale = Mathf.Max(GlobalValue.instance.LevelScale + 0.1f,1f);
					UItext.text = "Level " + WordLevel.ToString ();
				}
			}

		}
		//--------------------------------------------- DESTROY LETTER ON KEY PRESS ---------------------------//
		if (Input.anyKeyDown)
		{
			bool isUnique = true;
			if (SpawMonster.Count > 0) {
				foreach (char c in Input.inputString) {
					
					if (isUnique && System.Char.IsLetter (c)) 
					{
						for (int i = 0; i < SpawMonster.Count; i++) 
						{
							string gName = SpawMonster [i] [0].ToString ();
							string letter = c.ToString ().ToLower();

							if (isUnique && gName == letter) 
							{
								GameObject TargetLetter = GameObject.Find (SpawMonster [i]);

								if (TargetLetter != null) 
								{
									for (int z = 1; z < 6; z++) 
									{
										Instantiate (cloudPuff, TargetLetter.transform.position, Quaternion.identity);
									}

									SpawMonster.RemoveAt (i);
									Destroy (TargetLetter);

									isUnique = false;
									break;
								}
							}
						}
						//-----------------------------------------------//
						if (!isUnique) {
							break;
						}
					}
				}

				if (isUnique) {
					Debug.Log ("Malus");
					MonsterPacman.transform.position += new Vector3(-1.0f,0.0f,0.0f);
					//GlobalValue.instance.LevelScale += 0.1f;
				}
			}
		}


	}

	//----------------------------------------------------- Monster hit the tower --------------------------------------------//
	void OnTriggerEnter2D( Collider2D MonstreEntry){

		Life--;
		//Debug.Log (Life);
		//Debug.Log (CakeState[Life]);
		if (Life < 0) {
			//MonsterPacman.GetComponent<Monstre_script>().enabled = false;
			Gameover.color = Color.white;
		} else {
			Destroy (MonsterPacman);
			MonsterPacman = Instantiate (MonstrePacList [Random.Range (0, 7)], new Vector3 (39f, 1.0f, 0f), Quaternion.identity);
			Tower.GetComponent<SpriteRenderer>().sprite = CakeState[Life];
		}
	}
}
