using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovealone : MonoBehaviour {
    private CharacterController cc1;
    public Transform ARCamera;
    public float speed = 6;
    private Animator animator;
   void Awake()
    {
        cc1 = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        ARCamera = Camera.main.transform;
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (Joystick.h != 0 || Joystick.v != 0)
        {
            x = Joystick.h;
            y = Joystick.v;
        }
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1f)
        {
            animator.SetBool("Walk", true);
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun"))
            {
                Vector3 targetDir = new Vector3(x, y, 0);
                targetDir = ARCamera.TransformVector(targetDir);
                targetDir.y = 0;
                transform.LookAt(targetDir + transform.position);
                transform.Translate(Vector3.forward * speed * Time.deltaTime * 0.2f);

            }
           
        }
        else
        {
            animator.SetBool("Walk",false);
        }
    }
}
