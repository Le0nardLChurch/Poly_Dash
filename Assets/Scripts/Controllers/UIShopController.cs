using System.Collections;
using UnityEngine;
using TMPro;

public class UIShopController : UIController
{
#pragma warning disable 0649
	[SerializeField] TextMeshProUGUI coinTotalText;
	[SerializeField] TextMeshProUGUI mobilityCountText;
	[SerializeField] TextMeshProUGUI mobilityCostText;
	[SerializeField] TextMeshProUGUI slowCountText;
	[SerializeField] TextMeshProUGUI slowCostText;
	[SerializeField] TextMeshProUGUI livesCountText;
	[SerializeField] TextMeshProUGUI livesCostText;
#pragma warning restore 0649


	IEnumerator FlashTextCoroutine(TextMeshProUGUI text, Color color)
	{
		Color colorOG;
		for (int i = 0; i <= 3; i++)
		{
			colorOG = text.color;
			text.color = color;
			yield return new WaitForSeconds(0.05f);
			text.color = colorOG; 
		}
	}

	public void OnShopBuyItemClick(int item)
	{
		if (GlobalController.instance.CoinTotal >= GlobalController.instance.ShopCost[item])
		{
			GlobalController.instance.BuyItem(item);
			UpdateShopText();
		}
		else
		{
			StopCoroutine(FlashTextCoroutine(coinTotalText, Color.red));
			StartCoroutine(FlashTextCoroutine(coinTotalText, Color.red));
		}
	}

	public void UpdateShopText()
	{
		coinTotalText.text = "Coins: " + GlobalController.instance.CoinTotal;

		mobilityCostText.text = "Cost: " + GlobalController.instance.ShopCost[0];
		slowCostText.text = "Cost: " + GlobalController.instance.ShopCost[1];
		livesCostText.text = "Cost: " + GlobalController.instance.ShopCost[2];

		livesCountText.text = "Own: " + GlobalController.instance.PlayerLives;
		mobilityCountText.text = "Own: " + GlobalController.instance.Mobility;
		slowCountText.text = "Own: " + GlobalController.instance.Slow;
	}

	void Start()
	{
		UpdateShopText();
	}
}





