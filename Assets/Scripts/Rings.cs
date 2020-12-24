using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings : MonoBehaviour
{
	public void SetMaterial(Material material)
	{
		GetComponent<Renderer>().material = material;
	}
	public void SetColor(Color color)
	{
		GetComponent<Renderer>().material.color = color;
	}	
}





