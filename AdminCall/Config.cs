using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCall
{
	public sealed class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;
		public ushort DurationBroadcast { get; set; } = 10;
		public float Cooldown { get; set; } = 10f;
		public string ReplyMessage { get; set; } = "Ожидайте помощь...";
		public string CooldownMessage { get; set; } = "Извините, но вы уже использовали комманду подождите еще SEC секунд";
		public string MessageToAdmins { get; set; } = "Игрок под ником PLAYERNAME просит о помощи";
	}
}
