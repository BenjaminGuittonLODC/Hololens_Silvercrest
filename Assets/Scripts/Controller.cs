using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

    GestureManager gestureManager;
    SoundManager soundManager;
    public static int state;
    FocusOnPiece focuser;
    public Piece defaultPiece;
    InfoManager infoManager;
    public List<GameObject> toDesactivateModels = new List<GameObject>();
    VisualManager visualHolder;
    Cursor cursor;

    // Use this for initialization
    void Start () {
        gestureManager = FindObjectOfType<GestureManager>();
        soundManager = FindObjectOfType<SoundManager>();
        focuser = GetComponent<FocusOnPiece>();
        visualHolder = FindObjectOfType<VisualManager>();
        state = 0;
        infoManager = FindObjectOfType<InfoManager>();
        cursor = FindObjectOfType<Cursor>();
        DesactivateModels();

    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("NextState"))
        {
            Debug.Log("On Space State Change");
            ChangeState();
        }
	}

    void ChangeState(Piece piece= null)
    {
        //Default state
        if (state == 0)
        {
            StopAllCoroutines();                        //Make sure delayeddesctivatemodels coroutine isn't running and won't turn the models off later
            ActivateModels();
            gameObject.SendMessage("SplitObject");
            cursor.ShowSphere();
            state = 1;
        }
        //Splited State
        else if (state == 1)
        {
            if (piece == null)
                piece = defaultPiece;

            gameObject.SendMessage("FocusPiece");
            visualHolder.RotationMode();
            if (focuser != null && piece !=null)
            {
                focuser.Focus(piece);
            }
            state = 2;
        }
        //Focused On Piece State
        else if (state == 2)
        {
            gameObject.SendMessage("LeaveFocus");
            visualHolder.LeaveRotationMode();
            state = 3;
        }
        //Splited State (return)
        else if (state == 3)
        {
            gameObject.SendMessage("UnsplitObject");
            state = 0;                                      //Back to starting state
            StartCoroutine(DelayedDesactivateModels());     //Hide unnescessary models
        }
        infoManager.OnStateChanged();
    }

    public void OnAirTap()
    {
        Debug.Log("OnAirTap State Change");
        if (state == 1)
            state = 3;
        ChangeState();
    }

    public void PieceSelected(Piece piece)
    {
        Debug.Log("OnAirTap Piece State Change");
        if (state == 1 || state == 3)
        {
            state = 1;
            ChangeState(piece);
        }
    }       

    public void GoBackToSplit()
    {
        if (state == 2)
            ChangeState();
    }
    
    public void ScaleUp()
    {
        transform.localScale*= 1.2f;
    }

    public void FixedScaleUp()
    {
        transform.localScale =Vector3.one*0.7f;
    }

    public void ScaleDown()
    {
        transform.localScale *= 0.8f;
    }

    public void FixedScaleDown()
    {
        transform.localScale = Vector3.one * 0.25f;
    }

    void DesactivateModels()
    {
        for (int i = 0; i < toDesactivateModels.Count; i++)
        {
            toDesactivateModels[i].SetActive(false);
        }
        visualHolder.DefaultState();
    }

    IEnumerator DelayedDesactivateModels()
    {
        yield return new WaitForSeconds(1);
        DesactivateModels();
    }

    void ActivateModels()
    {
        for (int i = 0; i < toDesactivateModels.Count; i++)
        {
            toDesactivateModels[i].SetActive(true);
        }
        visualHolder.LeaveDefaultState();
    }

    public void StopClipDebug()
    {
        //soundManager.StopClipDebug();
    }
}
