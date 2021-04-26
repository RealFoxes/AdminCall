using System;
using Exiled.API.Features;
using Exiled.Events;
using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using Player = Exiled.Events.Handlers.Player;

namespace AdminCall
{
	public class AdminCall : Plugin<Config>
	{
		public EventHandler Handler { get; private set; }


		public override void OnEnabled()
		{
			Log.Info("AdminCall has been loaded ");
			this.Handler = new EventHandler(this);

			Exiled.Events.Handlers.Server.SendingConsoleCommand += Handler.OnSendingConsoleCommand;
			Exiled.Events.Handlers.Server.WaitingForPlayers += Handler.OnWaitingForPlayers;
		}

		public override void OnDisabled()
		{

			Exiled.Events.Handlers.Server.SendingConsoleCommand -= Handler.OnSendingConsoleCommand;
			Exiled.Events.Handlers.Server.WaitingForPlayers -= Handler.OnWaitingForPlayers;

			this.Handler = null;
		}

		public override void OnReloaded()
		{
		}
	}
}
