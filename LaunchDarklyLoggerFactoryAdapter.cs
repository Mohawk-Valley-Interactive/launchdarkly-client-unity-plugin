using Common.Logging;
using Common.Logging.Factory;
using System;

namespace LaunchDarkly.Unity
{
public class LaunchDarklyLoggerFactoryAdapter : AbstractCachingLoggerFactoryAdapter
{
	public class Logger : AbstractLogger
	{
		public Logger(string loggerName, LogLevel useLogLevel)
		{
			_loggerName = loggerName;
			_useLogLevel = useLogLevel;
		}

		protected override void WriteInternal(LogLevel level, object message, Exception exception)
		{
			if (exception != null)
			{
				UnityEngine.Debug.Log("LD+Unity Logging from " + _loggerName + ": " + message.ToString() + " " + exception);
			}
			else
			{
				UnityEngine.Debug.Log("LD+Unity Logging from " + _loggerName + ": " + message.ToString());
			}
		}

		public override bool IsTraceEnabled
		{
			get { return (_useLogLevel <= LogLevel.Trace); }
		}

		public override bool IsDebugEnabled
		{
			get { return (_useLogLevel <= LogLevel.Debug); }
		}

		public override bool IsInfoEnabled
		{
			get { return (_useLogLevel <= LogLevel.Info); }
		}

		public override bool IsWarnEnabled
		{
			get { return (_useLogLevel <= LogLevel.Warn); }
		}

		public override bool IsErrorEnabled
		{
			get { return (_useLogLevel <= LogLevel.Error); }
		}

		public override bool IsFatalEnabled
		{
			get { return (_useLogLevel <= LogLevel.Fatal); }
		}

		private string _loggerName;
		private LogLevel _useLogLevel;
	}

	private readonly LogLevel _useLogLevel;

	public LaunchDarklyLoggerFactoryAdapter(LogLevel useLogLevel) : base(false)
	{
		_useLogLevel = useLogLevel;
	}

	protected override ILog CreateLogger(string name)
	{
		return new Logger(name, _useLogLevel);
	}

}
}