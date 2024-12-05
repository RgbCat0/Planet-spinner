using System.Collections;
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
        public const int NeededPlayers = 1;
        private TitleUiManager _titleUiManager;
        private TeamSelectHandler _teamSelectHandler;
        private PlayerInputManager _playerInputManager;

        private void Start()
        {
            _titleUiManager = FindFirstObjectByType<TitleUiManager>()
                .GetComponent<TitleUiManager>();
            _teamSelectHandler = FindFirstObjectByType<TeamSelectHandler>()
                .GetComponent<TeamSelectHandler>();
            _playerInputManager = GetComponent<PlayerInputManager>();
        }

        public void AddPlayerInput(PlayerInput playerInput)
        {
            playerInputs.Add(playerInput);
            _titleUiManager.SetPlayerAmount(playerInputs.Count);
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

        private float _timerTeam1,
            _timerTeam2;
        public int Team1Score { private set; get; }
        public int Team2Score { private set; get; }
        public bool Team1Won { private set; get; }
        public List<bool> collectedItems = new();
        public List<bool> collectedItems2 = new();
        private bool _gamePaused;
        public List<GameObject> maps;

        public void GotoTeamSelect()
        {
            _titleUiManager.SwitchToTeamSelect();

            _teamSelectHandler.Bind();
            _teamSelectHandler.checkPosition = true;
        }

        public async void StartGame(List<string> playerOrder)
        {
            _teamSelectHandler.checkPosition = false;
            await SceneManager.LoadSceneAsync("Main");
            Instantiate(maps[0]);
            DistributePlayers(playerOrder);
        }

        private void DistributePlayers(List<string> playerOrder)
        {
            for (var i = 0; i < playerInputs.Count; i++)
                playerInputs[i].GetComponent<PlayerInputHandler>().BindToInGame(playerOrder[i]);
            StartCoroutine(StartGameCountdown());
        }

        private IEnumerator StartGameCountdown()
        {
            var temp = FindObjectsByType<RotatePlanet>(FindObjectsSortMode.InstanceID);
            var temp2 = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.InstanceID);
            foreach (var t in temp)
                t.movementEnabled = false;
            foreach (var t in temp2)
                t.movementEnabled = false;
            yield return new WaitForSeconds(1);
            UiManager.Instance.UpdateCountDown("2");
            yield return new WaitForSeconds(1);
            UiManager.Instance.UpdateCountDown("1");
            yield return new WaitForSeconds(1);
            UiManager.Instance.UpdateCountDown("GO!");
            yield return new WaitForSeconds(0.5f);
            UiManager.Instance.GameStart();
            foreach (var t in temp)
                t.movementEnabled = true;
            foreach (var t in temp2)
                t.movementEnabled = true;
        }

        public void AddItem(int teamNumber)
        {
            if (teamNumber == 0)
            {
                collectedItems.Add(true);
                Team1Score += 1000 / Mathf.RoundToInt(_timerTeam1);
                _timerTeam1 = 0;

                UiManager.Instance.ChangeIndexedItem(
                    teamNumber,
                    collectedItems.Count - 1,
                    Color.white
                );
                if (collectedItems.Count < 3)
                    return;
                Debug.Log("team 1 wins");
                Team1Won = true;
            }
            else
            {
                collectedItems2.Add(true);
                Team2Score += 1000 / Mathf.RoundToInt(_timerTeam2);
                _timerTeam2 = 0;
                if (collectedItems2.Count < 3)
                    return;
                Debug.Log("team 2 wins");
                Team1Won = false;
            }

            SceneManager.LoadScene("EndGame");
        }

        public int ReturnCountAmount(int teamNumber) =>
            teamNumber == 0 ? collectedItems.Count : collectedItems2.Count;

        private void Update()
        {
            _timerTeam1 += Time.deltaTime;
            _timerTeam2 += Time.deltaTime;
        }

        public void PauseGame()
        {
            _gamePaused = !_gamePaused;
            Time.timeScale = _gamePaused ? 0 : 1;
            UiManager.Instance.PauseGame(_gamePaused);
        }

        #endregion
    }
}
