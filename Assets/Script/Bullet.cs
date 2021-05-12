using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] GameObject explosive;

    public void Fly(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
            Explosion();
    }

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
        var direction = rb.velocity.normalized;

        foreach (var col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb)
            {
                if (col.gameObject.CompareTag("Player"))
                    rb.GetComponent<PlayerHealth>().Damage();

                else if (col.gameObject.CompareTag("Enemy"))
                    rb.GetComponent<Enemy>().Dead(direction);

                else if (col.gameObject.CompareTag("BadItem"))
                    rb.GetComponent<Items>().Fly(direction);
            }
        }

        Instantiate(explosive, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }
}
