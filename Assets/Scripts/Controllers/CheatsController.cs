using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Extensions;

public class CheatsController : MonoBehaviour
{
	public static CheatsController CC;
	SceneData sceneData;
	GameController GC;
	UIGameController UIGC;
	UIShopController UISC;
	List<KeyCode> cheatCode;
	Dictionary<string, List<KeyCode>> cheats = new Dictionary<string, List<KeyCode>>()
	{
		{ "Konami", new List<KeyCode> { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A, KeyCode.Return } },
		{ "Debug", new List<KeyCode> { KeyCode.D, KeyCode.E, KeyCode.B, KeyCode.U, KeyCode.G } },
		{ "RingSpeed", new List<KeyCode> { KeyCode.S, KeyCode.P, KeyCode.E, KeyCode.E, KeyCode.D } },
		{ "Slow", new List<KeyCode> { KeyCode.S, KeyCode.L, KeyCode.O, KeyCode.W } },
		{ "Zero", new List<KeyCode> { KeyCode.Z, KeyCode.E, KeyCode.R, KeyCode.O } },
	};
	bool debug = false;


	public bool Debugging
	{
		get { return debug; }
		set { debug = value; }
	}

	void LoadSceneData()
	{
		sceneData = FindObjectOfType<SceneData>();
	}

	void LoadControllers()
	{
		GC = sceneData.GameController;
		UIGC = sceneData.UIGameController;
		UISC = sceneData.UIShopController;
	}

	void Update()
	{
		foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(key))
			{
				//Debug.Log("Key: " + key);
				cheatCode.Add(key);
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape) || cheatCode.Count > 15)
		{
			//Debug.Log("Key: " + KeyCode.Escape);
			cheatCode.Clear();
		}
		else
		if (ListExt.IsEqual(cheatCode, cheats["Konami"]))
		{
			Debug.LogError("Contra");
			GlobalController.instance.PlayerLives = 30;
			if (UIGC != null)
				UIGC.UpdateLivesText();
			if (UISC != null)
				UISC.UpdateShopText();
			cheatCode.Clear();
		}
		else
		if (ListExt.IsEqual(cheatCode, cheats["Zero"]))
		{
			Debug.LogError("NoLife");
			GlobalController.instance.PlayerLives = 3;
			if (UIGC != null)
				UIGC.UpdateLivesText();
			if (UISC != null)
				UISC.UpdateShopText();
			cheatCode.Clear();
		}
		else
		if (ListExt.IsEqual(cheatCode, cheats["Debug"]))
		{
			Debug.LogError("Debug");
			debug = !debug;
			cheatCode.Clear();
		}
		else
		if (ListExt.IsEqual(cheatCode, cheats["RingSpeed"]))
		{
			Debug.LogError("Speed");
			if (GC == null)
				return;
			GC.RingSpeed += 5.0f;
			cheatCode.Clear();
		}
		else
		if (ListExt.IsEqual(cheatCode, cheats["Slow"]))
		{
			Debug.LogError("Slow");
			if (GC == null)
				return;
			GC.RingSpeed = 1.0f;
			cheatCode.Clear();
		}
		else
		if (Input.GetKeyDown(KeyCode.LeftBracket))
		{
			Debug.LogError("Money");
			GlobalController.instance.CoinTotal += 10000;
			if (UISC == null)
				return;
			UISC.UpdateShopText();
			cheatCode.Clear();
		}
		else
		if (Input.GetKeyDown(KeyCode.RightBracket))
		{
			Debug.LogError("NoMoney");
			GlobalController.instance.CoinTotal = 100;
			if (UISC == null)
				return;
			UISC.UpdateShopText();
			cheatCode.Clear();
		}
	}

	void Awake()
	{
		cheatCode = new List<KeyCode>();

		if (CC == null)
		{
			CC = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (CC != this)
		{
			DestroyImmediate(gameObject);
		}

		
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Finds controllers in new scene
		LoadSceneData();
		LoadControllers();
	}
}





