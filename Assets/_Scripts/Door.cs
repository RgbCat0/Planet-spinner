using System.Collections;
using System.Threading;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField]GameObject doorPart1;
    [SerializeField]GameObject doorPart2;
    bool pressed = false;
    int count;
    [SerializeField] bool xAs;
    void Start()
    {
        
    }

    void Update()
    {
        if (pressed && count < 4)
        {
            StartCoroutine("Move");
        }
        else
            StopCoroutine("Move");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!pressed) 
        {
            pressed = true;
        }

    }
    IEnumerator Move()
    {
        if (xAs)
        {
            gameObject.transform.position -= new Vector3(0, 0.05f, 0);
            doorPart1.transform.position -= new Vector3(0.3f, 0, 0);
            doorPart2.transform.position += new Vector3(0.3f, 0, 0);
        }
        else
        {
            gameObject.transform.position -= new Vector3(0, 0.05f, 0);
            doorPart1.transform.position += new Vector3(0, 0.3f, 0);
            doorPart2.transform.position -= new Vector3(0, 0.3f, 0);
        }
        count++;
        yield return new WaitForSeconds(0.5f);

    }
}
