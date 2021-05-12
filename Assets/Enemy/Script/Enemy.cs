using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public bool isShoot;

    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] Material material;

    [SerializeField] List<Collider> ragdollCollider;
    [SerializeField] List<Rigidbody> ragdollRigidbody;

    public float speed;
    float lengthPath;

    public Transform player;
    public LayerMask playerMask;


    Vector3 direction;
    float walkRadius = 10;
    float attackRadius = 3;

    float timeAttack = 3;

    [Header("Shot Component")]
    public GameObject bulletPrefab;
    public Transform shotPosition;

    public float LengthPath
    {
        get
        {
            lengthPath = (PlayerMovement.instance.transform.position - transform.position).magnitude;
            return lengthPath;
        }
    }

    private void Start()
    {
        PlayerMovement.instance.enemies.Add(this);
    }

    private void Update()
    {
        direction = player.position - transform.position;
        transform.LookAt(player);

        if (isShoot)
        {
            animator.SetBool("Walk", false);

            if (timeAttack < 0)
            {
                animator.SetTrigger("Shot");
                timeAttack = 8;
            }
            else
                timeAttack -= Time.deltaTime;
        }
        else
        {
            if (direction.magnitude < walkRadius && direction.magnitude > attackRadius)
                Run();

            else if (direction.magnitude < attackRadius)
            {
                animator.SetBool("Walk", false);

                if (timeAttack < 0)
                {
                    animator.SetTrigger("Punch");
                    timeAttack = 2;
                }
                else
                    timeAttack -= Time.deltaTime;
            }
            else
                animator.SetBool("Walk", false);
        }
    }

    void Run()
    {
        animator.SetBool("Walk", true);
        characterController.Move(direction.normalized * Time.deltaTime);
    }

    public void Shot()// Animation Event
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPosition.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Fly(transform.forward);
    }

    public void MelleAttack() // Animation Event
    {
        bool isAttack = Physics.CheckSphere(transform.position, attackRadius, playerMask);
        if (isAttack)
            player.GetComponent<PlayerHealth>().Damage();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BadItem"))
        {
            Dead(-transform.forward);
        }
    }


    public void Dead(Vector3 direction)
    {
        animator.enabled = false;
        characterController.enabled = false;
        GetComponent<Enemy>().enabled = false;
        meshRenderer.material = material;

        foreach (Rigidbody rb in ragdollRigidbody)
        {
            rb.isKinematic = false;

            int force = Random.Range(40, 70);
            rb.AddForce(direction * force, ForceMode.Impulse);
            rb.AddTorque(direction * force, ForceMode.Impulse);
        }

        foreach (Collider col in ragdollCollider)
            col.enabled = true;

        PlayerMovement.instance.enemies.Remove(this);
        PlayerMovement.instance.targetEnemy = null;
    }
}
