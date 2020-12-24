using UnityEngine;
using UnityEngine.SceneManagement;
using Extensions;

public class UIController : MonoBehaviour
{
	public void OnStart()
	{
		SceneManager.LoadScene(GlobalController.instance.LastPlayedLevel);
	}
	public void OnShop()
	{
		SceneManager.LoadScene("ShopScene");
	}
	public void OnLevels(int level)
	{
		SceneManager.LoadScene(level);
	}
	public void OnHelp()
	{
		SceneManager.LoadScene("HelpScene");
	}
	public void OnCredits()
	{
		SceneManager.LoadScene("CreditsScene");
	}
	public virtual void OnMenu()
	{
		SceneManager.LoadScene("MainMenuScene");
	}
    public void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnQuit()
	{
		Debug.LogError("Quit");
		Application.Quit();
	}

	public void OnNextLvl()
	{
		if (GlobalController.instance.LastPlayedLevel < 3)
		{
			SceneExt.SelectScene(GlobalController.instance.LastPlayedLevel + 1);
		}
		else
		{
			SceneExt.SelectScene("WinScene");
		}
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReset();
        }
    }
}





