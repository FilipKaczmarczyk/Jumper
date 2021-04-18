using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public CharacterController CharacterController;

    public bool IsCalled = false;
    public int CallingPanel = 0;

    public Vector3[] Pos;

    private Vector3 _beginPos;
    private Vector3 _endPos;

    private float _lerpTime;
    private float _timeStartedLerping;

    private bool ready = true;
    private bool moving = false;

    private Vector3 _currentPos;

    public Rigidbody Rigidbody;

    void Start()
    {
        _beginPos = Pos[0];
        _endPos = Pos[1];
    }

    void FixedUpdate()
    {
        if (!IsCalled)
        {
            if (ready)
            {
                StartLerping();
            }

            if (moving)
            {
                _currentPos = HelperFunctions.Lerp(_beginPos, _endPos, _timeStartedLerping, _lerpTime);
                Rigidbody.MovePosition(_currentPos);
            }

            if (transform.position == _endPos)
            {
                moving = false;
                ready = true;

                if (_endPos == Pos[0])
                {
                    _endPos = Pos[1];
                    _beginPos = Pos[0];
                }
                else
                {
                    _endPos = Pos[0];
                    _beginPos = Pos[1];
                }
            }
        }
        else
        {
            ready = true;
            moving = false;
            _beginPos = transform.position;
            _endPos = Pos[CallingPanel];
            IsCalled = false;
        }
    }
    private void StartLerping()
    {
        _timeStartedLerping = Time.time;
        _lerpTime = Mathf.Abs(_beginPos.z - _endPos.z) / 3;
        ready = false;
        moving = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            CharacterController.Move(Rigidbody.velocity * Time.deltaTime);
        }
    }
}
