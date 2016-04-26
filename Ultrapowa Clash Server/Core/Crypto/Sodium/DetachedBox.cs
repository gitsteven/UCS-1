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

namespace UCS.Core.Crypto.Sodium
{
    /// <summary>
    ///     A ciphertext / mac pair.
    /// </summary>
    public class DetachedBox
    {
        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DetachedBox" /> class.
        /// </summary>
        public DetachedBox()
        {
            //do nothing
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DetachedBox" /> class.
        /// </summary>
        /// <param name="cipherText">The cipher.</param>
        /// <param name="mac">The 16 byte mac.</param>
        public DetachedBox(byte[] cipherText, byte[] mac)
        {
            CipherText = cipherText;
            Mac = mac;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     Gets or sets the Cipher.
        /// </summary>
        public byte[] CipherText { get; set; }

        /// <summary>
        ///     Gets or sets the MAC.
        /// </summary>
        public byte[] Mac { get; set; }

        #endregion Public Properties
    }
}