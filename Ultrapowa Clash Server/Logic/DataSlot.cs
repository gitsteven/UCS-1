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

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UCS.Core;
using UCS.GameFiles;
using UCS.Helpers;

namespace UCS.Logic
{
    internal class DataSlot
    {
        #region Public Fields

        public Data Data;
        public int Value;

        #endregion Public Fields

        #region Public Constructors

        public DataSlot(Data d, int value)
        {
            Data = d;
            Value = value;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Decode(BinaryReader br)
        {
            Data = br.ReadDataReference();
            Value = br.ReadInt32WithEndian();
        }

        public byte[] Encode()
        {
            var data = new List<byte>();
            data.AddInt32(Data.GetGlobalID());
            data.AddInt32(Value);
            return data.ToArray();
        }

        public void Load(JObject jsonObject)
        {
            Data = ObjectManager.DataTables.GetDataById(jsonObject["global_id"].ToObject<int>());
            Value = jsonObject["value"].ToObject<int>();
        }

        public JObject Save(JObject jsonObject)
        {
            jsonObject.Add("global_id", Data.GetGlobalID());
            jsonObject.Add("value", Value);
            return jsonObject;
        }

        #endregion Public Methods
    }
}