using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerMove : MonoBehaviour {

    public float moveSpeed;
    public Transform cameraTransform;
    Vector3 camForward;
    Vector3 move;
    public bool Dodgable = true;
    Animator m_Animator;
    AnimatorStateInfo animatorStateInfo;
    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;

    public AudioEvent StepSound;
    AudioSource audioSource;

    public float x = 1.45f;

    public CameraRotate cameraRotate;

    float m_TurnAmount;
    float m_ForwardAmount;
    bool IsDodging;
    bool IsSteping;
    bool IsLock = false;
    // Use this for initialization
    void Start ()
    {
        m_Animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        animatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
        if (cameraRotate.IsLock)
        {
            IsLock = true;
            m_Animator.SetBool("IsLock", IsLock);
        }
        else
        {
            IsLock = false;
            m_Animator.SetBool("IsLock", IsLock);
        }

        if (IsLock)
        {
            Lock();
            moveSpeed = 2.5f;
        }
        else
        {
            UnLock();
            moveSpeed = 3f;
        }
    }

    void Lock()
    {
        camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        // transform.position += move * moveSpeed;

        LockMove();

        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);

        LockTurn();
        Step();

        ApplyExtraTurnRotation();

        AnimatorUpdate();
    }

    void UnLock()
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

    void LockMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float distance = Vector3.Distance(transform.position, cameraRotate.LockObj.transform.position);
        distance = Mathf.Max(5, distance);
        // Debug.Log(distance);
        // float C = 2 * distance * Mathf.PI;
        if (h < 0)
        {
            float fix = ((h / (distance)) * 0.1f);
            // Debug.Log(fix);
            h -= fix;
            v += fix;
        }
        if (h > 0)
        {
            float fix = ((h / (distance)) * 0.1f);
            // Debug.Log(fix);
            h -= fix;
            v += fix;
        }

        move =  h * cameraTransform.right + v * cameraTransform .forward;
    }

    float lastTime = 0;
    private void PlayStep()
    {
        if (Time.time - lastTime >= 0.1f)
        {
            StepSound.Play(audioSource);
            lastTime = Time.time;
        }
    }

    void Turn()
    {
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        Debug.Log(m_TurnAmount);
        m_ForwardAmount = move.z;
    }

    void LockTurn()
    {
        Vector3 lookPos = cameraRotate.LockObj.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        /*Quaternion q = cameraTransform.rotation;
        q.x = transform.rotation.x;
        q.z = transform.rotation.z;
        transform.rotation = q;*/
    }

    float DebugDodgeTime = 60;

    void Dodge()
    {
        DebugDodgeTime++;

        if (!Dodgable) return;
        if (IsDodging)
        {
            IsDodging = false;
            return;
        }


        if (!animatorStateInfo.IsName("Dodging") && DebugDodgeTime >= 60)
        {
            if (Input.GetButtonDown("JoyStickX"))
            {
                DebugDodgeTime = 0;
                IsDodging = Input.GetButtonDown("JoyStickX");
                m_Animator.SetTrigger("Dodge");
            }
        }

    }

    void Step()
    {
        if (!Dodgable) return;
        if (IsSteping)
        {
            IsSteping = false;
        }

        animatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
        m_Animator.SetFloat("StepX", move.x, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("StepY", move.z, 0.1f, Time.deltaTime);

        if (!animatorStateInfo.IsName("Step"))
        {

            if (Input.GetButtonDown("JoyStickX"))
            {
                IsSteping = Input.GetButtonDown("JoyStickX");
            }
        }
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)

        if (animatorStateInfo.IsTag("Attack") && animatorStateInfo.normalizedTime > 0.2f) return;
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void AnimatorUpdate()
    {
        m_Animator.SetFloat("Speed", move.z, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("IsDodging", IsDodging);
        m_Animator.SetBool("IsSteping", IsSteping);
        m_Animator.SetBool("IsLock", IsLock);
        m_Animator.SetFloat("X", move.x, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Y", move.z, 0.1f, Time.deltaTime);
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
