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

namespace UCS.GameFiles
{
    internal class RegionsData : Data
    {
        #region Public Constructors

        public RegionsData(CSVRow row, DataTable dt) : base(row, dt)
        {
            LoadData(this, GetType(), row);
        }

        #endregion Public Constructors

        #region Public Properties

        public string DisplayName { get; set; }
        public bool IsCountry { get; set; }
        public string Name { get; set; }
        public string TID { get; set; }

        #endregion Public Properties
    }
}