using UnityEngine;

namespace Jesper.TitleScreen
{
    public class PlayerCount : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] playerCountObjects;

        public void UpdateCount(int playerCount)
        {
            for (int i = 0; i < playerCountObjects.Length; i++)
            {
                playerCountObjects[i].SetActive(i < playerCount);
            }
        }
    }
}
