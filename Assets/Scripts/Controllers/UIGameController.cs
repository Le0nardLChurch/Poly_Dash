using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIGameController : UIController
{
#pragma warning disable 0649
	[SerializeField] GameController GC;
	[SerializeField] PauseController PC;
	[SerializeField] TextMeshProUGUI ringsText;
	[SerializeField] TextMeshProUGUI coinsText;
	[SerializeField] TextMeshProUGUI mobilityText;
	[SerializeField] TextMeshProUGUI slowText;
	[SerializeField] TextMeshProUGUI livesText;
	[SerializeField] GameObject winPanel;
	[SerializeField] Image[] starsImages;
	[SerializeField] Sprite starFillImage;
	[SerializeField] Sprite starEmptyImage;
#pragma warning restore 0649


	public override void OnMenu()
	{
		GlobalController.instance.CoinTotal += PlayerController.coins;
		SceneManager.LoadScene("MainMenuScene");
	}

	public void OnResumeClick()
	{
		PC.PauseGame(false);
	}

	IEnumerator FillStarsCoroutine(int index)
	{
		if (index != starsImages.Length)
		{
			if (GlobalController.instance[index])
			{
				yield return new WaitForSeconds(0.5f);
				starsImages[index].sprite = starFillImage;
			}
			else
			{
				GlobalController.instance[index] = true;
			}
			StartCoroutine(FillStarsCoroutine(index + 1));
		}
	}

	void FillStars(int index)
	{
		starsImages[index].sprite = starFillImage;

		GlobalController.instance[index] = true;
	}

	public void LevelComplete()
	{
		winPanel.SetActive(true);

		StartCoroutine(FillStarsCoroutine(0));
	}

	public void UpdateGameText(int rings, int goal)
	{
		if (goal > 0)
		{
			ringsText.text = "Obstacles: " + rings + " of " + goal;
		}
		else
		{
			ringsText.text = "Obstacles: " + rings + " of ∞";
		}

		UpdateGameText();
	}

	public void UpdateLivesText()
	{
		livesText.text = "Lives: " + GlobalController.instance.PlayerLives;
	}

	public void UpdateGameText()
	{
		coinsText.text = "Coins: " + PlayerController.coins;
		livesText.text = "Lives: " + PlayerController.lives;
		mobilityText.text = ": " + GlobalController.instance.Mobility;
		slowText.text = ": " + GlobalController.instance.Slow;
	}

	void Start()
	{
		winPanel.SetActive(false);
	}
}





