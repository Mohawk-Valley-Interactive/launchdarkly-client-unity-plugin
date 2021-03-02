using LaunchDarkly.Client;
using System.Collections.Generic;
using UnityEngine;

namespace LaunchDarkly.Unity
{
	public class LaunchDarklyClientHelpers
	{
		public static Vector3 LdValueToVector3(LdValue ldVector3)
		{
			IReadOnlyDictionary<string, float> vector3Dictionary = ldVector3.AsDictionary(LdValue.Convert.Float);
			Vector3 vec3 = Vector3.zero;
			vec3.x = vector3Dictionary.ContainsKey("x") ? vector3Dictionary["x"] : 0.0f;
			vec3.y = vector3Dictionary.ContainsKey("y") ? vector3Dictionary["y"] : 0.0f;
			vec3.z = vector3Dictionary.ContainsKey("z") ? vector3Dictionary["z"] : 0.0f;

			return vec3;
		}

		public static LdValue Vector3ToLdValue(Vector3 vec3)
		{
			Dictionary<string, LdValue> vector3Dictionary = new Dictionary<string, LdValue>(3);
			vector3Dictionary["x"] = LdValue.Of(vec3.x);
			vector3Dictionary["y"] = LdValue.Of(vec3.y);
			vector3Dictionary["z"] = LdValue.Of(vec3.z);

			return LdValue.Convert.Json.ObjectFrom(vector3Dictionary);
		}

		public static Color LdValueToColor(LdValue ldColor)
		{
			IReadOnlyDictionary<string, float> colorDictionary = ldColor.AsDictionary(LdValue.Convert.Float);
			Color color = new Color();
			color.r = colorDictionary.ContainsKey("r") ? colorDictionary["r"] : 0.0f;
			color.g = colorDictionary.ContainsKey("g") ? colorDictionary["g"] : 0.0f;
			color.b = colorDictionary.ContainsKey("b") ? colorDictionary["b"] : 0.0f;
			color.a = colorDictionary.ContainsKey("a") ? colorDictionary["a"] : 1.0f;

			return color;
		}

		public static LdValue ColorToLdValue(Color color)
		{
			Dictionary<string, LdValue> colorDictionary = new Dictionary<string, LdValue>(4);
			colorDictionary["r"] = LdValue.Of(color.r);
			colorDictionary["g"] = LdValue.Of(color.g);
			colorDictionary["b"] = LdValue.Of(color.b);
			colorDictionary["a"] = LdValue.Of(color.a);

			return LdValue.Convert.Json.ObjectFrom(colorDictionary);
		}
	}
}
