using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControll : MonoBehaviour
{
	Animator animator;
    int runStateHash = Animator.StringToHash("runnung");
    
    public float rspeed = 3.0f;
    public float speed = 1.0f;
    public float horizontalInput;
    public float verticalInput;

    void Movement(){
    horizontalInput = Input.GetAxis("Horizontal");
    animator.SetFloat("Strafe", horizontalInput);

    verticalInput = Input.GetAxis("Vertical");
    animator.SetFloat("Forward", verticalInput);
    
    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
    transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
}
    void CamFollow(){
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast (ray, out hitdist)) {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rspeed * Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

// Update is called once per frame
void Update() {
    Movement();
    CamFollow();
    }
}

