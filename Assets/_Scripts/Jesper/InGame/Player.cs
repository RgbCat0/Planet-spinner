using UnityEngine;

namespace Jesper.InGame
{
    public class Player : MonoBehaviour
    {
        public string item;
        private int TeamNumber => gameObject.name.EndsWith("1") ? 0 : 1;

        // ReSharper disable once ParameterHidesMember
        private void AddItem(string item)
        {
            this.item = item;
            UiManager.Instance.SetItemText(item, TeamNumber);
        }

        private void RemoveItem()
        {
            item = string.Empty;
            UiManager.Instance.SetItemText(string.Empty, TeamNumber);
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("SupplyItem") && item == string.Empty)
            {
                AddItem(other.gameObject.name);
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Finish") && item != string.Empty)
            {
                GameManager.Instance.AddItem(item, TeamNumber);
                RemoveItem();
            }
        }
    }
}
