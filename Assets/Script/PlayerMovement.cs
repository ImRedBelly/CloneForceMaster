using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] CharacterController characterController;
    [SerializeField] Joystick joystick;
    [SerializeField] float speed;

    public Transform targetPoint;
    public Transform targetEnemy;

    public Transform cameraTransform;

    public List<Enemy> enemies;
    public LayerMask enemiesMask;
    bool isFind;

    float inputH;
    float inputV;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        isFind = Physics.CheckSphere(transform.position, 5, enemiesMask);

        if (isFind && targetEnemy == null)
            SearchEnemy();

        Move();
        LookAtTarget();
    }

    private void Move()
    {
        inputH = joystick.Horizontal;
        inputV = joystick.Vertical;

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 direction = forward * inputV + right * inputH;
        direction.y = Physics.gravity.y;

        characterController.Move(direction * speed * Time.deltaTime);
    }



    public void LookAtTarget()
    {
        if (targetEnemy != null)
        {
            var look = Quaternion.LookRotation(targetEnemy.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, look, Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetPoint.rotation, Time.deltaTime);
        }
    }

    public void SearchEnemy()
    {
        float length = 0;
        float lengthEnemy = 0;

        foreach (Enemy enemy in enemies)
        {
            lengthEnemy = enemy.LengthPath;

            if (length == 0)
            {
                length = lengthEnemy;
                targetEnemy = enemy.transform;
            }
            else
            {
                if (lengthEnemy < length)
                {
                    length = lengthEnemy;
                    targetEnemy = enemy.transform;
                }
            }
        }
    }
}






























//void CameraTilt(float value)
//{
//    var rotC = cameraTransform.rotation;
//    var rotP = transform.localRotation;
//    if (value < 0)
//    {
//        cameraTransform.localRotation = Quaternion.Lerp(rotC,
//            Quaternion.Euler(0, rotP.y, 2), 5 * Time.deltaTime);
//    }
//    else if (value > 0)
//    {
//        cameraTransform.localRotation = Quaternion.Lerp(rotC,
//            Quaternion.Euler(0, rotP.y, -2), 5 * Time.deltaTime);
//    }
//    else
//    {
//        cameraTransform.localRotation = Quaternion.Lerp(rotC,
//            Quaternion.Euler(0, rotP.y, 0), 5 * Time.deltaTime);
//    }
//}