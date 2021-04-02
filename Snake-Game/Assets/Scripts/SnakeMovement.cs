using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> BodyParts = new List<Transform>();
    public float minDistance = 0.25f;
    public int index_menu;
    public float rotationSpeed = 50;
    public GameObject bodyPrefab;
    public int beginsize;
    public bool isInPause = false;
    public bool isInMapping = true;
    public string R1;
    public bool changeSnakeAxis;

    private float dist;
    private Transform curBodyPart;
    private Transform prevBodyPart;
    private float[] speed = { 2, 3, 5 };
    private float curspeed;


    // Start is called before the first frame update
    void Start()
    {
        changeSnakeAxis = false;
        //R1 = "";
        for (int i = 0; i < beginsize-1; i++)
        {
            AddBodyPart();
        }
        index_menu = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInPause && !isInMapping)
            Move();
    }

    public void Move()
    {
        curspeed = speed[index_menu % 3];

        if (Input.GetKey(R1))
            curspeed *= 2;

        //here we use smooth delta time because it has better results in the simulation
        BodyParts[0].Translate(BodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

        if (!changeSnakeAxis)
        {
            if (Input.GetAxis("Horizontal") != 0)
                BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }
        else
        {
            if (Input.GetAxis("Vertical") != 0)
                BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        }

        //start in 1 because the zero element was modified before
        for (int i = 1; i < BodyParts.Count; i++)
        {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            dist = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newpos = prevBodyPart.position;

            newpos.y = BodyParts[0].position.y;

            float T = Time.deltaTime * dist / minDistance * curspeed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        //we generate the new part with this
        GameObject bodypart = Instantiate(bodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject;
        if (BodyParts.Count == 1)
        {
            bodypart.gameObject.name = "second";
        }
        Transform newPart = (bodypart).transform;
        newPart.SetParent(transform);
        
        BodyParts.Add(newPart);
    }
}
