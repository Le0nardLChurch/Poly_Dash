//////////////////////////////////////////////////////////
// File: Transforms
// Name: Robert Secoura
// Date: 9/24/19
//////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transforms : MonoBehaviour
{
#pragma warning disable 0649
	[Header("Transforms")]
	[SerializeField] float xRotate = 0;
    [SerializeField] float yRotate = 0;
    [SerializeField] float zRotate = 0;
	[Header("Rotation")]
    [SerializeField] float xSpin = 0;
    [SerializeField] float ySpin = 0;
	[SerializeField] float zSpin = 0;
    [Header("Float")]
    //[SerializeField] float amplitude = 0.5f;
	//[SerializeField] float frequency = 1f;
#pragma warning restore 0649

    Vector3 posOffset = new Vector3();


    private void Update()
    {
		// Objects rotation
        transform.Rotate(new Vector3(xSpin, ySpin, zSpin) * Time.deltaTime);
		/**
		// Objects Float
		tempPos = posOffset;
        tempPos.y += (Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude);

        transform.position = tempPos;
		/**/
	}
    private void Start()
    {
        // Store the starting position & rotation of the object
		transform.Rotate(new Vector3(xRotate, yRotate, zRotate));
        posOffset = transform.localPosition;
	}
}





