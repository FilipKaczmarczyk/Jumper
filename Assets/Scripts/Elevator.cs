using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Elevator : MonoBehaviour
{
    public CameraController MainCamera;
    public PlayerController Player;
    public UIController UI;
    public SoundManager SoundManager;

    public Door[] doors;

    private float _timeStartedLerping;
    private float _lerpTime;

    private bool _isReady = false;
    public bool IsMoving = false;

    public bool havePlayer = false;

    private Vector3 _beginPosition;
    private Vector3 _endPosition;

    private int _destinationFloor;
    public int CurrentFloor = 0;

    public float[] FloorHegihts;

    void Start()
    {
        _beginPosition = transform.position;
    }

    void Update()
    {
        if (_isReady)
        {
            if (havePlayer)
                UI.ElevatorPanelImage.gameObject.SetActive(false);
            StartLerping();
            SoundManager.PlaySFX(2);
            _isReady = false;
        }

        if (IsMoving)
        {
            transform.position = Lerp(_beginPosition, _endPosition, _timeStartedLerping, _lerpTime);

            if (transform.position == _endPosition && IsMoving)
            {
                IsMoving = false;
                _beginPosition = transform.position;
                CurrentFloor = _destinationFloor;
                doors[CurrentFloor].Open();
                if (havePlayer)
                {
                    Player.PlayerFloor = CurrentFloor;
                    UI.FloorText.text = Player.PlayerFloor.ToString();
                    UI.ElevatorText.text = "Current Floor:  " + CurrentFloor.ToString();
                    havePlayer = false;
                    Player.Freeze = false;
                    MainCamera.Gameplay = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }

        
    }
    public void ChooseDestination(int destinationFloor)
    {
        _destinationFloor = destinationFloor;
        _endPosition = _beginPosition + new Vector3(0, FloorHegihts[_destinationFloor] - _beginPosition.y, 0);
        _lerpTime = Mathf.Abs(destinationFloor - CurrentFloor) * 3.0f;

        doors[CurrentFloor].Close();

        _isReady = true;
    }
    private void StartLerping()
    {
        _timeStartedLerping = Time.time;
        IsMoving = true;
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 3.0f)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        Vector3 result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doors[CurrentFloor].Close();
            havePlayer = true;
            Player.Freeze = true;
            UI.ElevatorPanelImage.gameObject.SetActive(true);
            MainCamera.Gameplay = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
