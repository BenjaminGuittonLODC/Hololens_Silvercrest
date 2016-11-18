using UnityEngine;
using System.Collections;

public class DetectFocusedPiece : MonoBehaviour {

    GameObject highlightedPiece;
	
	// Update is called once per frame
	void Update () {

        if (Controller.state == 1) //only if splited state
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f))
            {
                GameObject hitObj = hit.collider.gameObject;
                if (highlightedPiece != hit.collider.gameObject)
                {
                    if (highlightedPiece != null)
                        highlightedPiece.SendMessage("LeaveHighlighted");
                    highlightedPiece = hit.collider.gameObject;
                    highlightedPiece.SendMessage("OnHighlighted");
                }
            }
            else if (highlightedPiece != null)
            {
                highlightedPiece.SendMessage("LeaveHighlighted");
                highlightedPiece = null;
            }
        }
    }
}
