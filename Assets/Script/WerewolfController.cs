using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
                if (count % 20 == 0)
                {
                    if(count != 0) {_gameManager.executePlayer();}
                    _players.ForEach(player => { player.votedNum = 0; });
                    _gameManager.ProcessVotingPhase();
                }

                timeCounter.text = count.ToString();
                count++;
                yield return new WaitForSeconds(1.0f);
                yield return null;
            }
        }

        private void AssignPlayerName()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                buttonList[i].GetComponentInChildren<Text>().text = _players[i].id;
            }
        }

        public void OnClickPlayerButton(int index)
        {
            var you = _players.Find(player => player.id == "You");
            you.Vote(_players, () => _players[index].id);
//            Debug.Log("Vote Result:");
//            _players.ForEach(player => Debug.Log(player.id + ":" + player.votedNum));
//            _gameManager.ShowResult();
        }
    }
}