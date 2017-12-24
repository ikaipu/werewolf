using System.Collections.Generic;
using UnityEngine;

namespace Script
{
	public class Player  {
		private readonly string _id;
		private EnumRole _role;
		private int _votedNum = 0;

		public Player(string id ) {
			this._id = id;
		}
		public string GetId() {
			return this._id;
		}

		public EnumRole GetRole () {
			return _role;
		}

		public void SetRole (EnumRole role) {
			this._role = role;
		}

		public void Vote(List<Player> players) {
			var candidates = players.FindAll (player => player.GetId () != this._id);
			var votedPlayer = candidates [Random.Range (0, candidates.Count)];
			Debug.Log(this.GetId() + " => " + votedPlayer.GetId());
			votedPlayer.AddVotedNum ();
		}

		private void AddVotedNum () {
			this._votedNum++;
		}

		public int GetVotedNum () {
			return this._votedNum;
		}
	}
}
