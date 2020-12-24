using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class Obstacles : MonoBehaviour
{
#pragma warning disable 0649
	List<GameObject> obstacleModels;
#pragma warning restore 0649



    public void SetObstacle()
    {
		int obstacle = Random.Range(2, obstacleModels.Count);
		obstacleModels[obstacle].SetActive(true);
	}

	List<GameObject> GetChildren(GameObject parent)
	{
		List<GameObject> children = new List<GameObject>();

		foreach (Transform child in parent.transform)
		{
			if (child.IsChildOf(parent.transform))
			{
				children.Add(child.gameObject);
			}
		}
		return children;
	}

	void Awake()
	{
		obstacleModels = GetChildren(gameObject);
	}
}





