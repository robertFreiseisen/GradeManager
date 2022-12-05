
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Base.Helper
{
    public static class AuthUtils
    {

        /// <summary>
        /// Überprüft, ob das übergebene Passwort unter Verwendung des Salt
        /// nach dem Hashen mit dem gespeicherten Passwort übereinstimmt.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashedSaltetPassword"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string hashedSaltetPassword)
        {
            var saltHex = hashedSaltetPassword.Substring(hashedSaltetPassword.Length - 32, 32);
            var salt = HexStringToByteArray(saltHex);
            string hashText = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashText + saltHex == hashedSaltetPassword;
        }

        /// <summary>
        /// Erzeugt zufälligen Salt, hased das Passwort mit dem Salt, fügt
        /// den Salt hinten an und liefert das Ergebnis zurück
        /// </summary>
        /// <param name="password">Passwort im Klartext</param>
        /// <returns>gesaltetes und gehashtes Passwort</returns>
        public static string GenerateHashedPassword(string password)
        {
            // https://docs.microsoft.com/de-de/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-3.1
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashText = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashText + ByteArrayToHexString(salt);
        }

        /// <summary>
        /// JWT erzeugen. Minimale Claim-Infos: Email und Rolle
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Token mit Claims</returns>
        public static string GenerateJwtToken(string email, string roleName, SigningCredentials credentials)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email)
                };
            if (!string.IsNullOrEmpty(roleName))
            {
                authClaims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            var token = new JwtSecurityToken(
                //issuer: Config["Jwt:Issuer"],
                //audience: Config["Jwt:Audience"],
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static SecurityToken ValidateToken(string authToken, string securityKey)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters(securityKey);
                IPrincipal principal = tokenHandler.ValidateToken(authToken,
                    validationParameters, out SecurityToken validatedToken);
                return validatedToken;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static TokenValidationParameters GetValidationParameters(string securityKey)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true, 
                ValidateAudience = false, 
                ValidateIssuer = false,   
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)) 
            };
        }


        public static string ByteArrayToHexString(byte[] byteArray)
        {
            var hex = new StringBuilder(byteArray.Length * 2);
            foreach (byte b in byteArray)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] HexStringToByteArray(string hexString)
        {
            byte[] byteArray = new byte[16];
            for (int i = 0; i < hexString.Length / 2; i++)
            {
                string hexByte = hexString.Substring(i * 2, 2);
                byteArray[i] = Convert.ToByte(hexByte, 16);
            }
            return byteArray;
        }

        public static string GetTokenFromAuthorizationHeader(string authorizationHeader)
        {
            if (authorizationHeader == null)
            {
                return null;
            }
            var bearerTokenElements = authorizationHeader.Split(' ');
            if (bearerTokenElements.Length != 2)
            {
                return null;
            }
            var token = bearerTokenElements[1];

            return token;
        }
    }
}
