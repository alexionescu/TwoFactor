using System;

namespace TwoFactor {
	class TimeBasedOneTimePassword {

		public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public string SecretKey { get; set; }
		public int TimeStep { get; set; }

		public TimeBasedOneTimePassword(string key, int timeStep) {
			SecretKey = key;
			TimeStep = timeStep;
		}

		public string GetCurrentPassword(int digits = 6) {
			return HashedOneTimePassword.GetPassword(SecretKey, GetCurrentCounter(TimeStep), digits);
		}

		private long GetCurrentCounter(int timeStep) {
			return (long) (DateTime.Now - UNIX_EPOCH).TotalSeconds / timeStep;
		}
	}
}
