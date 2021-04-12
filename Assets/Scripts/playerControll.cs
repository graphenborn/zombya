using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControll : MonoBehaviour
{
	Animator animator;
    
    public float rotationSpeed = 3.0f;
    public float movementSpeed = 1.0f;

    private void AnimateMove(float horizontalInput, float verticalInput)
    {
        animator.SetBool("Running", horizontalInput != 0 || verticalInput != 0);
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        AnimateMove(horizontalInput, verticalInput);

        float dS = Time.deltaTime * movementSpeed;
        Vector3 dV = new Vector3(dS * horizontalInput, 0, dS * verticalInput);

        transform.Translate(dV, Space.World);
    }

    private void RotateToCursor()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if (playerPlane.Raycast(ray, out float hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    rotationSpeed * Time.deltaTime);
        }
    }

    void Start() 
    {
        animator = GetComponent<Animator>();
    }

    void Update() 
    {
        Move();
        RotateToCursor();
    }
}

