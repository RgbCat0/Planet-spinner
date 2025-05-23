using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jesper.TitleScreen
{
    public class TitleUiManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainPanel,
            teamSelectPanel;

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
            "Developers: Jesper, Thomas\nArtists: Lucas Bl, Jiray";
        private const string ExitingScreenName = "Thanks for playing!";
        public int counter;

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

        public void OnPlayButtonClicked() => GameManager.Instance.GotoTeamSelect();

        public void OnCreditsButtonClicked()
        {
            counter++;
            if (counter == 3)
                counter = 0;
            subText.text = $"Map: {counter + 1}";
        }

        public void OnExitButtonClicked() => StartCoroutine(ExitGame());

        private IEnumerator ExitGame()
        {
            mainText.text = ExitingScreenName;
            StartCoroutine(MoveMainTextToCenter());
            yield return new WaitForSeconds(animationDuration);
            Application.Quit();
            yield return null;
        }

        public void SetPlayerAmount(int playerCount) =>
            GameObject.Find("PlayerAmount").GetComponent<PlayerCount>().UpdateCount(playerCount);

        public void EnablePlayButton() => playButton.interactable = true;

        public void SwitchToTeamSelect()
        {
            mainPanel.SetActive(false);
            teamSelectPanel.SetActive(true);
        }
    }
}
