using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script
{
	public class GameManager {
		private List<Player> _players;

		public void InitPlayers (List<string> ids) {
			this._players = ids.ConvertAll (id => new Player (id));
			Debug.Log ("Players:");
			this._players.ForEach (player => Debug.Log (player.GetId ()));
		}

		public void AssignRoles(List<EnumRole> roles) {
			var shuffledRoles = roles.Shuffle();
			for (var i = 0; i < _players.Count; i++) {
				this._players [i].SetRole (shuffledRoles[i]);
			}
			Debug.Log ("Assigned Roles:");
			this._players.ForEach (player => Debug.Log(player.GetId() + ":" + player.GetRole()));
		}

		public void StartVotingPhase(){
			_players.ForEach (player => player.Vote (this._players));
			Debug.Log ("Vote Result:");
			this._players.ForEach (player => Debug.Log(player.GetId() + ":" + player.GetVotedNum()));
		}
		
		public void ShowResult() {
			var maxVotedNum = _players.ConvertAll (player => player.GetVotedNum ()).Max ();
			var executedPlayers = _players.FindAll (player => player.GetVotedNum () == maxVotedNum);
			Debug.Log ("ExecutedPlayers:");
			executedPlayers.ForEach (player => Debug.Log(player.GetId() + ":" + player.GetRole()));
			var isCitiznTeamWinner = executedPlayers.Exists (player => player.GetRole() == EnumRole.Werewolf );
			Debug.Log(isCitiznTeamWinner ? "Citizen team win!!" : "Werewolf team win!!");
		}
	}
}
