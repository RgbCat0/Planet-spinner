using UnityEngine;

namespace Jesper.InGame
{
    public class Player : MonoBehaviour
    {
        public bool item;
        private int TeamNumber => gameObject.name.EndsWith("1") ? 0 : 1;

        // ReSharper disable once ParameterHidesMember
        private void AddItem() { }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("SupplyItem") && !item)
            {
                item = true;
                UiManager.Instance.ChangeIndexedItem(
                    TeamNumber,
                    GameManager.Instance.ReturnCountAmount(TeamNumber),
                    Color.gray
                );
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Finish") && item)
            {
                AddItem();
                GameManager.Instance.AddItem(TeamNumber);
                item = false;
            }
        }
    }
}
