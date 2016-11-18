using UnityEngine;

/// <summary>
/// GestureAction performs custom actions based on
/// which gesture is being performed.
/// </summary>
public class Manipulation : MonoBehaviour
{
    private Vector3 manipulationPreviousPosition;
    ShadowModeManager shadowManager;
    private float rotationFactor;

    void Start()
    {
        shadowManager = FindObjectOfType<ShadowModeManager>();
    }

    void PerformManipulationStart(Vector3 position)
    {
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        Vector3 moveVector = Vector3.zero;

        // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
        moveVector = position - manipulationPreviousPosition;

        // 4.a: Update the manipulationPreviousPosition with the current position.
        manipulationPreviousPosition = position;

        // 4.a: Increment this transform's position by the moveVector.
        transform.position += moveVector * 2f;

        shadowManager.UpdatePositions();
    }
}