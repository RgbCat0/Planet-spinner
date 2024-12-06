using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    GameObject doorPart1;

    [SerializeField]
    GameObject doorPart2;

    [SerializeField]
    Transform buttonBase;

    [SerializeField]
    bool xAs;

    bool pressed = false;
    int count = 0;
    float moveSpeed = 1f;

    void Update()
    {
        if (pressed && count < 4)
        {
            Move();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!pressed)
        {
            pressed = true;
        }
    }

    void Move()
    {
        if (xAs)
        {
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                buttonBase.position * 2,
                moveSpeed * Time.deltaTime
            );
            doorPart1.transform.position -= new Vector3(0.3f, 0, 0);
            doorPart2.transform.position += new Vector3(0.3f, 0, 0);
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                buttonBase.position * 2,
                moveSpeed * Time.deltaTime
            );
            doorPart1.transform.position += new Vector3(0, 0.3f, 0);
            doorPart2.transform.position -= new Vector3(0, 0.3f, 0);
        }
        count++;
        // BYUG FIX
        doorPart1.GetComponent<Collider>().enabled = false;
        doorPart2.GetComponent<Collider>().enabled = false;
        enabled = false;
    }
}
