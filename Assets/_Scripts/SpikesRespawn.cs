using Unity.VisualScripting;
using UnityEngine;

public class SpikesRespawn : MonoBehaviour
{

   [SerializeField] GameObject RespawnPointRed;
   [SerializeField] GameObject RespawnPointBlue;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player1"))
        {
            collision.gameObject.transform.position = RespawnPointRed.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
        if (collision.collider.CompareTag("Player2"))
        {
            collision.gameObject.transform.position = RespawnPointBlue.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        }
    }
}
