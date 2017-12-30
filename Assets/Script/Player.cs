using System.Collections.Generic;
using UnityEngine;

namespace Script
{
	public class Player : IPlayer {
		public string id { get; }
		public EnumRole role { get; set; }
		public int votedNum { get; private set; }

		public Player(string id ) {
			this.id = id;
		}

		public void Vote(List<IPlayer> players) {
			var candidates = players.FindAll (player => player.id != id);
			var votedPlayer = candidates [Random.Range (0, candidates.Count)];
			Debug.Log(id + " => " + votedPlayer.id);
			votedNum ++;
		}

	}
}
