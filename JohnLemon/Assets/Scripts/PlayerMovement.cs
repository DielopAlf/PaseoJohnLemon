using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 m_Movement;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    public float turnSpeed= 20f;

    Quaternion m_Rotation = Quaternion.identity;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal,0f,vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal,0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical,0f);

        bool IsWalking = hasHorizontalInput || hasVerticalInput;
        Debug.Log(IsWalking);
        m_Animator.SetBool("IsWalking", IsWalking);

        Vector3 desiredForward=Vector3.RotateTowards(transform.forward, m_Movement,turnSpeed * Time.deltaTime,0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }
    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);

    }
}
