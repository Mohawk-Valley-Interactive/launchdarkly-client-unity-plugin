using UnityEngine;
using UnityEngine.SceneManagement;

public class LDPluginDemoChangeLevelButtonBehavior : MonoBehaviour
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
			Debug.LogWarning("LDPluginDemoChangeLevelButtonBehavior.OnClick() - targetSceneName not initialized.");
		}
	}
}
