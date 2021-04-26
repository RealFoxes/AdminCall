using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MEC;
using UnityEngine;
using static Broadcast;

namespace AdminCall
{
	public sealed class EventHandler
	{
		public Dictionary<string, float> PlayersCooldown { get; set; }
		public EventHandler(AdminCall plugin)
		{
			this.Plugin = plugin;
		}
		public void OnWaitingForPlayers()
		{
			PlayersCooldown = new Dictionary<string, float>();
		}
		public void OnSendingConsoleCommand(SendingConsoleCommandEventArgs ev)
		{
			if (ev.Name.StartsWith("call"))
			{
				if (PlayersCooldown.TryGetValue(ev.Player.UserId, out float EndOfCooldown))
				{
					if (EndOfCooldown > Time.time)
					{
						ev.ReturnMessage = Plugin.Config.CooldownMessage.Replace("SEC", (EndOfCooldown - Time.time).ToString());
						return;
					}
					PlayersCooldown.Remove(ev.Player.UserId);
				}

				ev.ReturnMessage = Plugin.Config.ReplyMessage;
				PlayersCooldown.Add(ev.Player.UserId, Time.time+Plugin.Config.Cooldown);

				string msgToAdm = Plugin.Config.MessageToAdmins
					.Replace("PLAYERNAME", ev.Player.Nickname)
					.Replace("MESSAGE", System.String.Join(" ",ev.Arguments));
				Log.Info(msgToAdm);
				foreach (Player player in Player.List)
				{
					if (player.RemoteAdminAccess) 
						player.Broadcast(Plugin.Config.DurationBroadcast, msgToAdm);
				}

			}
		}


		public AdminCall Plugin;
	}
}
