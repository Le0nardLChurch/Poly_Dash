using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class EndlessController : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] ThemeController TC;
	[SerializeField] GameController GC;
	[SerializeField] RingSpawner RS;
	[SerializeField] SkinnedMeshRenderer playerSkin;
	[SerializeField] ParticleSystemRenderer deathParticles;
#pragma warning restore 0649
	List<Material[]> ringCurrentMaterials;
	int matIndex = 0;
	int themeIndex = 0;


	public void SetMaterials()
	{
		TC.PatternIndex = (matIndex < TC.PatternMats.Length - 1) ? matIndex += 1 : matIndex = 0;
		TC.ThemeIndex = (themeIndex < TC.ThemesCount - 1) ? themeIndex += 1 : themeIndex = 0;

		RS.RingColors = TC.LevelColors(themeIndex);
		RS.GetComponent<Renderer>().material = TC.LvlBackgroundsMat(themeIndex);
		playerSkin.material = TC.PlayerMat(themeIndex);
		deathParticles.material = TC.PlayerMat(themeIndex);

		GC.RingSpeed += 0.25f;
		GC.RotationSpeed += 12.5f;
	}

	void Start()
	{
		matIndex = TC.PatternIndex;
		themeIndex = TC.ThemeIndex;
	}
}





