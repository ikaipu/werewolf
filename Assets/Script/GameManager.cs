using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script
{
	public class GameManager {
		private List<IPlayer> _players;

		public void InitPlayers (List<IPlayer> players)
		{
			_players = players;
			Debug.Log ("Players:");
			_players.ForEach (Debug.Log);
		}

		public void AssignRoles(List<EnumRole> roles) {
			var shuffledRoles = roles.Shuffle();
			for (var i = 0; i < _players.Count; i++) {
				_players [i].role = shuffledRoles[i];
			}
			Debug.Log ("Assigned Roles:");
			_players.ForEach (player => Debug.Log(player.id + ":" + player.role));
		}

		public void StartVotingPhase(){
			_players.ForEach (player => player.Vote (_players));
			Debug.Log ("Vote Result:");
			_players.ForEach (player => Debug.Log(player.id + ":" + player.votedNum));
		}
		
		public void ShowResult() {
			var maxVotedNum = _players.ConvertAll (player => player.votedNum).Max ();
			var executedPlayers = _players.FindAll (player => player.votedNum == maxVotedNum);
			Debug.Log ("ExecutedPlayers:");
			executedPlayers.ForEach (player => Debug.Log(player.id + ":" + player.role));
			var isCitiznTeamWinner = executedPlayers.Exists (player => player.role == EnumRole.Werewolf );
			Debug.Log(isCitiznTeamWinner ? "Citizen team win!!" : "Werewolf team win!!");
		}
	}
}
