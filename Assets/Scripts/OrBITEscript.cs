using UnityEngine;
using System.Collections;

public class OrBITEscript : MonoBehaviour
{

    public GameObject cube;
    Transform center;
    Vector3 axis = Vector3.left;
    //Vector3 desiredPosition;
    public float radius;
    //public float radiusSpeed = 0.5f;
    public float rotationSpeed;

    void Start()
    {
        center = cube.transform;
        transform.position = (transform.position - center.position).normalized * radius + center.position;
    }

    void Update()
    {
        transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
        //desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        //transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }
}