using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Jesper.InGame
{
    public class UiManager : MonoBehaviour
    {
        #region Singleton pattern
        public static UiManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(this); this script is only used in the main scene
                return;
            }
            Destroy(gameObject);
        }
        #endregion
        [FormerlySerializedAs("Team1Images")]
        [SerializeField]
        private List<RawImage> team1Images;

        [FormerlySerializedAs("Team2Images")]
        [SerializeField]
        private List<RawImage> team2Images;

        [SerializeField]
        private TextMeshProUGUI countdown,
            countdown2;

        [SerializeField]
        private GameObject countDownParent;

        public void ChangeIndexedItem(int teamNumber, int index, Color color)
        {
            if (teamNumber == 0)
                team1Images[index].color = color;
            else
                team2Images[index].color = color;
        }

        public void UpdateCountDown(string text)
        {
            countdown.text = text;
            countdown2.text = text;
        }

        public void GameStart()
        {
            countDownParent.SetActive(false);
            countdown.text = "PAUSED";
            countdown2.text = "PAUSED"; // already setups the text
        }

        public void PauseGame(bool pause) => countDownParent.SetActive(pause);
    }
}
