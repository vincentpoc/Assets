using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour {

	public static GlobalValue instance;

	public float JumpSpeed = 10.0f;
	public float JumpHeight = 2f;
	public float JumpSquash = 0.2f;
	public float AttSpeed = 4f;
	public float SpawnTime = 2.1f;
	public int JumpTime = 2;
	public float LevelScale = 1f;
	public float JumpScale = 1f;

	// Use this for initialization
	void Start () {
		instance = this;
	}

}
