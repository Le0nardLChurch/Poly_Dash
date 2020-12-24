using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] PlayerController PC;
	[SerializeField] GameController GC;
	[SerializeField] GameObject pausePanel;
#pragma warning restore 0649
	bool paused = false;


	public void PauseGame(bool isPaused)
	{
		paused = isPaused;
		pausePanel.SetActive(isPaused);
		GC.StopRings(isPaused);
		PC.GetComponent<Animator>().enabled = !isPaused;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !PC.IsDead && !GC.HasWon())
		{
			paused = !paused;
			PauseGame(paused);
		}
	}

	void Start()
	{
		pausePanel.SetActive(false);
	}
}





