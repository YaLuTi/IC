using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float moveSpeed;
    public Transform cameraTransform;
    Vector3 camForward;
    Vector3 move;
    Animator m_Animator;
    AnimatorStateInfo animatorStateInfo;
    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;

    float m_TurnAmount;
    float m_ForwardAmount;
    bool IsDodging;
    // Use this for initialization
    void Start ()
    {
        m_Animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        // transform.position += move * moveSpeed;

        Move();

        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);

        Turn();
        Dodge();
        ApplyExtraTurnRotation();
        AnimatorUpdate();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        move = v * camForward + h * cameraTransform.right;
    }

    void Turn()
    {
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;
    }

    void Dodge()
    {
        if (IsDodging)
        {
            IsDodging = false;
        }

        animatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

        if (!animatorStateInfo.IsName("Dodge"))
        {

            if (Input.GetButtonDown("JoyStickX"))
            {
                IsDodging = Input.GetButtonDown("JoyStickX");
            }
        }

    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void AnimatorUpdate()
    {
        m_Animator.SetFloat("Speed", move.z, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("IsDodging", IsDodging);
    }

    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (Time.deltaTime > 0)
        {
            Vector3 v = (m_Animator.deltaPosition * 0.01f * moveSpeed) / Time.deltaTime;
            transform.position += v;
        }
    }
}
