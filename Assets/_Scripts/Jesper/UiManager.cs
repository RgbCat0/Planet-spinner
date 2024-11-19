using TMPro;
using UnityEngine;

namespace Jesper
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
        private TextMeshProUGUI item;

        public void SetItemText(string text)
        {
            item.text = $"Supply item: {text}";
        }
    }
}
