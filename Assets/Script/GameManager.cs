using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.XR.WSA;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Script
{
	public class GameManager {
		private List<IPlayer> _players { get; set; }
		private List<EnumRole> _roles { get; }

		public GameManager(List<IPlayer> players, List<EnumRole> roles)
		{
			_players = players;
			_roles = roles;
		}
		
		public void PrepareGame() {
			InitPlayers(_players);
			AssignRoles(_roles);
		}
		
		public void ShowResult() {
//			var maxVotedNum = _players.ConvertAll (player => player.votedNum).Max ();
//			var executedPlayers = _players.FindAll (player => player.votedNum == maxVotedNum);
//			Debug.Log ("ExecutedPlayers:");
//			executedPlayers.ForEach (player => Debug.Log(player.id + ":" + player.role));
//			var isCitiznTeamWinner = executedPlayers.Exists (player => player.role == EnumRole.Werewolf );
//			Debug.Log(isCitiznTeamWinner ? "Citizen team win!!" : "Werewolf team win!!");
		}

		public void ProcessVotingPhase()
		{
			var livingPlayers = _players.FindAll(player => !player.isDead);
			var enemies = livingPlayers.FindAll(player => player.id != "You");
			enemies.ForEach(enemy => Debug.Log(enemy));

			foreach (IPlayer enemy in enemies)
			{
				Func<string> RandomSelectPlayer = () =>
				{
					var candidates = livingPlayers.FindAll(player => player.id != enemy.id);
					return candidates [Random.Range (0, candidates.Count)].id;
				};
				enemy.Vote(livingPlayers, RandomSelectPlayer);
			}
			Debug.Log("Vote: " + string.Join(", ", _players.ConvertAll(p => p.id + " => " + p.voteFor).ToArray()));
			Debug.Log ("Vote Result: " + string.Join(", ", livingPlayers.ConvertAll(p => p.id + ": " + livingPlayers.Count(p2 => p2.voteFor == p.id) ).ToArray()));
		}

		public void executePlayer()
		{
			var idMapVoteNum = _players.ToDictionary(p => p.id, p => _players.Count(p2 => p2.voteFor == p.id));
			var votedPlayersDic = idMapVoteNum.Where(e => e.Value == idMapVoteNum.Values.Max()).ToDictionary(e => e.Key, e => e.Value);
			
			if (votedPlayersDic.Count == 1)
			{
				var votedPlayerId = _players.Find(player => player.id == votedPlayersDic.First().Key);
				votedPlayerId.isDead = true;
				Debug.Log("Player: \"" + votedPlayerId + "\" was Executed.");
			}
			else
			{
				Debug.Log("No one was Executed.");
			}
		}

		private void InitPlayers (List<IPlayer> players)
		{
			_players = players;
			Debug.Log("Players: " + string.Join(", ", _players.ConvertAll(p => p.id).ToArray()));
			_players.ForEach (player =>
			{
				player.isDead = false;
			});
		}

		private void AssignRoles(List<EnumRole> roles) {
			var shuffledRoles = roles.Shuffle();
			for (var i = 0; i < _players.Count; i++) {
				_players [i].role = shuffledRoles[i];
			}
			Debug.Log ("Assigned Roles: " + string.Join(", ", _players.ConvertAll(p => p.role.ToString()).ToArray()));
		}
	}
}
