using LaunchDarkly.Client;
using LaunchDarkly.Unity;
using UnityEngine;

public class LDPluginDemoBackgroundBehavior : MonoBehaviour
{
	public string flagName = "background-color";
	public Color flagDefault = Color.white;

	private void Awake()
	{
		ldFlagDefault = LaunchDarklyClientHelpers.ColorToLdValue(flagDefault);
		InitialBackgroundColor = Camera.main.backgroundColor;
		IntendedBackgrondColor = InitialBackgroundColor;
	}

	void Start()
	{
		LaunchDarklyClientBehavior.Instance.RegisterFeatureFlagChangedCallback(flagName, ldFlagDefault, SetBackgroundColor, true);
	}

	void Update()
	{
		if (Camera.main.backgroundColor != IntendedBackgrondColor)
		{
			Camera.main.backgroundColor = IntendedBackgrondColor;
		}
	}

	private LdValue ldFlagDefault;
	private Color InitialBackgroundColor;
	private Color IntendedBackgrondColor;

	private void SetBackgroundColor(LdValue newColor)
	{
		IntendedBackgrondColor = LaunchDarklyClientHelpers.LdValueToColor(newColor);
	}
}
