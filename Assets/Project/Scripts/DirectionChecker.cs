using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChecker : MonoBehaviour
{

    public bool hasBeenTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider otherCollider)
    {

        hasBeenTriggered = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
