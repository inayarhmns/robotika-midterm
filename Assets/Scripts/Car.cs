using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public float speed = 1.0f;
    public Transform path;

    private List<Transform> nodes;
    private int currentNode = 0;

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    public WheelCollider wheelBR;
    public WheelCollider wheelBL;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        //nodes = new List<Transform>();

        //for (int i = 0; i < pathTransforms.Length; i++)
        //{
        //    if (pathTransforms[i] != path.transform)
        //    {
        //        nodes.Add(pathTransforms[i]);
        //    }
        //}

    }

    // Update is called once per frame
    void Update()
    {
        MoveWheels();
        RunSteer();
        //MoveGameObject();
        CheckWaypointDistance();
        Sensors();
    }

    private void MoveWheels()
    {
        wheelBL.motorTorque = speed;
        wheelBR.motorTorque = speed;
    }

    private void MoveGameObject()
    {
        var step = speed * Time.deltaTime;

        this.transform.position = Vector3.MoveTowards(transform.position, nodes[currentNode].position, step);
    }

    private void CheckWaypointDistance()
    {
        //if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1f)
        //{
        //    if (currentNode < nodes.Count - 1)
        //    {
        //        currentNode++;
        //    }
        //    else
        //    {
        //        currentNode = 0;
        //    }
        //}
    }

    private void RunSteer()
    {
        float MaxAngle = 45f;
        //Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        if (Input.GetKeyDown(KeyCode.A))
        {
            wheelFL.steerAngle = -MaxAngle;
            wheelFR.steerAngle = -MaxAngle;
            print("A");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            wheelFL.steerAngle = MaxAngle;
            wheelFR.steerAngle = MaxAngle;
            print("D");
        }
        else
        {
            wheelFL.steerAngle = 0;
            wheelFR.steerAngle = 0;
        }
        //float newSteer = (relativeVector.x / relativeVector.magnitude) * MaxAngle;
        //wheelFL.steerAngle = newSteer;
        //wheelFR.steerAngle = newSteer;
    }

    private void Sensors()
    {
        RaycastHit hit;
        float sensorLength = 10.0f;

        if (Physics.Raycast(transform.position, transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
            }
        }

    }
}
