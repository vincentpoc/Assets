using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterPuff : MonoBehaviour {

	private float alphaFade;
	private Vector3 rollVector;
	private float rollFactor;

	void Start () {
		alphaFade = 1f;
		rollFactor = Random.Range (-3f, 3f);
		rollVector = new Vector3 (Random.Range (-10f, 10f), Random.Range (-10f, 10f), 0f);
	}

	void Update () {
		
		rollVector = Vector3.Scale(rollVector, new Vector3(0.96f,0.96f,1f));
		this.transform.Translate (rollVector * 0.005f);

		rollFactor *= 0.95f;
		this.transform.RotateAround (this.transform.position, Vector3.forward, rollFactor);

		alphaFade -= 0.0125f;
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, alphaFade);

		if (alphaFade <= 0) {
			Destroy (this.gameObject);
		}

	}
}
