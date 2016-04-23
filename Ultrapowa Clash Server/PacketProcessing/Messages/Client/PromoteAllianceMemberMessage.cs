using System;
using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 14306

    internal class PromoteAllianceMemberMessage : Message
    {
        #region Public Fields

        public long m_vId;
        public int m_vRole;

        #endregion Public Fields

        #region Public Constructors

        public PromoteAllianceMemberMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                m_vId = br.ReadInt64WithEndian();
                m_vRole = br.ReadInt32WithEndian();
            }
        }

        public override void Process(Level level)
        {
            var target = ResourcesManager.GetPlayer(m_vId);
            var player = level.GetPlayerAvatar();
            var alliance = ObjectManager.GetAlliance(player.GetAllianceId());
            if (player.GetAllianceRole() == 2 || player.GetAllianceRole() == 4)
                if (player.GetAllianceId() == target.GetPlayerAvatar().GetAllianceId())
                {
                    target.GetPlayerAvatar().SetAllianceRole(m_vRole);
                    if (m_vRole == 2)
                    {
                        player.SetAllianceRole(4);
                        var eventStreamEntry = new AllianceEventStreamEntry();
                        eventStreamEntry.SetId((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                        eventStreamEntry.SetAvatar(player);
                        eventStreamEntry.SetEventType(6);
                        eventStreamEntry.SetAvatarId(target.GetPlayerAvatar().GetId());
                        eventStreamEntry.SetAvatarName(target.GetPlayerAvatar().GetAvatarName());
                        alliance.AddChatMessage(eventStreamEntry);
                        foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                            if (onlinePlayer.GetPlayerAvatar().GetAllianceId() == target.GetPlayerAvatar().GetAllianceId())
                            {
                                var p = new AllianceStreamEntryMessage(onlinePlayer.GetClient());
                                p.SetStreamEntry(eventStreamEntry);
                                PacketManager.ProcessOutgoingPacket(p);
                            }
                    }
                }
            // PacketManager.ProcessOutgoingPacket(new AllianceDataMessage(Client, alliance));
            PacketManager.ProcessOutgoingPacket(new PromoteAllianceMemberOkMessage(Client, target, this));
        }

        #endregion Public Methods
    }
}