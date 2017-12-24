using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager {
	private List<Player> players;

	public void InitPlayers (List<string> ids) {
		this.players = ids.ConvertAll (id => new Player (id));
		Debug.Log ("Players:");
		this.players.ForEach (player => Debug.Log (player.GetId ()));
	}

	public void AssignRoles(List<EnumRole> roles) {
		List<EnumRole> shuffledRoles = roles.Shuffle();
		for (int i = 0; i < players.Count; i++) {
			this.players [i].SetRole (shuffledRoles[i]);
		}
		Debug.Log ("Assigned Roles:");
		this.players.ForEach (player => Debug.Log(player.GetId() + ":" + player.GetRole()));
	}

	public void StartVotingPhase(){
		players.ForEach (player => player.Vote (this.players));
		Debug.Log ("Vote Result:");
		this.players.ForEach (player => Debug.Log(player.GetId() + ":" + player.GetVotedNum()));
	}
		
	public void ShowResult() {
		int MaxVotedNum = players.ConvertAll (player => player.GetVotedNum ()).Max ();
		List<Player> executedPlayers = players.FindAll (player => player.GetVotedNum () == MaxVotedNum);
		Debug.Log ("ExecutedPlayers:");
		executedPlayers.ForEach (player => Debug.Log(player.GetId() + ":" + player.GetRole()));
		bool isCitiznTeamWinner = executedPlayers.Exists (player => player.GetRole() == EnumRole.werewolf );
		if (isCitiznTeamWinner) {
			Debug.Log ("Citizen team win!!");
		} else {
			Debug.Log ("Werewolf team win!!");
		}
	}
}
