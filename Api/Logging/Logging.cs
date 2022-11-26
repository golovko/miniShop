using System;
namespace Api.Logging
{
	public class Logging : ILogging
	{
		public Logging()
		{
		}

        public void Log(string message, LogType type)
		{
			if (type == LogType.Error)
			{
				Console.WriteLine("ERROR - " + message);
			}
			else if (type == LogType.Info)
			{
				Console.WriteLine("INFO - " + message);
			}
			else Console.WriteLine("Message - " + message); 

		}

    }
	
}

