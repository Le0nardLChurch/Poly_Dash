using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] GameController gameController;
	[SerializeField] AudioController audioController;
	[SerializeField] PauseController pauseController;
	[SerializeField] ThemeController themeController;
	[SerializeField] PlayerController playerController;
	[SerializeField] EndlessController endlessController;
	[SerializeField] UIController uiController;
	[SerializeField] UIShopController uiShopController;
	[SerializeField] UIGameController uiGameController;
	[SerializeField] UIHelpController uiHelpController;
#pragma warning restore 0649


	public PlayerController PlayerController
	{
		get { return playerController; }
	}
	public GameController GameController
	{
		get { return gameController; }
	}
	public PauseController PauseController
	{
		get { return pauseController; }
	}
	public ThemeController ThemeController
	{
		get { return themeController; }
	}
	public AudioController AudioController
	{
		get { return audioController;}
	}
	public EndlessController EndlessController
	{
		get { return endlessController; }
	}
	public UIController UIController
	{
		get { return uiController; }
	}
	public UIShopController UIShopController
	{
		get { return uiShopController; }
	}
	public UIGameController UIGameController
	{
		get { return uiGameController; }
	}
	public UIHelpController UIHelpController
	{
		get { return uiHelpController; }
	}
}





