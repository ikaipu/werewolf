﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Script
{
    public class WerewolfController : MonoBehaviour
    {
        public List<Button> buttonList;
        public Text timeCounter;
        private List<IPlayer> _players { get; set; }
        private readonly GameManager _gameManager;

        public WerewolfController()
        {
            var playerIds = new List<string>
            {
                "John",
                "Gong",
                "Ketchup",
                "Mark",
                "You"
            };
            _players = playerIds.ConvertAll(id => (IPlayer) new Player(id));

            var roles = new List<EnumRole>
            {
                EnumRole.Citizen,
                EnumRole.Citizen,
                EnumRole.Citizen,
                EnumRole.Citizen,
                EnumRole.Werewolf
            };

            _gameManager = new GameManager(_players, roles);
        }

        // Use this for initialization
        private void Start()
        {
            timeCounter.text = "Ready...";
            buttonList.ForEach(Button => { Button.enabled = false; });
            StartCoroutine(TimeManageCoroutine());
        }

        private IEnumerator TimeManageCoroutine()
        {
            
            int count = 0;
            Debug.Log("Start:");
            AssignPlayerName();
            _gameManager.PrepareGame();
            buttonList.ForEach(Button => { Button.enabled = true; });
            while (true)
            {
                timeCounter.text = count.ToString();
                if(count % 10 == 0 && count != 0) {_gameManager.executePlayer();}
                
                var livingPlayers = _players.FindAll(player => !player.isDead);
                if (_players.Find(player => player.id == "You").isDead)
                {
                    buttonList.ForEach(Button => { Button.enabled = false; });
                }
                for (var i = 0; i < buttonList.Count; i++)
                {
                    if (_players[i].isDead) buttonList[i].enabled = false;
                }
                
                if (livingPlayers.FindAll(p => p.role == EnumRole.Werewolf).Count ==
                    livingPlayers.FindAll(p => p.role == EnumRole.Citizen).Count)
                {
                    Debug.Log("Werewolf Team Win!!!");
                    yield break;
                }
                if (livingPlayers.FindAll(p => p.role == EnumRole.Werewolf).Count == 0)
                {
                    Debug.Log("Citizen Team Win!!!");
                    yield break;
                }
                yield return new WaitForSeconds(1.0f);
                yield return null;
                if (count % 10 == 0)
                {
                    _players.ForEach(player => { player.voteFor = ""; });
                    _gameManager.ProcessVotingPhase();
                }
                count++;
            }
        }

        private void AssignPlayerName()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].GetComponentInChildren<Text>().text = _players[i].id;
            }
        }

        public void OnClickPlayerButton(int index)
        {
            var you = _players.Find(player => player.id == "You");
            you.Vote(_players, () => _players[index].id);
            var livingPlayers = _players.FindAll(player => !player.isDead);
            Debug.Log ("Vote Result: " + string.Join(", ", livingPlayers.ConvertAll(p => p.id + ": " + livingPlayers.Count(p2 => p2.voteFor == p.id) ).ToArray()));
        }
    }
}