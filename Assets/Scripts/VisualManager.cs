using UnityEngine;
using System.Collections;

public class VisualManager : MonoBehaviour {

    public GameObject manipulationVisual;
    public GameObject defaultVisual;
    public GameObject reloadButton;
    public GameObject rotationHand;

    bool isDefaultState = true;

    void Start()
    {
        defaultVisual.SetActive(true);
    }

    public void SettingMode()
    {
        manipulationVisual.SetActive(true);
        manipulationVisual.SendMessage("Show");
        reloadButton.SetActive(true);
        reloadButton.SendMessage("Show");
        defaultVisual.SendMessage("Hide", SendMessageOptions.DontRequireReceiver);
    }

    public void LeaveSettingMode()
    {
        manipulationVisual.SendMessage("Hide",SendMessageOptions.DontRequireReceiver);
        reloadButton.SendMessage("Hide", SendMessageOptions.DontRequireReceiver);
        if (isDefaultState)
        {
            defaultVisual.SetActive(true);
            defaultVisual.SendMessage("Show");
        }
    }

    public void DefaultState()
    {
        manipulationVisual.SendMessage("Hide", SendMessageOptions.DontRequireReceiver);
        reloadButton.SendMessage("Hide", SendMessageOptions.DontRequireReceiver);
        rotationHand.SendMessage("Hide", SendMessageOptions.DontRequireReceiver);
        defaultVisual.SetActive(true);
        defaultVisual.SendMessage("Show");
        gameObject.SendMessage("StopLookingAtCam");
        isDefaultState = true;
    }

    public void LeaveDefaultState()
    {
        defaultVisual.SendMessage("Hide", SendMessageOptions.DontRequireReceiver);
        isDefaultState = false;
    }

    public void RotationMode()
    {
        gameObject.SendMessage("LookAtCam");
        rotationHand.SetActive(true);
        rotationHand.SendMessage("Show");
    }

    public void LeaveRotationMode()
    {
        gameObject.SendMessage("StopLookingAtCam");
        rotationHand.SendMessage("Hide", SendMessageOptions.DontRequireReceiver);
    }
}
