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
        if(horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetFloat("Strafe", 1);
            animator.SetFloat("Forward", 1);
        }
        else
        {
            animator.SetFloat("Strafe", 0);
            animator.SetFloat("Forward", 0);
        }
        
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        AnimateMove(horizontalInput, verticalInput);

        float deltaS = Time.deltaTime * movementSpeed;

        transform.Translate(
            deltaS * horizontalInput, 
            0, 
            deltaS * verticalInput, 
            Camera.main.transform);
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

