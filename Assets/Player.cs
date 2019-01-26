using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    
    void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(Vector3.right * horizontal);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Item"))
        {
            Debug.Log("Премет: " + name);
        }
    }
}
