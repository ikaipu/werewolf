using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
	public class Player : IPlayer {
		public string id { get; }
		public EnumRole role { get; set; }
		public int votedNum { get; set; }
		public void Vote(List<IPlayer> players, Func<string> SelectPlayerId)
		{
			var selectedPlayerId = SelectPlayerId();
			var votedPlayer = players.Find(player => player.id == selectedPlayerId);
			votedPlayer.votedNum++;
		}

		public Player(string id ) {
			this.id = id;
		}
	}
}
