using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{

    public Vector3 turnPoint;
    public float moveSpeed;
    Vector3 targetPosition, originalPosition;
    void Awake()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == originalPosition)
        {
            targetPosition = turnPoint;
        } else if (transform.position == turnPoint) {
            targetPosition = originalPosition;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
