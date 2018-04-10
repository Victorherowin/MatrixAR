using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {

    private CharacterController cc;
    private Animator animator;
	private Transform m_ar_camera;
    public float speed = 7.8f;

    void Awake() {
        cc = this.GetComponent <CharacterController>();
        animator = this.GetComponent<Animator>();
		m_ar_camera =GameObject.Find("ARCamera").transform;
    }
	
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //按键的取值，以虚拟杆中的值为优先
        if (Joystick.h != 0 || Joystick.v != 0) {
            x = Joystick.h; y = Joystick.v;
        }
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1) {
            animator.SetBool("Walk", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun")) {
				Vector3 targetDir = new Vector3(x, y, 0);
				targetDir = m_ar_camera.TransformVector(targetDir);
				targetDir.y = 0;
                transform.LookAt(targetDir + transform.position);
                //cc.SimpleMove(transform.forward * speed);
				transform.Translate(Vector3.forward*speed*Time.deltaTime*0.2f);
            }
        } else {
            animator.SetBool("Walk", false);
        }
	}
}
