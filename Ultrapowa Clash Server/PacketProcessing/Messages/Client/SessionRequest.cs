﻿using System.IO;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class SessionRequest : Message
    {
        public string Hash;
        public int MajorVersion;
        public int MinorVersion;
        //Packet 10100

        public int Unknown1;
        public int Unknown2;
        public int Unknown4;
        public int Unknown6;
        public int Unknown7;

        public SessionRequest(Client client, BinaryReader br) : base(client, br)
        {
        }

        public override void Decode()
        {
            using (var reader = new CoCSharpPacketReader(new MemoryStream(GetData())))
            {
                Unknown1 = reader.ReadInt32();
                Unknown2 = reader.ReadInt32();
                MajorVersion = reader.ReadInt32();
                Unknown4 = reader.ReadInt32();
                MinorVersion = reader.ReadInt32();
                Hash = reader.ReadString();
                Unknown6 = reader.ReadInt32();
                Unknown7 = reader.ReadInt32();
            }
            if (MajorVersion == 8)
                Client.CState = 1; // Clash of Clans 8.x
            else
                Client.CState = 0; // Old Clash of Clans client
        }

        public override void Process(Level level)
        {
            var authOk = new SessionSuccess(Client, this);
            PacketManager.ProcessOutgoingPacket(authOk);
        }
    }
}