using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework.Internal;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Script
{
	public class GameManager {
		private List<IPlayer> _players { get; set; }
		private List<EnumRole> _roles { get; set; }

		public GameManager(List<IPlayer> players, List<EnumRole> roles)
		{
			_players = players;
			_roles = roles;
		}
		
		public void PrepareGame() {
			InitPlayers(_players);
			AssignRoles(_roles);
		}

		public void ProcessDayPhase()
		{
			
		}
		
		public void ShowResult() {
			var maxVotedNum = _players.ConvertAll (player => player.votedNum).Max ();
			var executedPlayers = _players.FindAll (player => player.votedNum == maxVotedNum);
			Debug.Log ("ExecutedPlayers:");
			executedPlayers.ForEach (player => Debug.Log(player.id + ":" + player.role));
			var isCitiznTeamWinner = executedPlayers.Exists (player => player.role == EnumRole.Werewolf );
			Debug.Log(isCitiznTeamWinner ? "Citizen team win!!" : "Werewolf team win!!");
		}

		public void ProcessVotingPhase()
		{
			var enemies = _players.FindAll(player => player.id != "You");
			enemies.ForEach(enemy => Debug.Log(enemy));

			foreach (IPlayer enemy in enemies)
			{
				Func<string> RandomSelectPlayer = () =>
				{
					var candidates = _players.FindAll(player => player.id != enemy.id);
					return candidates [Random.Range (0, candidates.Count)].id;
				};
				enemy.Vote(_players, RandomSelectPlayer);
			}
			Debug.Log ("Vote Result:");
			_players.ForEach (player => Debug.Log(player.id + ":" + player.votedNum));
		}

		private void InitPlayers (List<IPlayer> players)
		{
			_players = players;
			Debug.Log ("Players:");
			_players.ForEach (player => Debug.Log(player.id));
		}

		private void AssignRoles(List<EnumRole> roles) {
			var shuffledRoles = roles.Shuffle();
			for (var i = 0; i < _players.Count; i++) {
				_players [i].role = shuffledRoles[i];
			}
			Debug.Log ("Assigned Roles:");
			_players.ForEach (player => Debug.Log(player.id + ":" + player.role));
		}
	}
}
