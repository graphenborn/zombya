using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControll : MonoBehaviour
{
	Animator animator;
    int runStateHash = Animator.StringToHash("runnung");
    
    public float speed = 3.0f;
    public float horizontalInput;
    public float verticalInput;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

// Update is called once per frame
void Update() {

    horizontalInput = Input.GetAxis("Horizontal");
    animator.SetFloat("Strafe", horizontalInput);

    verticalInput = Input.GetAxis("Vertical");
    animator.SetFloat("Forward", verticalInput);
    
    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
    transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
    }
}
