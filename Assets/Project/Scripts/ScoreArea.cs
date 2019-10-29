using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{

    public GameObject effectObject;

    public DirectionChecker directionChecker;

    // Start is called before the first frame update
    void Start()
    {
        effectObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {

        if (directionChecker.hasBeenTriggered)
        {
            if (otherCollider.GetComponent<Ball>() != null)
            {
                effectObject.SetActive(true);
            }
        }
    }

    

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.GetContact(0));

        Debug.Log(collision.GetContact(0).normal);


    }


}
