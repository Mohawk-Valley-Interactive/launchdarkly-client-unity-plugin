using Common.Logging;
using UnityEngine;

namespace LaunchDarkly.Unity
{
	public class LaunchDarklyLoggerBehavior : MonoBehaviour
	{
		public LogLevel logLevel = LogLevel.Info;

		public void Awake()
		{
			LogManager.Adapter = new LaunchDarklyLoggerFactoryAdapter(logLevel);
		}

		public ILoggerFactoryAdapter GetLoggerFactoryAdapter()
		{
			return loggerFactoryAdapter;
		}

		private ILoggerFactoryAdapter loggerFactoryAdapter = null;
	}
}
