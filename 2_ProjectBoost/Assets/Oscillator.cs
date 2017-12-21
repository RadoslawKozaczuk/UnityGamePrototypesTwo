using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

	[SerializeField] Vector3 movementVector = Vector3.right;
	[SerializeField] float period = 5f;

	[Range(0, 1)]
	[SerializeField]
	float movementFactor = 0;

	Vector3 startingPos;

	Transform tranform;

	// Use this for initialization
	void Start () {
		tranform = GetComponent<Transform>();
		startingPos = tranform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// comparing floats is unpredictable because they may different by a very tiny amount
		// the smallest float available is Mathf.Epsilon
		// so we write <= Epsilon to say equal 0
		if (period <= Mathf.Epsilon) return; // protect against zero


		// Time.time is frame independent by definition 
		float cycles = Time.time / period;
		const float tau = Mathf.PI * 2; // about 6.28
		float rawSineWave = Mathf.Sin(cycles * tau);

		movementFactor = rawSineWave / 2f + 0.5f; // normalization to <0,1>

		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
	}
}
