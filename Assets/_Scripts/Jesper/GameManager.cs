using System.Collections.Generic;
using Jesper.InGame;
using Jesper.TitleScreen;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jesper
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton pattern
        // singleton pattern
        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                return;
            }
            Destroy(gameObject);
        }
        #endregion
        #region Player input handler
        public List<PlayerInput> playerInputs = new(); // game can only continue if there are 4 players
        private const int NeededPlayers = 2;

        public void AddPlayerInput(PlayerInput playerInput)
        {
            playerInputs.Add(playerInput);
            var temp = FindFirstObjectByType<TitleUiManager>().GetComponent<TitleUiManager>();
            temp.SetPlayerText(playerInputs.Count);
            if (playerInputs.Count == 2)
            {
                playerInputs[1]
                    .GetComponent<PlayerInputHandler>()
                    .SwitchControlScheme("Controller1");
            }
            if (playerInputs.Count == NeededPlayers)
            {
                temp.EnablePlayButton();
            }
        }
        #endregion
        #region In game

        public void DistributePlayers()
        {
            // foreach (var playerInput in playerInputs)
            // {
            // playerInput.GetComponent<PlayerInputHandler>().BindToInGame(false);
            // }
            //test
            playerInputs[0].GetComponent<PlayerInputHandler>().BindToInGame(false);
            playerInputs[1].GetComponent<PlayerInputHandler>().BindToInGame(true);
        }

        public List<string> collectedItems = new();

        public void AddItem(string item)
        {
            collectedItems.Add(item);
            UiManager.Instance.SetItemText(item);
            if (collectedItems.Count == 1)
            {
                Debug.Log("Win");
            }
        }
        #endregion
    }
}
