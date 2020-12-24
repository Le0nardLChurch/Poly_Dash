using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] Material[] coinMaterials;
#pragma warning restore 0649

	CoinType coinType;
	int coinValue;
	Material coinMaterial;

	public enum CoinType
	{
		Gold,
		Silver,
		Bronze,
	}

	public void SetCoins(CoinType coinType)
	{
		this.coinType = coinType;

		switch(coinType)
		{
			case CoinType.Gold:
				coinValue = 25;
				coinMaterial = coinMaterials[0];
				GetComponent<Renderer>().material = coinMaterial;
				break;
			case CoinType.Silver:
				coinValue = 10;
				coinMaterial = coinMaterials[1];
				GetComponent<Renderer>().material = coinMaterial;
				break;
			case CoinType.Bronze:
				coinValue = 5;
				coinMaterial = coinMaterials[2];
				GetComponent<Renderer>().material = coinMaterial;
				break;
		}
	}

	public int CoinValue
	{
		get { return coinValue; }
	}

	public Material CoinMaterial
	{
		get { return coinMaterial; }
	}

	public CoinType RandomCoin()
	{
		Array values = CoinType.GetValues(typeof(CoinType));
		System.Random random = new System.Random();
		return (CoinType)values.GetValue(random.Next(values.Length));
	}

}





