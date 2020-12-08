using System;
using System.Collections.Generic;
using LaunchDarkly.Client;
using LaunchDarkly.Xamarin;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaunchDarkly.Unity
{
	public class ClientBehavior : MonoBehaviour
	{
		public static ClientBehavior Instance;

		public static bool IsInitialized
		{
			get { return ldClient != null && ldClient.Initialized; }
			private set { }
		}

		public bool isInitializedOnAwake = true;
		public double connectionTimeoutMS = 1000;
		public string mobileKey = "Please assign mobile key";
		public string userKey = "default_user_key";
		public bool isUserAnonymous = false;
		public bool automaticallyIdentifyWhenAttributesPending = true;
		public bool reloadAttributesOnSceneChange = true;

		public void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Debug.LogWarning("LaunchDarklyClientBehavior.Awake() Instance attempted creation multiple times; was prevented.");
				Destroy(gameObject);
				return;
			}

			if (isInitializedOnAwake && !Initialize())
			{
				Debug.LogError("LaunchDarklyClientBehavior.Awake() Failed to initialize LD Client Behavior.");
			}
			else if (isInitializedOnAwake == false)
			{
				Debug.LogWarning("LaunchDarklyClientBehavior.Awake() isInitializedOnAwake set to false. Initialize must be called elsewhere for client to connect.");
			}

			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		public void Update()
		{
			if (automaticallyIdentifyWhenAttributesPending && hasAttributesPending)
			{
				IdentifyUser();
			}
		}

		public bool Initialize()
		{
			if (IsInitialized)
			{
				return false;
			}

			if (mobileKey.Length == 0 || userKey.Length == 0)
			{
				Debug.LogError("User and mobile key must be defined for initialization to complete. Initialization failed.");
				return false;
			}

			Configuration ldConfiguration = Configuration.Builder(mobileKey).Build();
			RefreshUserAttributes();
			User ldUser = userBuilder.Build();
			ldClient = LdClient.Init(ldConfiguration, ldUser, System.TimeSpan.FromMilliseconds(connectionTimeoutMS));
			hasAttributesPending = false;

			ldClient.FlagChanged += OnFlagChanged;

			if(flagCallbacks.Count > 0)
			{
				foreach(string key in flagCallbacks.Keys)
				{
					foreach (CallbackInfo callback in flagCallbacks[key])
					{
						if(callback.checkAsap)
						{
							ExecuteVariationCheck(key, callback.callback, ref callback.defaultValue);
						}
					}
				}
			}

			return ldClient.Initialized;
		}

		public void TrackMetric(string eventName)
		{
			if(IsInitialized)
			{
				ldClient.Track(eventName);	
			}
			else
			{
				Debug.LogWarning("LaunchDarkly ClientBehavior.TrackMetric(string): Client not initialized, metric not tracked");
			}
		}

		public void TrackMetric(string eventName, LdValue ldValue)
		{
			if(IsInitialized)
			{
				ldClient.Track(eventName, ldValue);	
			}
			else
			{
				Debug.LogWarning("LaunchDarkly ClientBehavior.TrackMetric(string, LdValue): Client not initialized, metric not tracked");
			}
		}

		public void TrackMetric(string eventName, LdValue ldValue, double metricValue)
		{
			if(IsInitialized)
			{
				ldClient.Track(eventName, ldValue, metricValue);	
			}
			else
			{
				Debug.LogWarning("LaunchDarkly ClientBehavior.TrackMetric(string, LdValue, double): Client not initialized, metric not tracked");
			}
		}


		public void RegisterFeatureFlagChangedCallback(string flagName, LdValue valueDefault, Action<LdValue> callback, bool checkAsap)
		{
			if (!IsInitialized)
			{
				Debug.LogWarning("LauchDarklyClientBehavior.RegisterBoolFlagCallback called before initialized.");
			}

			List<CallbackInfo> callbackInfoList = null;
			if (flagCallbacks.ContainsKey(flagName))
			{
				callbackInfoList = flagCallbacks[flagName];
			}
			else
			{
				callbackInfoList = new List<CallbackInfo>();
				flagCallbacks[flagName] = callbackInfoList;
			}

			CallbackInfo callbackInfo = new CallbackInfo(valueDefault, callback, checkAsap);

			if (callbackInfoList.Contains(callbackInfo))
			{
				Debug.LogError("LaunchDarklyClientBehavior.RegisterBoolFlagCallback - duplicate callback attempted registration; process ABORTED.");
				return;
			}

			callbackInfoList.Add(callbackInfo);

			if (IsInitialized && checkAsap)
			{
				ExecuteVariationCheck(flagName, callback, ref valueDefault);
			}
		}

		public void RefreshUserAttributes()
		{
			userBuilder = User.Builder(userKey);
			userBuilder.Anonymous(isUserAnonymous);

			foreach (IUserAttributeProviderBehavior attributeProvider in GameObject.FindObjectsOfType<IUserAttributeProviderBehavior>())
			{
				attributeProvider.InjectAttributes(ref userBuilder);
			}

			hasAttributesPending = true;
		}

		public void UpdateUser(IUserAttributeProviderBehavior attributeProvider)
		{
			attributeProvider.InjectAttributes(ref userBuilder);
			hasAttributesPending = true;
		}

		public void IdentifyUser()
		{
			if (hasAttributesPending)
			{
				ldClient.Identify(userBuilder.Build(), System.TimeSpan.FromMilliseconds(connectionTimeoutMS));
				hasAttributesPending = false;
			}
		}

		public bool BoolVariation(string flagName, bool defaultValue = false)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.BoolVariation(flagName, defaultValue);
			}

			return defaultValue;
		}

		public EvaluationDetail<bool> BoolVariationDetail(string flagName, bool defaultValue = false)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.BoolVariationDetail(flagName, defaultValue);
			}

			return null;
		}

		public int IntVariation(string flagName, int defaultValue = 0)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.IntVariation(flagName, defaultValue);
			}

			return defaultValue;
		}

		public EvaluationDetail<int> IntVariationDetail(string flagName, int defaultValue = 0)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.IntVariationDetail(flagName, defaultValue);
			}

			return null;
		}

		public float FloatVariation(string flagName, float defaultValue = 0.0f)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.FloatVariation(flagName, defaultValue);
			}

			return defaultValue;
		}

		public EvaluationDetail<float> FloatVariationDetail(string flagName, float defaultValue = 0.0f)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.FloatVariationDetail(flagName, defaultValue);
			}

			return null;
		}

		public LdValue JsonVariation(string flagName, LdValue defaultValue)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.JsonVariation(flagName, defaultValue);
			}

			return defaultValue;
		}

		public EvaluationDetail<LdValue> StringVariationDetail(string flagName, LdValue defaultValue)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.JsonVariationDetail(flagName, defaultValue);
			}

			return null;
		}

		public string StringVariation(string flagName, string defaultValue)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.StringVariation(flagName, defaultValue);
			}

			return defaultValue;
		}

		public EvaluationDetail<string> StringVariationDetail(string flagName, string defaultValue)
		{
			if(ldClient != null && ldClient.Initialized)
			{
				return ldClient.StringVariationDetail(flagName, defaultValue);
			}

			return null;
		}

		private static LdClient ldClient;
		private int lastLoadedSceneIndex = 0;
		private IUserBuilder userBuilder;
		private bool hasAttributesPending = false;

		private class CallbackInfo
		{
			public LdValue defaultValue;
			public Action<LdValue> callback;
			public bool checkAsap;

			public CallbackInfo(LdValue defaultValue, Action<LdValue> callback, bool checkAsap)
			{
				this.defaultValue = defaultValue;
				this.callback = callback;
				this.checkAsap = checkAsap;
			}

			public bool Equals(CallbackInfo obj)
			{
				return this.callback == obj.callback;
			}
		}

		private Dictionary<string, List<CallbackInfo>> flagCallbacks = new Dictionary<string, List<CallbackInfo>>();

		private void ExecuteVariationCheck(string flagName, Action<LdValue> callback, ref LdValue valueDefault)
		{
			LdValue flagValue = LdValue.Null;
			switch (valueDefault.Type)
			{
				case LdValueType.Array:
				case LdValueType.Object:
					flagValue = ldClient.JsonVariation(flagName, valueDefault);
					break;
				case LdValueType.Bool:
					flagValue = LdValue.Of(ldClient.BoolVariation(flagName, valueDefault.AsBool));
					break;
				case LdValueType.Number:
					flagValue = LdValue.Of(ldClient.FloatVariation(flagName, valueDefault.AsFloat));
					break;
				case LdValueType.String:
					flagValue = LdValue.Of(ldClient.StringVariation(flagName, valueDefault.AsString));
					break;
				case LdValueType.Null:
				default:
					break;
			}

			callback(flagValue);
		}

		private void OnFlagChanged(object sender, FlagChangedEventArgs e)
		{

			if (flagCallbacks.ContainsKey(e.Key))
			{
				foreach (CallbackInfo callbackInfo in flagCallbacks[e.Key])
				{
					callbackInfo.callback(e.NewValue);
				}
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
			if(scene.buildIndex != lastLoadedSceneIndex)
			{
				lastLoadedSceneIndex = scene.buildIndex;
				if(IsInitialized && reloadAttributesOnSceneChange)
				{
					RefreshUserAttributes();
					IdentifyUser();
					flagCallbacks.Clear();
				}

			}
		}

	}
}

