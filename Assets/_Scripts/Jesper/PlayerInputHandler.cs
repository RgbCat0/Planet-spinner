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

        public void BindToInGame(bool rotatingPlayer)
        {
            if (!rotatingPlayer)
                JoinNormalPlayer();
            else
                JoinRotatingPlayer();
        }

        private void JoinNormalPlayer()
        {
            var temp = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.InstanceID);
            foreach (var movement in temp)
            {
                if (!movement.Bound)
                    continue;
                movement.BindPlayerInput(_playerInput);
                break;
            }
        }

        private void JoinRotatingPlayer()
        {
            var temp = FindObjectsByType<RotatePlanet>(FindObjectsSortMode.InstanceID);
            foreach (var rotatePlanet in temp)
            {
                if (rotatePlanet.Bound)
                    continue;
                rotatePlanet.BindPlayerInput(_playerInput);
                break;
            }
        }
    }
}
