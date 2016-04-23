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

using System;
using System.Security.Cryptography;
using Sodium.Exceptions;

namespace Sodium
{
    /// <summary>
    /// A public / private key pair.
    /// </summary>
    public class KeyPair : IDisposable
    {
        #region Private Fields

        private readonly byte[] _privateKey;
        private readonly byte[] _publicKey;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyPair"/> class.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key.</param>
        /// <exception cref="KeyOutOfRangeException"></exception>
        public KeyPair(byte[] publicKey, byte[] privateKey)
        {
            //verify that the private key length is a multiple of 16
            if (privateKey.Length % 16 != 0)
                throw new KeyOutOfRangeException("Private Key length must be a multiple of 16 bytes.");

            _publicKey = publicKey;

            _privateKey = privateKey;
            _ProtectKey();
        }

        #endregion Public Constructors

        #region Private Destructors

        ~KeyPair()
        {
            Dispose();
        }

        #endregion Private Destructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the Private Key.
        /// </summary>
        public byte[] PrivateKey
        {
            get
            {
                _UnprotectKey();
                var tmp = new byte[_privateKey.Length];
                Array.Copy(_privateKey, tmp, tmp.Length);
                _ProtectKey();

                return tmp;
            }
        }

        /// <summary>
        /// Gets or sets the Public Key.
        /// </summary>
        public byte[] PublicKey
        {
            get { return _publicKey; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Dispose of private key in memory.
        /// </summary>
        public void Dispose()
        {
            if (_privateKey != null && _privateKey.Length > 0)
                Array.Clear(_privateKey, 0, _privateKey.Length);
        }

        #endregion Public Methods

        #region Private Methods

        private void _ProtectKey()
        {
            if (!SodiumLibrary.IsRunningOnMono)
                ProtectedMemory.Protect(_privateKey, MemoryProtectionScope.SameProcess);
        }

        private void _UnprotectKey()
        {
            if (!SodiumLibrary.IsRunningOnMono)
                ProtectedMemory.Unprotect(_privateKey, MemoryProtectionScope.SameProcess);
        }

        #endregion Private Methods
    }
}