using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    public bool isfine = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isfine == false)
            isfine = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Snake" || other.gameObject.name == "bodySnake(Clone)" || other.gameObject.name == "second")
            isfine = false;
    }
}
