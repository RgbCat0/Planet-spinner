using UnityEngine;

namespace Jesper.InGame
{
    public class Player : MonoBehaviour
    {
        public string item;

        // ReSharper disable once ParameterHidesMember
        private void AddItem(string item)
        {
            this.item = item;
            UiManager.Instance.SetItemText(item);
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
                GameManager.Instance.AddItem(item);
                item = string.Empty;
            }
            else
            {
                Debug.Log($"Player collided with {other.gameObject.name}");
            }
        }
    }
}
