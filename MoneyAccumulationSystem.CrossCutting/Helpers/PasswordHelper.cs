using System;
using System.Security.Cryptography;
using System.Text;

namespace MoneyAccumulationSystem.CrossCutting.Helpers;

public static class PasswordHelper
{
    public static string GetHashedPassword(string password)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}