using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerForce : MonoBehaviour
{
    public GameObject forcePoint;
    [SerializeField] float force;

    [SerializeField] float maxSwipeTime;
    [SerializeField] float maxSwipeDistance;

    float swipeStartTime;
    float swipeEndTime;
    float swipeTime;

    Vector2 startSwipePosition;
    Vector2 endSwipePosition;
    float swipeLenght;

    void Update()
    {
        SwipeInput();
    }

    void SwipeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                swipeStartTime = Time.time;
                startSwipePosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                swipeEndTime = Time.time;
                endSwipePosition = touch.position;

                swipeTime = swipeEndTime - swipeStartTime;
                swipeLenght = (endSwipePosition - startSwipePosition).magnitude;

                if (swipeTime < maxSwipeTime && swipeLenght > maxSwipeDistance)
                    SwipeControl();
            }
        }
    }
    void SwipeControl()
    {
        Vector2 distance = endSwipePosition - startSwipePosition;
        float xDistance = Mathf.Abs(distance.x);
        float yDistance = Mathf.Abs(distance.y);

        if (xDistance > yDistance)
        {
            if (distance.x > 0)
                AddForce(transform.right + transform.up);

            else if (distance.x < 0)
                AddForce(-transform.right + transform.up);
        }
        else if (yDistance > xDistance)
        {
            if (distance.y > 0)
                AddForce(transform.forward + transform.up);

            else if (distance.y < 0)
                AddForce(-transform.forward + transform.up);
        }
    }
    void AddForce(Vector3 direction)
    {
        Collider[] colliders = Physics.OverlapSphere(forcePoint.transform.position, 5);


        foreach (var col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb)
            {
                if (col.gameObject.CompareTag("Bullet"))
                    rb.GetComponent<Bullet>().Fly(direction * 3);
                
                else if (col.gameObject.CompareTag("Enemy"))
                    rb.GetComponent<Enemy>().Dead(direction);
                
                else if (col.gameObject.CompareTag("BadItem"))
                    rb.GetComponent<Items>().Fly(direction);
            }
        }
    }
}