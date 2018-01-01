
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class WerewolfController : MonoBehaviour
    {
        public List<Button> ButtonList;
        private List<IPlayer> _players { get; set; }
        private readonly GameManager _gameManager;

        public WerewolfController()
        {
            var playerIds = new List<string>
            {
                "John",
                "Gong",
                "Ketchup",
                "You"
            };
            _players = playerIds.ConvertAll(id => (IPlayer) new Player(id));
            
            var roles = new List<EnumRole>
            {
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
            ButtonList.ForEach(Button => { Button.enabled = false; });
            Debug.Log("Start:");
            AssignPlayerName();
            
            _gameManager.PrepareGame();
            _gameManager.ProcessDayPhase();
            _gameManager.ProcessVotingPhase();
            ButtonList.ForEach(Button => { Button.enabled = true; });
        }

        private void AssignPlayerName()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                ButtonList[i].GetComponentInChildren<Text>().text = _players[i].id;
            }
        }

        public void OnClickPlayerButton(int index)
        {
            var you = _players.Find(player => player.id == "You");
            you.Vote(_players, () => _players[index].id);
            Debug.Log ("Vote Result:");
            _players.ForEach (player => Debug.Log(player.id + ":" + player.votedNum));
            _gameManager.ShowResult();
            
        }
    }
}