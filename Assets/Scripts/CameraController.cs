using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController Player;

    public Transform Target;

    public LayerMask LayerMask;

    public bool UseOffset;
    public Vector3 Offset;

    public float RotateSpeed;

    public Transform Pivot;

    public float MaxLimitAngle;
    public float MinLimitAngle;

    public bool invertY;

    public bool Gameplay = true;

    void Start()
    {
        if (!UseOffset)
        {
            Offset = Target.position - transform.position;
        }

        Pivot.transform.position = Target.transform.position;
        Pivot.transform.parent = Target.transform;
        Pivot.transform.position = new Vector3(Pivot.transform.position.x, Pivot.transform.position.y + 0.7f, Pivot.transform.position.z);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDrawGizmos()
    {
        if(Player == null)
        {
            return;
        }

        Vector3 direction = transform.position - Player.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Player.transform.position + Vector3.up * 1.1f, direction);
    }

    void LateUpdate()
    {
        if (Gameplay)
        {
            float horizontal = Input.GetAxis("Mouse X") * RotateSpeed;
            Target.Rotate(0, horizontal, 0);

            float vertical = Input.GetAxis("Mouse Y") * RotateSpeed;
            if (invertY)
            {
                Pivot.Rotate(vertical, 0, 0);
            }
            else
            {
                Pivot.Rotate(-vertical, 0, 0);
            }

            if (Pivot.rotation.eulerAngles.x > MaxLimitAngle && Pivot.rotation.eulerAngles.x < 180.0f)
            {
                Pivot.rotation = Quaternion.Euler(MaxLimitAngle, Target.rotation.eulerAngles.y, 0);
            }

            if (Pivot.rotation.eulerAngles.x > 180.0f && Pivot.rotation.eulerAngles.x < 360.0f + MinLimitAngle)
            {
                Pivot.rotation = Quaternion.Euler(360.0f + MinLimitAngle, Target.rotation.eulerAngles.y, 0);
            }

        }

        float goalEngleX = Pivot.eulerAngles.x;
        float goalEngleY = Pivot.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(goalEngleX, goalEngleY, 0);
        transform.position = Target.position - (rotation * Offset);

        if (transform.position.y < Target.position.y)
        {
            transform.position = new Vector3(transform.position.x, Target.position.y - .5f, transform.position.z);
        }

        Vector3 camOnPlayerPosOffset = Vector3.up * 1.1f;
        Vector3 direction = transform.position - (Player.transform.position + camOnPlayerPosOffset);

        if (Physics.Raycast(Player.transform.position + camOnPlayerPosOffset, direction, out RaycastHit hit, direction.magnitude, LayerMask))
        {
            Debug.Log(hit);
            transform.position = hit.point;
        }

        transform.LookAt(Pivot);
    }
    
}
