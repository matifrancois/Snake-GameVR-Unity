using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eat : MonoBehaviour
{
    public bool crashed = false;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "littleDuck")
        {
            Destroy(other.gameObject);
            crashed = true;
        }
        if (other.gameObject.name == "bodySnake(Clone)")
        {
            gameOver = true;
            Debug.Log("me comi a mi mismo");
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "littleDuck")
    //    {
    //        Destroy(collision.gameObject);
    //        crashed = true;
    //    }
    //}

}
