using UnityEngine;

public class LDPluginDemoGameAttributesBehavior : MonoBehaviour
{
	public static LDPluginDemoGameAttributesBehavior Instance;

	// Start is called before the first frame update
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Debug.LogWarning("LDPluginDemoGameAttributesBehavior.Awake() Instance attempted creation multiple times; was prevented.");
			Destroy(gameObject);
			return;
		}
	}
}
