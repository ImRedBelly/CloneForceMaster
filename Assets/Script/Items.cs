using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Fly(Vector3 direction)
    {
        int force = Random.Range(40, 70);
        rb.AddForce(direction * force, ForceMode.Impulse);
        rb.AddTorque(direction * force, ForceMode.Impulse);
    }
}
