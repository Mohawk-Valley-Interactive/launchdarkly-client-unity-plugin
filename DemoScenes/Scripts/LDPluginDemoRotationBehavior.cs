using LaunchDarkly.Client;
using LaunchDarkly.Unity;
using UnityEngine;

public class LDPluginDemoRotationBehavior : MonoBehaviour
{
	public float speed = 1.0f;
	public string flagName = "cube-rotation-axis-2";

	void Start()
	{
		LaunchDarklyClientBehavior.Instance.RegisterFeatureFlagChangedCallback(flagName, LaunchDarklyClientHelpers.Vector3ToLdValue(flagDefault), onFeatureFlagChanged, true);
	}

	private void onFeatureFlagChanged(LdValue flagValue)
	{
		rotationAxis = LaunchDarklyClientHelpers.LdValueToVector3(flagValue);
	}

	void Update()
	{
		if (rotationAxis != Vector3.zero)
		{
			this.gameObject.transform.Rotate(rotationAxis, speed * Time.deltaTime);
		}
	}

	private Vector3 flagDefault = Vector3.zero;
	private Vector3 rotationAxis;
}
