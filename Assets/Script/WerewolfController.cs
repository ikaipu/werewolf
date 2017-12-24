using System.Collections.Generic;
using UnityEngine;

namespace Script
{
	public class WerewolfController : MonoBehaviour {

		// Use this for initialization
		private void Start () {
			Debug.Log ("Start:");
			var gameManager = new GameManager ();
			var playerIds = new List<string> {
				"takumi",
				"john",
				"gong",
				"ketchup"
			};
			gameManager.InitPlayers (playerIds);
			var roles = new List<EnumRole> {
				EnumRole.Citizen,
				EnumRole.Citizen,
				EnumRole.Citizen,
				EnumRole.Werewolf
			};
			gameManager.AssignRoles (roles);
			gameManager.StartVotingPhase ();
			gameManager.ShowResult ();
		}
	}
}
