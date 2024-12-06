using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Jesper.TitleScreen
{
    /// <summary>
    /// Handles the team selection of the players. Players can select between two teams and two rotations.
    /// </summary>
    public class TeamSelectHandler : MonoBehaviour
    {
        [FormerlySerializedAs("playerObjects")]
        public List<RectTransform> players; // used for team selection movement
        public bool checkPosition;

        [SerializeField]
        private List<Vector2> rotateBox1;

        [SerializeField]
        private List<Vector2> rotateBox2;

        [SerializeField]
        private List<Vector2> playerBox1;

        [SerializeField]
        private List<Vector2> playerBox2;

        [SerializeField]
        private List<string> playerOrder = new(4); // if player0 is in playerBox2, playerOrder[0] = "playerBox2"

        public void Bind()
        {
            for (var i = 0; i < GameManager.Instance.playerInputs.Count; i++)
            {
                players[i]
                    .GetComponent<TitlePlayerMovement>()
                    .Bind(GameManager.Instance.playerInputs[i]);
            }
        }

        private void Update()
        {
            if (!checkPosition)
                return;
            for (var i = 0; i < players.Count; i++)
            {
                if (IsInsideBox(players[i].anchoredPosition, rotateBox1))
                {
                    if (NotSameBox("rotate1"))
                        playerOrder[i] = "rotate1";
                }
                else if (IsInsideBox(players[i].anchoredPosition, rotateBox2))
                {
                    if (NotSameBox("rotate2"))
                        playerOrder[i] = "rotate2";
                }
                else if (IsInsideBox(players[i].anchoredPosition, playerBox1))
                {
                    if (NotSameBox("player1"))
                        playerOrder[i] = "player1";
                }
                else if (IsInsideBox(players[i].anchoredPosition, playerBox2))
                {
                    if (NotSameBox("player2"))
                        playerOrder[i] = "player2";
                }
                else
                    playerOrder[i] = "";
            }
            // checks if all players have selected a team (players can be less than 4 for testing)
            if (playerOrder.Count(x => x != "") < GameManager.NeededPlayers)
                return;
            checkPosition = false;
            GameManager.Instance.StartGame(playerOrder);
            enabled = false;
        }

        private IEnumerator timer()
        {
            yield return new WaitForSeconds(3);
        }

        private bool IsInsideBox(Vector2 position, List<Vector2> box)
        {
            if (box.Count < 2)
                return false;

            Vector2 bottomLeft = box[0];
            Vector2 topRight = box[1];

            return position.x >= bottomLeft.x
                && position.x <= topRight.x
                && position.y >= bottomLeft.y
                && position.y <= topRight.y;
        }

        private bool NotSameBox(string boxName)
        {
            return playerOrder.All(x => x != boxName);
        }
    }
}
