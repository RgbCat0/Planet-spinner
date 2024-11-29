using TMPro;
using UnityEngine;

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
        [SerializeField]
        private TextMeshProUGUI item,
            item2;

        [SerializeField]
        private TextMeshProUGUI countdown,
            countdown2;

        [SerializeField]
        private GameObject countDownParent;

        public void SetItemText(string text, int team)
        {
            if (team == 0)
                item.text = $"Supply item: {text}";
            else
                item2.text = $"Supply item: {text}";
        }

        public void UpdateCountDown(string text)
        {
            countdown.text = text;
            countdown2.text = text;
        }

        public void GameStart()
        {
            countDownParent.SetActive(false);
        }
    }
}
