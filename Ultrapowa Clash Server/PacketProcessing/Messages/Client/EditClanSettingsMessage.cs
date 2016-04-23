using System;
using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 14316

    internal class EditClanSettingsMessage : Message
    {
        #region Private Fields

        private int m_vAllianceBadgeData;
        private string m_vAllianceDescription;
        private int m_vAllianceOrigin;
        private int m_vAllianceType;
        private int m_vRequiredScore;
        private int m_vWarFrequency;
        private int Unknown;

        #endregion Private Fields

        #region Public Constructors

        public EditClanSettingsMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                m_vAllianceDescription = br.ReadScString();
                Unknown = br.ReadInt32WithEndian();
                m_vAllianceBadgeData = br.ReadInt32WithEndian();
                m_vAllianceType = br.ReadInt32WithEndian();
                m_vRequiredScore = br.ReadInt32WithEndian();
                m_vWarFrequency = br.ReadInt32WithEndian();
                m_vAllianceOrigin = br.ReadInt32WithEndian();
            }
        }

        public override void Process(Level level)
        {
            //Clans Edit Manager
            var alliance = ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId());
            if (alliance != null)
            {
                alliance.SetAllianceDescription(m_vAllianceDescription);
                alliance.SetAllianceBadgeData(m_vAllianceBadgeData);
                alliance.SetAllianceType(m_vAllianceType);
                alliance.SetRequiredScore(m_vRequiredScore);
                alliance.SetWarFrequency(m_vWarFrequency);
                alliance.SetAllianceOrigin(m_vAllianceOrigin);

                var avatar = level.GetPlayerAvatar();
                var allianceId = avatar.GetAllianceId();
                var eventStreamEntry = new AllianceEventStreamEntry();
                eventStreamEntry.SetId((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                eventStreamEntry.SetAvatar(avatar);
                eventStreamEntry.SetEventType(10);
                eventStreamEntry.SetAvatarId(avatar.GetId());
                eventStreamEntry.SetAvatarName(avatar.GetAvatarName());
                alliance.AddChatMessage(eventStreamEntry);

                foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                    if (onlinePlayer.GetPlayerAvatar().GetAllianceId() == allianceId)
                    {
                        var p = new AllianceStreamEntryMessage(onlinePlayer.GetClient());
                        p.SetStreamEntry(eventStreamEntry);
                        PacketManager.ProcessOutgoingPacket(p);
                        PacketManager.ProcessOutgoingPacket(new OwnHomeDataMessage(Client, level));
                        PacketManager.ProcessOutgoingPacket(new AllianceDataMessage(Client, alliance));
                    }

                DatabaseManager.Singelton.Save(alliance);
            }
        }

        #endregion Public Methods
    }
}