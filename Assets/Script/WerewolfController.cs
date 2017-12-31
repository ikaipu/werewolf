using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class WerewolfController : MonoBehaviour
    {
        public List<Button> ButtonList;

        // Use this for initialization
        private void Start()
        {
            StartCoroutine(PlayGame());
        }

        private IEnumerator PlayGame()
        {
            ButtonList.ForEach(Button => { Button.enabled = false; });
            Debug.Log("Start:");
            var playerIds = new List<string>
            {
                "Takumi",
                "John",
                "Gong",
                "Ketchup"
            };
            AssignPlayerName(playerIds);
            var players = playerIds.ConvertAll(id => (IPlayer) new Player(id));
            var roles = new List<EnumRole>
            {
                EnumRole.Citizen,
                EnumRole.Citizen,
                EnumRole.Citizen,
                EnumRole.Werewolf
            };
            
            var gameManager = new GameManager(players, roles);

            gameManager.PrepareGame();
            yield return gameManager.PlayGame();
        }

        private void AssignPlayerName(List<string> playerIds)
        {
            for (int i = 0; i < playerIds.Count; i++)
            {
                ButtonList[i].GetComponentInChildren<Text>().text = playerIds[i];
            }
            //
        }
    }
}