using Jesper.InGame;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jesper
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _playerInput;

        // debug info
        [SerializeField]
        private string controllerType;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.defaultControlScheme = "Controller2";
            _playerInput.SwitchCurrentControlScheme(
                _playerInput.defaultControlScheme,
                _playerInput.devices[0]
            );
            DontDestroyOnLoad(gameObject); // this gameObject is the main entry for every scene
            controllerType = _playerInput.devices[0].name;
            Debug.Log("PlayerInput successfully started");

            GameManager.Instance.AddPlayerInput(_playerInput); // register player input
        }

        public void SwitchControlScheme(string controlScheme) =>
            _playerInput.SwitchCurrentControlScheme(controlScheme, _playerInput.devices[0]);

        public void BindToTeamSelect(GameObject gameObjectToBind)
        {
            // aaaaaa
        }

        public void BindToInGame(string playerType)
        {
            switch (playerType)
            {
                case "player1"
                or "player2":
                    JoinNormalPlayer(playerType);
                    break;
                case "rotate1"
                or "rotate2":
                    JoinRotatingPlayer(playerType);
                    break;
            }
        }

        private void JoinNormalPlayer(string playerType)
        {
            SwitchControlScheme("Controller2");
            var temp = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.InstanceID);
            foreach (var movement in temp)
            {
                if (movement.gameObject.name.Contains(playerType))
                    movement.BindPlayerInput(_playerInput);
                else
                    continue;
                break;
            }
        }

        private void JoinRotatingPlayer(string playerType)
        {
            SwitchControlScheme("Controller1");
            var temp = FindObjectsByType<RotatePlanet>(FindObjectsSortMode.InstanceID);
            foreach (var rotatePlanet in temp)
            {
                if (rotatePlanet.gameObject.name.Contains(playerType))
                    rotatePlanet.BindPlayerInput(_playerInput);
                else
                    continue;
                break;
            }
        }
    }
}
