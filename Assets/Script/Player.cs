using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
	public class Player : IPlayer {
		public string id { get; }
		public EnumRole role { get; set; }
		public string voteFor { get; set; }
		public bool isDead { get; set; }

		public void Vote(List<IPlayer> players, Func<string> SelectPlayerId)
		{
			var selectedPlayerId = SelectPlayerId();
			var votedPlayer = players.Find(player => player.id == selectedPlayerId);
			voteFor = votedPlayer.id;
		}

		public Player(string id ) {
			this.id = id;
		}
	}
}
