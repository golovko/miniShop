using System;
namespace Api.Logging
{
	public interface ILogging
	{
		public void Log(string message, LogType type);

    }
}

