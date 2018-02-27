using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour {

	public static GlobalValue instance;

	public float JumpSpeed = 12.0f;
	public float JumpHeight = 4f;
	public float JumpSquash = 0.2f;
	public float AttSpeed = 6f;
	public float SpawnTime = 3.0f;
	public int JumpTime = 0;
	public float LevelScale = 0f;
	public float JumpScale = 1f;

	// Use this for initialization
	void Start () {
		instance = this;
	}

}
