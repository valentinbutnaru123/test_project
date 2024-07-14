using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Common
{
	public class PasswordHasher
	{

		public static byte[] GenerateSalt()
		{
			using (var rng = new RNGCryptoServiceProvider())
			{
				byte[] salt = new byte[16]; 
				rng.GetBytes(salt);
				return salt;
			}
		}

		public static byte[] HashPassword(string password, byte[] salt)
		{
			using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
			{
				return pbkdf2.GetBytes(32); // 256-bit hash
			}
		}
		public static byte[] CombineSaltAndHash(byte[] salt, byte[] hash)
		{
			byte[] result = new byte[salt.Length + hash.Length];
			Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
			Buffer.BlockCopy(hash, 0, result, salt.Length, hash.Length);
			return result;
		}

		public static void SeparateSaltAndHash(byte[] combined, out byte[] salt, out byte[] hash)
		{
			salt = new byte[16];
			hash = new byte[32];
			Buffer.BlockCopy(combined, 0, salt, 0, salt.Length);
			Buffer.BlockCopy(combined, salt.Length, hash, 0, hash.Length);
		}

		public static bool VerifyPassword(string enteredPassword, byte[] storedSalt, byte[] storedHash)
		{
			byte[] enteredHash = HashPassword(enteredPassword, storedSalt);
			return StructuralComparisons.StructuralEqualityComparer.Equals(storedHash, enteredHash);

		}

		
	}
}
