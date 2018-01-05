using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	[Tooltip("In m^-1")]
	[SerializeField] private float xSpeed = 15f;

	[Tooltip("Horizontal max lean range in m")]
	[SerializeField] private float xRange = 9f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		float xOffset = xThrow * xSpeed * Time.deltaTime;
		float rawXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);

		transform.localPosition = new Vector3(rawXPos, transform.localPosition.y, transform.localPosition.z);

		print(xOffset);
	}
}
