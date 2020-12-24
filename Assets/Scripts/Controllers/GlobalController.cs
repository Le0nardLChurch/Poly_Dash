using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
	public static GlobalController instance;

	bool endless = false;
	int coinTotal = 1000;
	int lastPlayedLevel = 1;
	bool[] stars = new bool[3] { true, true, true };
	int[] shopCost = new int[] { 500, 500, 1000 };
	int[] powerUps = new int[] { 3, 3, 3 };


	public bool this[int index]
	{
		get { return stars[index]; }
		set { stars[index] = value; }
	}

	public bool Endless
	{
		get { return endless; }
		set { endless = value; }
	}

	public int[] ShopCost
	{
		get { return shopCost; }
	}

	public int CoinTotal
	{
		get { return coinTotal; }
		set { coinTotal = value; }
	}

	public int LastPlayedLevel
	{
		get { return lastPlayedLevel; }
		set { lastPlayedLevel = value; }
	}

	public int Mobility
	{
		get { return powerUps[0]; }
		set { powerUps[0] = value; }
	}

	public int Slow
	{
		get { return powerUps[1]; }
		set { powerUps[1] = value; }
	}

	public int PlayerLives
	{
		get { return powerUps[2]; }
		set { powerUps[2] = value; }
	}

	public void BuyItem(int item)
	{
		CoinTotal -= shopCost[item];
		powerUps[item]++;
	}

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
}





