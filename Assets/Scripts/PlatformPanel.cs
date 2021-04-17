using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPanel : MonoBehaviour
{
    public int PanelNumber;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Platform.instance.IsCalled = true;
            Platform.instance.CallingPanel = PanelNumber;
        }
    }
}
