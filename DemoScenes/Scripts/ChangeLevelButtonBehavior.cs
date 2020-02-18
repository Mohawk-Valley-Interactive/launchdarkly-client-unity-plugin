﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelButtonBehavior : MonoBehaviour
{
	public string targetSceneName;
	public void OnClick()
	{
		if(targetSceneName != null || targetSceneName.Length == 0)
		{
			SceneManager.LoadScene(targetSceneName);
		}
		else
		{
			Debug.LogWarning("ChangeLevelButtonBehavior.OnClick() - targetSceneName not initialized.");
		}
	}
}
