using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UIController UI;

    public float MoveSpeed;
    public float JumpForce;
    public float GravityScale;

    public CharacterController CharacterController;

    public Animator Animator;

    private Vector3 _moveDirection;

    public float disToGround;

    public float JumpOffset;
    public float FallOffset;

    private bool _jump = false;
    public int JumpCounter = 0;

    public int PlayerFloor = 0;

    public bool Freeze = false;

    void Start()
    {
        UI.JumpText.text = JumpCounter.ToString();
        UI.FloorText.text = PlayerFloor.ToString();
    }

    void Update()
    {
        float yDir = _moveDirection.y;
        Vector3 moveDirection = Vector3.zero;

        if (!Freeze)
        {
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        }

        moveDirection = moveDirection.normalized * MoveSpeed;

        if (IsGrounded((moveDirection * Time.deltaTime) + transform.position, FallOffset) || _jump)
        {
            _moveDirection = moveDirection;
        }
        else
        {
            _moveDirection = Vector3.zero;
        }

        _moveDirection.y = yDir;

        if (IsGrounded(transform.position, FallOffset))
        {
            if (Input.GetButtonDown("Jump"))
            {
                _moveDirection.y = JumpForce;
                _jump = true;
                JumpCounter++;
                UI.JumpText.text = JumpCounter.ToString();
            }
            else
            {
                if(_moveDirection.y <= 0)
                {
                    _jump = false;  
                } 
            }
        }

        _moveDirection.y = _moveDirection.y + (Physics.gravity.y * GravityScale * Time.deltaTime);

        CharacterController.Move((_moveDirection) * Time.deltaTime);

        Animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
        Animator.SetBool("Jump", !IsGrounded(transform.position, JumpOffset));

    }
    bool IsGrounded(Vector3 position, float offset)
    {
        return Physics.Raycast(position, Vector3.down, disToGround + offset);
    }
}

