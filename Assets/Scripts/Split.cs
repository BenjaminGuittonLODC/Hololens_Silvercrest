using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Split : MonoBehaviour {

    public List<Piece> pieces;
    bool split = false;

    GestureManager gestureManager;

    // Use this for initialization
    void Start () {

        gestureManager = FindObjectOfType<GestureManager>();
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].originalPos = pieces[i].transform.localPosition;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Split"))
        {
            if (!split)
            {
                //StartCoroutine(SplitObject());
                SplitObject();
            }
            else
            {
                //StartCoroutine(UnsplitObject());
                UnsplitObject();
            }
        }
    }

    public void SplitObject()
    {
        Debug.Log("Spliting");

        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].SendMessage("MoveToSplitPos");
        }

        split = true;
    }

    public void UnsplitObject()
    {
        Debug.Log("Unspliting");

        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].SendMessage("BackToPos");
        }
        split = false;
    }

}
