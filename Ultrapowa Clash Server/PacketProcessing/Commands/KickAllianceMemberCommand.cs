﻿/*
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
using System.IO;
using UCS.Core;
using UCS.Core.Network;
using UCS.Helpers;
using UCS.Logic;
using UCS.Logic.AvatarStreamEntry;
using UCS.Logic.StreamEntry;
using UCS.PacketProcessing.Messages.Server;

namespace UCS.PacketProcessing.Commands
{
    //Commande 0x21F
    internal class KickAllianceMemberCommand : Command
    {
        #region Public Constructors

        public KickAllianceMemberCommand(BinaryReader br)
        {
            m_vAvatarId = br.ReadInt64WithEndian();
            br.ReadByte();
            m_vMessage = br.ReadScString();
            br.ReadInt32WithEndian();
        }

        #endregion Public Constructors

        //00 00 02 24 20 41 6A 6B 00 00 00 01 00 00 02 1F
        //00 00 00 17 00 9E 81 01
        //01
        //00 00 00 33
        //44 C3 A9 73 6F 6C C3 A9 2C 20 6E 6F 75 73 20 61 76 6F 6E 73 20 64 C3 A9 63 69 64 C3 A9 20 64 65 20 74 27 65 78 63 6C 75 72 65 20 64 75 20 63 6C 61 6E 2E
        //00 00 01 E6

        #region Public Methods

        public override void Execute(Level level)
        {
            var targetAccount = ResourcesManager.GetPlayer(m_vAvatarId, true);
            if (targetAccount != null)
            {
                var targetAvatar = targetAccount.GetPlayerAvatar();
                var targetAllianceId = targetAvatar.GetAllianceId();
                var requesterAvatar = level.GetPlayerAvatar();
                var requesterAllianceId = requesterAvatar.GetAllianceId();
                if (requesterAllianceId > 0 && targetAllianceId == requesterAllianceId)
                {
                    var alliance = ObjectManager.GetAlliance(requesterAllianceId);
                    var requesterMember = alliance.GetAllianceMember(requesterAvatar.GetId());
                    var targetMember = alliance.GetAllianceMember(m_vAvatarId);
                    if (targetMember.HasLowerRoleThan(requesterMember.GetRole()))
                    {
                        targetAvatar.SetAllianceId(0);
                        alliance.RemoveMember(m_vAvatarId);
                        //Now sending messages
                        if (ResourcesManager.IsPlayerOnline(targetAccount))
                        {
                            var leaveAllianceCommand = new LeaveAllianceCommand();
                            leaveAllianceCommand.SetAlliance(alliance);
                            leaveAllianceCommand.SetReason(2); //kick
                            var availableServerCommandMessage =
                                new AvailableServerCommandMessage(targetAccount.GetClient());
                            availableServerCommandMessage.SetCommandId(2);
                            availableServerCommandMessage.SetCommand(leaveAllianceCommand);
                            PacketManager.ProcessOutgoingPacket(availableServerCommandMessage);

                            var kickOutStreamEntry = new AllianceKickOutStreamEntry();
                            kickOutStreamEntry.SetId(
                                (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                            kickOutStreamEntry.SetAvatar(requesterAvatar);
                            kickOutStreamEntry.SetIsNew(0);
                            kickOutStreamEntry.SetAllianceId(alliance.GetAllianceId());
                            kickOutStreamEntry.SetAllianceBadgeData(alliance.GetAllianceBadgeData());
                            kickOutStreamEntry.SetAllianceName(alliance.GetAllianceName());
                            kickOutStreamEntry.SetMessage(m_vMessage);
                            var p = new AvatarStreamEntryMessage(targetAccount.GetClient());
                            p.SetAvatarStreamEntry(kickOutStreamEntry);
                            PacketManager.ProcessOutgoingPacket(p);
                        }

                        var eventStreamEntry = new AllianceEventStreamEntry();
                        eventStreamEntry.SetId((int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                        eventStreamEntry.SetAvatar(targetAvatar);
                        eventStreamEntry.SetEventType(1);
                        eventStreamEntry.SetAvatarId(requesterAvatar.GetId());
                        eventStreamEntry.SetAvatarName(requesterAvatar.GetAvatarName());
                        alliance.AddChatMessage(eventStreamEntry);

                        foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                            if (onlinePlayer.GetPlayerAvatar().GetAllianceId() == requesterAllianceId)
                            {
                                var p = new AllianceStreamEntryMessage(onlinePlayer.GetClient());
                                p.SetStreamEntry(eventStreamEntry);
                                PacketManager.ProcessOutgoingPacket(p);
                            }
                    }
                }
            }
        }

        #endregion Public Methods

        #region Private Fields

        readonly long m_vAvatarId;
        readonly string m_vMessage;

        #endregion Private Fields
    }
}