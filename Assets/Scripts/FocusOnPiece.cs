using UnityEngine;
using System.Collections;

public class FocusOnPiece : MonoBehaviour {

    public GameObject[] pieces;
    Piece activePiece;
    Rotation modelRotation;
    GestureManager gestureManager;

	// Use this for initialization
	void Start () {
        modelRotation = GetComponent<Rotation>();
        gestureManager = FindObjectOfType<GestureManager>();
    }

    public void Focus(Piece piece)
    {
        modelRotation.rotate = false;
        activePiece = piece;
        gestureManager.activePiece = piece;
        for (int i=0;i<pieces.Length;i++)
        {
            if (pieces[i] != piece.gameObject)
                pieces[i].SendMessage("ScaleDown");
        }
        activePiece.SendMessage("MoveToFocusPos");
        gestureManager.SendMessage("StartNavigation");
    }

    public IEnumerator LeaveFocus()
    {
        ActivateAllPieces();

        gestureManager.activePiece = null;

        for (int i = 0; i < pieces.Length; i++)
        {
            if (activePiece.gameObject != pieces[i])
            {
                pieces[i].SendMessage("ScaleUp");
            }
        }
        activePiece.SendMessage("BackToSplitPos");
        gestureManager.SendMessage("QuitMode");

        yield return new WaitForSeconds(1f);

        modelRotation.rotate = true;
    }
    
    void ActivateAllPieces()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].SetActive(true);
        }
    }
}
