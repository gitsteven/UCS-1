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

using System.Security.Cryptography;
using System.Text;
using Sodium.Exceptions;

namespace Sodium
{
    /// <summary>
    /// Create and Open SealedPublicKeyBoxes.
    /// </summary>
    public static class SealedPublicKeyBox
    {
        #region Public Fields

        public const int RecipientPublicKeyBytes = 32;
        public const int RecipientSecretKeyBytes = 32;

        #endregion Public Fields

        #region Private Fields

        private const int CryptoBoxSealbytes = 48;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Creates a SealedPublicKeyBox
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="recipientKeyPair">
        /// The recipientKeyPair key pair (uses only the public key).
        /// </param>
        /// <returns>The anonymously encrypted message.</returns>
        /// <exception cref="KeyOutOfRangeException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static byte[] Create(string message, KeyPair recipientKeyPair)
        {
            return Create(Encoding.UTF8.GetBytes(message), recipientKeyPair.PublicKey);
        }

        /// <summary>
        /// Creates a SealedPublicKeyBox
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="recipientKeyPair">
        /// The recipientKeyPair key pair (uses only the public key).
        /// </param>
        /// <returns>The anonymously encrypted message.</returns>
        /// <exception cref="KeyOutOfRangeException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static byte[] Create(byte[] message, KeyPair recipientKeyPair)
        {
            return Create(message, recipientKeyPair.PublicKey);
        }

        /// <summary>
        /// Creates a SealedPublicKeyBox
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="recipientPublicKey">The 32 byte recipient's public key.</param>
        /// <returns>The anonymously encrypted message.</returns>
        /// <exception cref="KeyOutOfRangeException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static byte[] Create(string message, byte[] recipientPublicKey)
        {
            return Create(Encoding.UTF8.GetBytes(message), recipientPublicKey);
        }

        /// <summary>
        /// Creates a SealedPublicKeyBox
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="recipientPublicKey">The 32 byte recipient's public key.</param>
        /// <returns>The anonymously encrypted message.</returns>
        /// <exception cref="KeyOutOfRangeException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static byte[] Create(byte[] message, byte[] recipientPublicKey)
        {
            //validate the length of the recipient public key
            if (recipientPublicKey == null || recipientPublicKey.Length != RecipientPublicKeyBytes)
                throw new KeyOutOfRangeException("recipientPublicKey",
                    (recipientPublicKey == null) ? 0 : recipientPublicKey.Length,
                    string.Format("recipientPublicKey must be {0} bytes in length.", RecipientPublicKeyBytes));

            var buffer = new byte[message.Length + CryptoBoxSealbytes];
            var ret = SodiumLibrary.crypto_box_seal(buffer, message, message.Length, recipientPublicKey);

            if (ret != 0)
                throw new CryptographicException("Failed to create SealedBox");

            return buffer;
        }

        /// <summary>
        /// Opens a SealedPublicKeyBox
        /// </summary>
        /// <param name="cipherText">Hex-encoded cipherText to be opened.</param>
        /// <param name="recipientKeyPair">The recipient's key pair.</param>
        /// <returns>The decrypted message.</returns>
        /// <exception cref="KeyOutOfRangeException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static byte[] Open(string cipherText, KeyPair recipientKeyPair)
        {
            return Open(Utilities.HexToBinary(cipherText), recipientKeyPair.PrivateKey, recipientKeyPair.PublicKey);
        }

        /// <summary>
        /// Opens a SealedPublicKeyBox
        /// </summary>
        /// <param name="cipherText">The cipherText to be opened.</param>
        /// <param name="recipientKeyPair">The recipient's key pair.</param>
        /// <returns>The decrypted message.</returns>
        /// <exception cref="KeyOutOfRangeException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static byte[] Open(byte[] cipherText, KeyPair recipientKeyPair)
        {
            return Open(cipherText, recipientKeyPair.PrivateKey, recipientKeyPair.PublicKey);
        }

        /// <summary>
        /// Opens a SealedPublicKeyBox
        /// </summary>
        /// <param name="cipherText">Hex-encoded cipherText to be opened.</param>
        /// <param name="recipientSecretKey">The recipient's secret key.</param>
        /// <param name="recipientPublicKey">The recipient's public key.</param>
        /// <returns>The decrypted message.</returns>
        /// <exception cref="KeyOutOfRangeException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static byte[] Open(string cipherText, byte[] recipientSecretKey, byte[] recipientPublicKey)
        {
            return Open(Utilities.HexToBinary(cipherText), recipientSecretKey, recipientPublicKey);
        }

        /// <summary>
        /// Opens a SealedPublicKeyBox
        /// </summary>
        /// <param name="cipherText">The cipherText to be opened.</param>
        /// <param name="recipientSecretKey">The recipient's secret key.</param>
        /// <param name="recipientPublicKey">The recipient's public key.</param>
        /// <returns>The decrypted message.</returns>
        /// <exception cref="KeyOutOfRangeException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public static byte[] Open(byte[] cipherText, byte[] recipientSecretKey, byte[] recipientPublicKey)
        {
            //validate the length of the recipient secret key
            if (recipientSecretKey == null || recipientSecretKey.Length != RecipientSecretKeyBytes)
                throw new KeyOutOfRangeException("recipientPublicKey",
                    (recipientSecretKey == null) ? 0 : recipientSecretKey.Length,
                    string.Format("recipientSecretKey must be {0} bytes in length.", RecipientSecretKeyBytes));

            //validate the length of the recipient public key
            if (recipientPublicKey == null || recipientPublicKey.Length != RecipientPublicKeyBytes)
                throw new KeyOutOfRangeException("recipientPublicKey",
                    (recipientPublicKey == null) ? 0 : recipientPublicKey.Length,
                    string.Format("recipientPublicKey must be {0} bytes in length.", RecipientPublicKeyBytes));

            var buffer = new byte[cipherText.Length - CryptoBoxSealbytes];
            var ret = SodiumLibrary.crypto_box_seal_open(buffer, cipherText, cipherText.Length, recipientPublicKey,
                recipientSecretKey);

            if (ret != 0)
                throw new CryptographicException("Failed to open SealedBox");

            return buffer;
        }

        #endregion Public Methods
    }
}