using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipBehavior : MonoBehaviour {

    public float speed;
    public float turnSpeed;
    public float hoverForce;
    public float hoverHeight;
    private float powerInput;
    private float turnInput;
    private Rigidbody carRigidbody;

    void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    //void Update()
    //{

    //}//

    void FixedUpdate()
    {
        powerInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        Ray ray = new Ray(transform.position, -transform.up);
        Material mat = null;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance);
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;

            mat = findRaycastMaterial(hit);
            carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        if (mat == null)
        {
            //Debug.Log("null");
            carRigidbody.AddRelativeForce(0f, 0f, (powerInput * -speed)/4);
            carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
        }
        else if (mat.name == "Sidewalk (Instance)")
        {
            //Debug.Log("Sidewalk");
            carRigidbody.AddRelativeForce(0f, 0f, (powerInput * -speed)/2);
            carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
        }
        else if (mat.name == "Street (Instance)")
        {
            //Debug.Log("Street");
            carRigidbody.AddRelativeForce(0f, 0f, powerInput * -speed);
            carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
        }
        //Debug.DrawRay(transform.position, -transform.forward * hoverHeight, Color.white);
    }


    Material findRaycastMaterial(RaycastHit hit)
    {
        // Just in case, also make sure the collider also has a renderer
        // material and texture
        var meshCollider = hit.collider as MeshCollider;
        var terrainCollider = hit.collider as TerrainCollider;
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            //Debug.Log("MeshCollider null");
            return null;
        }

        Mesh mesh = meshCollider.sharedMesh;
        var triangles = mesh.triangles;

        // Extract vertices indices that were hit
        var v0 = triangles[hit.triangleIndex * 3 + 0];
        var v1 = triangles[hit.triangleIndex * 3 + 1];
        var v2 = triangles[hit.triangleIndex * 3 + 2];
        Renderer rend = hit.collider.GetComponent<Renderer>();

        for (var m = 0; m < rend.materials.Length; m++)
        {
            var mts = mesh.GetTriangles(m);
            for (var i = 0; i < mts.Length; i += 3)
            {
                if (mts[i] == v0 && mts[i + 1] == v1 && mts[i + 2] == v2) {
                    return rend.materials[m];
                }
            }
        }
   
        return null;
    }


}
