using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Jesper.TitleScreen
{
    public class TitleUiManager : MonoBehaviour
    {
        private enum UiState
        {
            TitleScreen,
            SettingsScreen,
            CreditsScreen,
            Exiting
        }

        private UiState _currentState = UiState.TitleScreen;

        [SerializeField]
        private TextMeshProUGUI mainText,
            subText,
            playerText;

        [SerializeField]
        private Button playButton,
            exitButton;

        [SerializeField]
        private AnimationCurve textMovementCurve;

        [SerializeField]
        private float animationDuration = 1f;

        private const string TitleScreenName = "Rotational race";
        private const string SettingsScreenName = "Settings Title"; // no settings rn
        private const string CreditsScreenName =
            "Developers: Jesper, Thomas\nArtists: Lucas Bl, Lucas Bo, Jiray";
        private const string ExitingScreenName = "Thanks for playing!";

        private void Start()
        {
            mainText.text = TitleScreenName;
        }

        private IEnumerator MoveMainTextToCenter()
        {
            var timeElapsed = 0f;
            var startPosition = mainText.rectTransform.anchoredPosition;
            var endPosition = mainText.rectTransform.anchoredPosition = Vector2.zero;

            while (timeElapsed < animationDuration)
            {
                mainText.rectTransform.anchoredPosition = Vector2.Lerp(
                    startPosition,
                    endPosition,
                    textMovementCurve.Evaluate(timeElapsed / animationDuration)
                );
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }

        public async void OnPlayButtonClicked()
        {
            await SceneManager.LoadSceneAsync("Main");

            GameManager.Instance.DistributePlayers();
        }

        public void OnExitButtonClicked() => StartCoroutine(ExitGame());

        private IEnumerator ExitGame()
        {
            mainText.text = ExitingScreenName;
            _currentState = UiState.Exiting;
            StartCoroutine(MoveMainTextToCenter());
            yield return new WaitForSeconds(animationDuration);
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // testing purposes
#endif
            yield return null;
        }

        public void SetPlayerText(int playerCount) => playerText.text = $"Players: {playerCount}";

        public void EnablePlayButton() => playButton.interactable = true;
    }
}
