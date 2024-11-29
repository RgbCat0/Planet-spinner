using System.Collections.Generic;
using Jesper.InGame;
using Jesper.TitleScreen;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Jesper
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton pattern
        // singleton pattern
        public static GameManager Instance;

        private void Awake()
        {
            Application.targetFrameRate = 120;
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
        private const int NeededPlayers = 4;
        private TitleUiManager _titleUiManager;
        private TeamSelectHandler _teamSelectHandler;

        private void Start()
        {
            _titleUiManager = FindFirstObjectByType<TitleUiManager>()
                .GetComponent<TitleUiManager>();
            _teamSelectHandler = FindFirstObjectByType<TeamSelectHandler>()
                .GetComponent<TeamSelectHandler>();
        }

        public void AddPlayerInput(PlayerInput playerInput)
        {
            playerInputs.Add(playerInput);
            _titleUiManager.SetPlayerText(playerInputs.Count);
            if (playerInputs.Count == 2)
            {
                playerInputs[1]
                    .GetComponent<PlayerInputHandler>()
                    .SwitchControlScheme("Controller1");
            }

            if (playerInputs.Count == 4)
            {
                playerInputs[3] // player 4
                    .GetComponent<PlayerInputHandler>()
                    .SwitchControlScheme("Controller1");
            }
            if (playerInputs.Count == NeededPlayers)
            {
                _titleUiManager.EnablePlayButton();
            }
        }
        #endregion

        #region In game

        public void GotoTeamSelect()
        {
            _titleUiManager.SwitchToTeamSelect();

            _teamSelectHandler.Bind();
            _teamSelectHandler.checkPosition = true;
        }

        public async void StartGame(List<string> playerOrder)
        {
            await SceneManager.LoadSceneAsync("Main");
            _teamSelectHandler.checkPosition = false;
            DistributePlayers(playerOrder);
        }

        private void DistributePlayers(List<string> playerOrder)
        {
            for (var i = 0; i < playerInputs.Count; i++)
                playerInputs[i].GetComponent<PlayerInputHandler>().BindToInGame(playerOrder[i]);
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
