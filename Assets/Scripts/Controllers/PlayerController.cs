using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Extensions;


public class PlayerController : MonoBehaviour
{
	public static int lives;
	public static int coins;
#pragma warning disable 0649
	[SerializeField] AudioManager AM;
	[SerializeField] GameController GC;
	[SerializeField] ThemeController TC;
	[SerializeField] UIGameController UIC;
	[SerializeField] EndlessController EC;
	[SerializeField] ParticleSystem deathParticles;
#pragma warning restore 0649
	int lvlMatIndex = 0;
	bool isDead = false;

	public bool IsDead
	{
		get { return isDead; }
	}

	void MovePlayer()
	{
		transform.Translate(Vector3.forward * 2.0f * Time.deltaTime);
	}

	IEnumerator NextLifeCoroutine()
	{
		Debug.Log("NextLife");
		yield return new WaitForSeconds(2.0f);
		coins = 0;

		GlobalController.instance.PlayerLives--;
		GlobalController.instance[0] = false;
		SceneExt.SelectScene(GlobalController.instance.LastPlayedLevel);
	}

	IEnumerator GameWinCoroutine()
	{
		Debug.Log("GameWin");
		yield return new WaitForSeconds(3.0f);

		GlobalController.instance.CoinTotal += coins;
		GlobalController.instance.PlayerLives = 3;
		GlobalController.instance.LastPlayedLevel = SceneManager.GetActiveScene().buildIndex;

		UIC.LevelComplete();
	}

	IEnumerator GameOverCoroutine(string nextScene)
	{
		Debug.Log("GameOver");
		yield return new WaitForSeconds(3.0f);

		GlobalController.instance.CoinTotal += coins;
		GlobalController.instance.PlayerLives = 3;
		GlobalController.instance[0] = true;
		SceneExt.SelectScene(nextScene);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Coin"))
		{
			//Debug.LogWarning("Collision: Coin");
			coins += other.GetComponent<Coins>().CoinValue;
			Destroy(other.gameObject);

			AM.Play("Coin", AM.GetEffects());
		}

		//when you pass an obstacle without colliding
		if (other.CompareTag("Goal"))
		{
			//Debug.LogWarning("RingCount: " + GC.Collected);
			GC.Collected++;
			UIC.UpdateGameText(GC.Collected, GC.Goal);
			AM.Play("Ring", AM.GetEffects());

			//In endless, changes materials every 25 levels.
			if (GC.Collected % 25 == 0 && EC != null)
			{
				EC.SetMaterials();
			}

			//Call ringCount functions
			if (GC.HasWon())
			{
				StartCoroutine(GameWinCoroutine());
			}
		}

		// If collided with obstacle 
		if (other.CompareTag("Obstacle"))
		{
			//Debug.LogWarning("Collision: Obstacle");
			//stops rings
			isDead = true;
			GC.IsMoving = false;

			GetComponent<Animator>().enabled = false;
			GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
			deathParticles.Play();
			AM.Play("Death", AM.GetEffects());

			if (lives > 0)
			{
				StartCoroutine(NextLifeCoroutine());
			}
			else
			{
				StartCoroutine(GameOverCoroutine("GameOverScene"));
			}
		}
	}

	void Update()
	{
		if (GC.HasWon())
		{
			MovePlayer();
		}
	}

	void Awake()
	{
		lvlMatIndex = TC.ThemeIndex;
		GetComponentInChildren<SkinnedMeshRenderer>().material = TC.PlayerMat(lvlMatIndex);
		deathParticles.GetComponent<ParticleSystemRenderer>().material = TC.PlayerMat(lvlMatIndex);
	}

	IEnumerator Start()
	{
		GlobalController.instance.LastPlayedLevel = SceneManager.GetActiveScene().buildIndex;

		coins = 0;
		if (GC.Goal > 0)
		{
			lives = GlobalController.instance.PlayerLives;
			yield return new WaitUntil(() => AM.HasLoaded);
			AM.Play(SceneManager.GetActiveScene().name, AM.GetMusic());
		}
		else
		{
			lives = 0;
			yield return new WaitUntil(() => AM.HasLoaded);
			StartCoroutine(AM.PlayChain(AM.GetMusic(), true));
		}
		UIC.UpdateGameText(GC.Collected, GC.Goal);
	}
}





