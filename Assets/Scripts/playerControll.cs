using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControll : MonoBehaviour
{
	Animator animator;
    //int runStateHash = Animator.StringToHash("runnung");
    
    public float rotationSpeed = 3.0f;
    public float movementSpeed = 1.0f;
    private float horizontalInput;
    private float verticalInput;

    private void AnimateMove(float horizontalInput, float verticalInput)
    {
        if(horizontalInput != 0 || verticalInput != 0){
            animator.SetFloat("Strafe", 1);
            animator.SetFloat("Forward", 1);
        }
        else{
            animator.SetFloat("Strafe", 0);
            animator.SetFloat("Forward", 0);
        }
        
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        AnimateMove(horizontalInput, verticalInput);

        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * horizontalInput);

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

