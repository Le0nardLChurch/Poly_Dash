using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTheme
{
#pragma warning disable 0649
	[SerializeField] Color[] levelColors;
	[SerializeField] Material lvlBackgroundsMat;
	[SerializeField] Material playerMat;
#pragma warning restore 0649


	public Color[] LevelColors
	{
		get { return levelColors; }
	}
	public Material LvlBackgroundsMat
	{
		get { return lvlBackgroundsMat; }
	}
	public Material PlayerMat
	{
		get { return playerMat; }
	}
}

public class ThemeController : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] List<LevelTheme> levelThemes;
	[SerializeField] Material[] patternMats;
	[SerializeField] int patternIndex = 0;
	[SerializeField] int themeIndex = 0;
#pragma warning restore 0649


	public int ThemesCount
	{
		get { return levelThemes.Count; }
	}
	public int ThemeIndex
	{
		get { return themeIndex; }
		set { themeIndex = value; }
	}
	public int PatternIndex
	{
		get { return patternIndex; }
		set { patternIndex = value; }
	}

	public Material[] PatternMats
	{
		get { return patternMats; }
	}

	public Color[] LevelColors(int index)
	{
		return levelThemes[index].LevelColors;
	}
	public Material LvlBackgroundsMat(int index)
	{
		return levelThemes[index].LvlBackgroundsMat;
	}
	public Material PlayerMat(int index)
	{
		return levelThemes[index].PlayerMat;
	}
}





