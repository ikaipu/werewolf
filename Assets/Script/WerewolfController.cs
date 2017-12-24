using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Start:");
		GameManager gameManager = new GameManager ();
		List<string> playerIds = new List<string> {
			"takumi",
			"john",
			"gong",
			"ketchup"
		};
		gameManager.InitPlayers (playerIds);
		List<EnumRole> roles = new List<EnumRole> {
			EnumRole.citizen,
			EnumRole.citizen,
			EnumRole.citizen,
			EnumRole.werewolf
		};
		gameManager.AssignRoles (roles);
		gameManager.StartVotingPhase ();
		gameManager.ShowResult ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
