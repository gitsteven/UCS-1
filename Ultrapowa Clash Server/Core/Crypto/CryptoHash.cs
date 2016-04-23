/*
 * Program : Ultrapowa Clash Server
 * Description : A C# Writted 'Clash of Clans' Server Emulator !
 *
 * Authors:  Jean-Baptiste Martin <Ultrapowa at Ultrapowa.com>,
 *           And the Official Ultrapowa Developement Team
 *
 * Copyright (c) 2016  UltraPowa
 * All Rights Reserved.
 */

using System.Text;

namespace Sodium
{
    /// <summary>
    /// Provides hashing via selected primitive.
    /// </summary>
    public class CryptoHash
    {
        #region Private Fields

        private const int SHA256_BYTES = 32;

        //pulled from various #define statements; may break with new versions
        private const int SHA512_BYTES = 64;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Hashes a string using the default algorithm (This is what you want to use)
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Hex-encoded hash.</returns>
        public static byte[] Hash(string message)
        {
            return Hash(Encoding.UTF8.GetBytes(message));
        }

        /// <summary>
        /// Hashes a byte array using the default algorithm (This is what you want to use)
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static byte[] Hash(byte[] message)
        {
            var buffer = new byte[SHA512_BYTES];
            SodiumLibrary.crypto_hash(buffer, message, message.Length);

            return buffer;
        }

        /// <summary>
        /// Hashes a string using the SHA256 algorithm
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Hex-encoded hash.</returns>
        public static byte[] Sha256(string message)
        {
            return Sha256(Encoding.UTF8.GetBytes(message));
        }

        /// <summary>
        /// Hashes a byte array using the SHA256 algorithm
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static byte[] Sha256(byte[] message)
        {
            var buffer = new byte[SHA256_BYTES];
            SodiumLibrary.crypto_hash_sha256(buffer, message, message.Length);

            return buffer;
        }

        /// <summary>
        /// Hashes a string using the SHA512 algorithm
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Hex-encoded hash.</returns>
        public static byte[] Sha512(string message)
        {
            return Sha512(Encoding.UTF8.GetBytes(message));
        }

        /// <summary>
        /// Hashes a byte array using the SHA512 algorithm
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static byte[] Sha512(byte[] message)
        {
            var buffer = new byte[SHA512_BYTES];
            SodiumLibrary.crypto_hash_sha512(buffer, message, message.Length);

            return buffer;
        }

        #endregion Public Methods
    }
}