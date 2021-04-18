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

    private static int JUMP_ANIM_HASH = Animator.StringToHash("Jump");
    private static int SPEED_ANIM_HASH = Animator.StringToHash("Speed");

    private Vector3 _moveDirection;

    public float disToGround;

    public float JumpOffset;
    public float FallOffset;

    private bool _jump = false;
    public int JumpCounter = 0;

    public int PlayerFloor = 0;

    public bool Freeze = false;

    private float _jumpYPos;

    private float _timer = 0;

    void Start()
    {
        UI.JumpText.text = JumpCounter.ToString();
        UI.FloorText.text = PlayerFloor.ToString();
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float yDir = _moveDirection.y;
        Vector3 moveDirection = Vector3.zero;

        if (!Freeze)
        {
            moveDirection = (transform.forward * vertical) + (transform.right * horizontal);
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

        Animator.SetFloat(SPEED_ANIM_HASH, Mathf.Abs(vertical) + Mathf.Abs(horizontal));
        Animator.SetBool(JUMP_ANIM_HASH, !IsGrounded(transform.position, JumpOffset));

        _timer += Time.deltaTime;
        DisplayTime();
    }
    bool IsGrounded(Vector3 position, float offset)
    {
        return Physics.Raycast(position, Vector3.down, disToGround + offset);
    }
    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(_timer / 60F);
        int seconds = Mathf.FloorToInt(_timer - minutes * 60);

        string displayTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        UI.TimeText.text = displayTime;
    }
}

