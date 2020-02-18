using LaunchDarkly.Client;
using System;
using UnityEngine;

namespace LaunchDarkly.Unity
{
	public abstract class IUserAttributeProviderBehavior : MonoBehaviour
	{
		[Serializable]
		public class StringAttribute
		{
			public bool isSet = false;
			public bool isPrivate = false;
			public String value;
		}

		public abstract void InjectAttributes(ref IUserBuilder userBuilder);
	}
}

