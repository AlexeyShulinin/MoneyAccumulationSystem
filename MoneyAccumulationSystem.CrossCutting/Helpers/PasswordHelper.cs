using System;
using System.Security.Cryptography;
using System.Text;

namespace MoneyAccumulationSystem.CrossCutting.Helpers;

public class PasswordHelper
{
    public static string GetHashedPassword(string password)
    {
        using SHA256 hash = SHA256.Create();
        return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }
}