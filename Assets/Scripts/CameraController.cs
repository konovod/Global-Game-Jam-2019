using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -1);
    [SerializeField] private float speed = 4;
    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset,
            speed * Time.deltaTime);
    }
}
