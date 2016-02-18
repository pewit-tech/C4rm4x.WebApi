﻿#region Using

using System;
using System.IdentityModel.Tokens;

#endregion

namespace C4rm4x.WebApi.Security.Jwt
{
    /// <summary>
    /// Represents the cryptographic key and security algorithms that are used to generate
    /// a digital signature using Hash-based message authentication code (HMAC)
    /// </summary>
    internal class HmacSigningCredentials : SigningCredentials
    {
        private class Algorithms
        {
            public const string HmacSha256Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
            public const string HmacSha384Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";
            public const string HmacSha512Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

            public const string Sha256Digest = "http://www.w3.org/2001/04/xmlenc#sha256";
            public const string Sha384Digest = "http://www.w3.org/2001/04/xmlenc#sha384";
            public const string Sha512Digest = "http://www.w3.org/2001/04/xmlenc#sha512";
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="base64EncodedKey">The key encoded using base-64 encode</param>
        public HmacSigningCredentials(string base64EncodedKey)
            : this(Convert.FromBase64String(base64EncodedKey))
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">They key as a byte array</param>
        public HmacSigningCredentials(byte[] key)
            : base(new InMemorySymmetricSecurityKey(key),
                    CreateSignatureAlgorithm(key),
                    CreateDigestAlgorithm(key))
        { }

        private static string CreateSignatureAlgorithm(byte[] key)
        {
            switch (key.Length)
            {
                case 32:
                    return Algorithms.HmacSha256Signature;
                case 48:
                    return Algorithms.HmacSha384Signature;
                case 64:
                    return Algorithms.HmacSha512Signature;
                default:
                    throw new InvalidOperationException("Unsupported key length");
            }
        }

        private static string CreateDigestAlgorithm(byte[] key)
        {
            switch (key.Length)
            {
                case 32:
                    return Algorithms.Sha256Digest;
                case 48:
                    return Algorithms.Sha384Digest;
                case 64:
                    return Algorithms.Sha512Digest;
                default:
                    throw new InvalidOperationException("Unsupported key length");
            }
        }
    }
}
