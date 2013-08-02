using System;
using System.Threading;

namespace TwoFactor {
	class Program {
		static void Main(string[] args) {
			/* create a worker thread with key alpha with a 
			 * one time password algorithm which rotates every
			 * 30 seconds and write the password to the console
			 * every 10 seconds. (should see same password 3 times)
			 */
			Writer writerAlpha = new Writer("alpha", 30, 10);
			Thread writerThread = new Thread(writerAlpha.WriteCurrentPasswordToConsoleLoop);
			writerThread.Start();
		}
	}

	class Writer {
		private readonly TimeBasedOneTimePassword _myPasswordGenerator;
		private readonly int WriteWait;

		public Writer(string key, int timeStep, int writeWait) {
			_myPasswordGenerator = new TimeBasedOneTimePassword(key, timeStep);
			WriteWait = writeWait;
		}

		public void WriteCurrentPasswordToConsoleLoop() {
			while (true) {
				Console.WriteLine(_myPasswordGenerator.GetCurrentPassword());
				Thread.Sleep(WriteWait * 1000);
			}
		}
	}
}
