using UnityEngine;

public class GameAttributesBehavior : MonoBehaviour
{
	public static GameAttributesBehavior Instance;

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
			Debug.LogWarning("GameAttributesBehavior.Awake() Instance attempted creation multiple times; was prevented.");
			Destroy(gameObject);
			return;
		}
	}
}
