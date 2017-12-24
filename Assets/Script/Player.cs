using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player  {
	private string id;
	private EnumRole role;
	private int votedNum = 0;

	public Player(string id ) {
		this.id = id;
	}
	public string GetId() {
		return this.id;
	}

	public EnumRole GetRole () {
		return role;
	}

	public void SetRole (EnumRole role) {
		this.role = role;
	}

	public void Vote(List<Player> players) {
		List<Player> candidates = players.FindAll (player => player.GetId () != this.id);
		Player votedPlayer = candidates [Random.Range (0, candidates.Count)];
		Debug.Log(this.GetId() + " => " + votedPlayer.GetId());
		votedPlayer.AddVotedNum ();
	}

	public void AddVotedNum () {
		this.votedNum++;
	}

	public int GetVotedNum () {
		return this.votedNum;
	}
}
