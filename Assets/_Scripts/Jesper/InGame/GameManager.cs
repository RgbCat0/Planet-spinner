using System.Collections.Generic;
using UnityEngine;

namespace Jesper.InGame
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
    }
}
