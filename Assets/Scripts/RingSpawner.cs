using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Extensions;

public class RingSpawner : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] GameController GC;
	[SerializeField] ThemeController TC;
	[SerializeField] GameObject ringPrefab;
	[SerializeField] GameObject obstaclePrefab;
	[SerializeField] GameObject ringsGO;
	[SerializeField] Vector3 spawnPos = new Vector3(0, 0, 0);
#pragma warning restore 0649
	Material[] ringMat;
	Color[] ringColors;
	int themeIndex = 0;
	int lvlMatIndex = 0;
	int obstacles = 0;
	bool isObstacle = true;


	public Color[] RingColors
	{
		get { return ringColors; }
		set { ringColors = value; }
	}

	bool LastRing()
	{
		return obstacles == 0;
	}

	void SpawnRing(bool isObstacle)
	{
		GameObject ringGO;
		Vector3 lastRingPos = ringsGO.transform.GetChild(ringsGO.transform.childCount - 1).localPosition;

		ringGO = Instantiate(ringPrefab, lastRingPos + spawnPos, ringsGO.transform.rotation, ringsGO.transform);
		ringGO.GetComponent<Rings>().SetMaterial(ringMat[TC.PatternIndex]);

		if (!GC.HasWon())
		{
			ringGO.GetComponent<Rings>().SetColor(ringColors[themeIndex]);
			themeIndex = (themeIndex < ringColors.Length - 1) ? themeIndex + 1 : 0;
		}

		if (isObstacle)
		{
			SpawnObstacle(ringGO);
			obstacles--;
		}

		GameController.ringList.Add(ringGO);
	}

	/* Spawns new obstacle and parents it to ringGO */
	void SpawnObstacle(GameObject ringGO)
	{
		// Spawns new obstacle at ringGO position with random Z rotation. 
		GameObject obstacleGO = Instantiate(obstaclePrefab, ringGO.transform.position, Quaternion.Euler(0, 0, MathExt.RoundOff(Random.Range(0, 360), 15)));
		if (!CheatsController.CC.Debugging)
		{
			obstacleGO.GetComponent<Obstacles>().SetObstacle();
		}

		Coins coins = obstacleGO.GetComponentInChildren<Coins>();
		Coins.CoinType coinType = coins.RandomCoin();

		//Debug.LogWarning(coinType);
		obstacleGO.GetComponentInChildren<Coins>().SetCoins(coinType);
		obstacleGO.transform.SetParent(ringGO.transform);
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.GetComponent<Rings>() != null)
		{
			SpawnRing(isObstacle);

			isObstacle = !LastRing() ? !isObstacle : false;
		}
	}

	void StartRingsSetup(GameObject ringGO)
	{
		int startRingPattern = (TC.PatternIndex > 0) ? TC.PatternIndex - 1 : ringMat.Length - 1;

		ringGO.GetComponent<Rings>().SetMaterial(ringMat[startRingPattern]);

		//ringGO.GetComponent<Rings>().SetColor(ringColors[themeIndex]);
		themeIndex = (themeIndex < ringColors.Length - 1) ? themeIndex + 1 : 0;
	}

	void Awake()
	{
		lvlMatIndex = TC.ThemeIndex;

		GetComponent<Renderer>().material = TC.LvlBackgroundsMat(lvlMatIndex);
		ringColors = TC.LevelColors(lvlMatIndex);
		ringMat = TC.PatternMats;
		obstacles = GC.Goal;
	}

	void Start()
	{
		List<GameObject> startRings = GameObjectExt.GetChildren(ringsGO);
		foreach (GameObject ring in startRings)
		{
			StartRingsSetup(ring);
		}
	}
}





