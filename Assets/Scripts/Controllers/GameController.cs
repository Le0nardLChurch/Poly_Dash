using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Extensions;

public class GameController : MonoBehaviour
{
	public static List<GameObject> ringList;
#pragma warning disable 0649
	[SerializeField] UIGameController UIC;
	[SerializeField] GameObject ringsGO;
	[SerializeField] GameObject mobilityPanel;
	[SerializeField] GameObject slowPanel;
	[SerializeField] int ringGoal;
	[SerializeField] float ringSpeed = 2.0f;
	[SerializeField] float rotationSpeed = 100.0f;
#pragma warning restore 0649
	int ringsCollected = 0;
	bool isMoving = true;
	bool powerInUse = false;
	float mobilityMod;
	float slowMod;


	public bool IsMoving
	{
		get { return isMoving; }
		set { isMoving = value; }
	}
	public int Collected
	{
		get { return ringsCollected; }
		set { ringsCollected = value; }
	}
	public int Goal
	{
		get { return ringGoal; }
		private set { ringGoal = value; }
	}
	public float RingSpeed
	{
		get { return ringSpeed; }
		set { ringSpeed = value; }
	}
	public float RotationSpeed
	{
		get { return rotationSpeed; }
		set { rotationSpeed = value; }
	}

	public bool RingsLeft()
	{
		return (ringGoal - ringsCollected) == 0;
	}
	public bool HasWon()
	{
		return ringsCollected == ringGoal;
	}

	void PowersToggle(string power, GameObject panel, float mod, bool powerUsed)
	{
		powerInUse = powerUsed;
		panel.SetActive(powerUsed);
		if (power == "Mobility")
		{
			RotationSpeed += powerInUse ? mod : -mod;
		}
		else
		if (power == "Slow")
		{
			RingSpeed -= powerInUse ? mod : -mod;
		}
	}

	IEnumerator MobilityCoroutine()
	{
		GlobalController.instance.Mobility--;
		PowersToggle("Mobility", mobilityPanel, mobilityMod, true);
		yield return new WaitForSeconds(5.0f);
		PowersToggle("Mobility", mobilityPanel, mobilityMod, false);
	}

	IEnumerator SlowCoroutine()
	{
		GlobalController.instance.Slow--;
		PowersToggle("Slow", slowPanel, slowMod, true);
		yield return new WaitForSeconds(5.0f);
		PowersToggle("Slow", slowPanel, slowMod, false);
	}

	public void StopRings(bool isStopped)
	{
		isMoving = !isStopped;
		Time.timeScale = isMoving ? 1 : 0;
	}

	/* Player Movement */
	public void RotateRings(Vector3 direction)
	{
		ringsGO.transform.Rotate(direction * rotationSpeed * Time.deltaTime);
	}

	/* Rings Movement */
	void MoveRings()
	{
		for (int i = 0; i < ringList.Count; i++)
		{
			ringList[i].transform.Translate(Vector3.back * ringSpeed * Time.deltaTime);
		}
	}

	void Update()
	{
		if (isMoving)
		{
			MoveRings();

			if (!HasWon())
			{
				if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
				{
					RotateRings(Vector3.forward);
				}
				if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
				{
					RotateRings(Vector3.back);
				} 
			}

			if (!powerInUse)
			{
				if (Input.GetKey(KeyCode.J) && GlobalController.instance.Mobility > 0)
				{
					Debug.LogError("Mobility");
					GlobalController.instance[1] = false;
					StopAllCoroutines();
					StartCoroutine(MobilityCoroutine());
					UIC.UpdateGameText();
				}
				if (Input.GetKey(KeyCode.K) && GlobalController.instance.Slow > 0)
				{
					Debug.LogError("Slow");
					GlobalController.instance[2] = false;
					StopAllCoroutines();
					StartCoroutine(SlowCoroutine());
					UIC.UpdateGameText();
				}
			}
			else
			{
				Debug.LogWarning("Power in Uses");
			}
		}
	}

	void Awake()
	{
		ringList = GameObjectExt.GetChildren(ringsGO);
	}

	void Start()
	{
		GlobalController.instance.LastPlayedLevel = SceneManager.GetActiveScene().buildIndex;
		Time.timeScale = 1;

		powerInUse = false;
		mobilityPanel.SetActive(false);
		slowPanel.SetActive(false);

		mobilityMod = RotationSpeed * 1.5f;
		slowMod = RingSpeed / 2;

		UIC.UpdateGameText(Collected, Goal);
	}
}





