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
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UCS.Core.Crypto.Sodium.Exceptions;

namespace UCS.Core.Crypto.Sodium
{
    public partial class GenericHash
    {
        #region Public Classes

        /// <summary>
        ///     Blake2b implementation of HashAlgorithm suitable for hashing streams.
        /// </summary>
        public class GenericHashAlgorithm : HashAlgorithm
        {
            #region Private Destructors

            ~GenericHashAlgorithm()
            {
                Marshal.FreeHGlobal(hashStatePtr);
            }

            #endregion Private Destructors

            #region Public Methods

            public override void Initialize()
            {
                SodiumLibrary.hash_init(hashStatePtr, key, key.Length, bytes);
            }

            #endregion Public Methods

            #region Private Fields

            private readonly int bytes;
            private readonly IntPtr hashStatePtr;
            private readonly byte[] key;

            #endregion Private Fields

            #region Public Constructors

            /// <summary>
            ///     Initializes the hashing algorithm.
            /// </summary>
            /// <param name="key">The key; may be null, otherwise between 16 and 64 bytes.</param>
            /// <param name="bytes">The size (in bytes) of the desired result.</param>
            /// <exception cref="KeyOutOfRangeException"></exception>
            /// <exception cref="BytesOutOfRangeException"></exception>
            public GenericHashAlgorithm(string key, int bytes) : this(Encoding.UTF8.GetBytes(key), bytes)
            {
            }

            /// <summary>
            ///     Initializes the hashing algorithm.
            /// </summary>
            /// <param name="key">The key; may be null, otherwise between 16 and 64 bytes.</param>
            /// <param name="bytes">The size (in bytes) of the desired result.</param>
            /// <exception cref="KeyOutOfRangeException"></exception>
            /// <exception cref="BytesOutOfRangeException"></exception>
            public GenericHashAlgorithm(byte[] key, int bytes)
            {
                hashStatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SodiumLibrary._HashState)));

                //validate the length of the key
                int keyLength;
                if (key != null)
                {
                    if (key.Length > KEY_BYTES_MAX || key.Length < KEY_BYTES_MIN)
                    {
                        throw new KeyOutOfRangeException(
                            string.Format("key must be between {0} and {1} bytes in length.",
                                KEY_BYTES_MIN, KEY_BYTES_MAX));
                    }

                    keyLength = key.Length;
                }
                else
                {
                    key = new byte[0];
                    keyLength = 0;
                }

                this.key = key;

                //validate output length
                if (bytes > BYTES_MAX || bytes < BYTES_MIN)
                    throw new BytesOutOfRangeException("bytes", bytes,
                        string.Format("bytes must be between {0} and {1} bytes in length.", BYTES_MIN, BYTES_MAX));

                this.bytes = bytes;

                Initialize();
            }

            #endregion Public Constructors

            #region Protected Methods

            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                byte[] subArray = new byte[cbSize];
                Array.Copy(array, ibStart, subArray, 0, cbSize);
                SodiumLibrary.hash_update(hashStatePtr, subArray, cbSize);
            }

            protected override byte[] HashFinal()
            {
                byte[] buffer = new byte[bytes];
                SodiumLibrary.hash_final(hashStatePtr, buffer, bytes);
                return buffer;
            }

            #endregion Protected Methods
        }

        #endregion Public Classes
    }
}