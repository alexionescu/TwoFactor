using System;
using System.Text;
using System.Security.Cryptography;

namespace TwoFactor {
	public static class HashedOneTimePassword {

		public static string GetPassword(string secret, long iteration, int digits = 6) {
			byte[] counter = BitConverter.GetBytes(iteration);
			if (BitConverter.IsLittleEndian) {
				Array.Reverse(counter);
			}
			byte[] key = Encoding.ASCII.GetBytes(secret);
			HMACSHA1 hmac = new HMACSHA1(key, true);
			byte[] hash = hmac.ComputeHash(counter);
			int offset = hash[hash.Length - 1] & 0xf;
			int binary = ((hash[offset] & 0x7f) << 24) |
			             (hash[offset + 1] << 16) |
			             (hash[offset + 2] << 8) |
			             (hash[offset + 3]);

			//get password to requested # of digits
			int password = binary % (int)Math.Pow(10, digits);
			return password.ToString(new string('0', digits));
		}
	}
}
