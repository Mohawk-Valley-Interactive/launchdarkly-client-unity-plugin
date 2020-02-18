using Common.Logging;
using UnityEngine;

namespace LaunchDarkly.Unity
{
	public class LoggerBehavior : MonoBehaviour
	{
		public LogLevel logLevel = LogLevel.Info;

		public void Awake()
		{
			LogManager.Adapter = new LoggerFactoryAdapter(logLevel);
		}

		public ILoggerFactoryAdapter GetLoggerFactoryAdapter()
		{
			return loggerFactoryAdapter;
		}

		private ILoggerFactoryAdapter loggerFactoryAdapter = null;
	}
}
