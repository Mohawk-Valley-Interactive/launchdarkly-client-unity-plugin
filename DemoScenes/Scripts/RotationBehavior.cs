using LaunchDarkly.Client;
using LaunchDarkly.Unity;
using UnityEngine;

public class RotationBehavior : MonoBehaviour
{
	public float speed = 1.0f;
	public string flagName = "cube-rotation-axis-2";

	void Start()
	{
		ClientBehavior.Instance.RegisterFeatureFlagChangedCallback(flagName, ClientHelpers.Vector3ToLdValue(flagDefault), onFeatureFlagChanged, true);
	}

	private void onFeatureFlagChanged(LdValue flagValue)
	{
		rotationAxis = ClientHelpers.LdValueToVector3(flagValue);
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
