using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RingDestroyer : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Destroy");
		GameController.ringList.Remove(other.gameObject);
		Destroy(other.gameObject);
	}
}





