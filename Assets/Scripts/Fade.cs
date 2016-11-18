using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fade : MonoBehaviour {
    public Material mat;
    public Material transparantMat;
    public float speed;
    Color color;

    bool visible;

    public Piece[] pieces;

    void Start()
    {
        color = mat.color;
        color.a = 1;
        mat.color = color;
        visible = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fade"))
        {
            if (visible)
                StartCoroutine(FadeOut());
            else
                StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeOut()
    {
        Debug.Log("Fading Out");

        float a = 1;
        while(a>0)
        {
            a -= Time.deltaTime * speed;
            color.a = a;
            mat.color = color;
            yield return 0;
        }
        visible = false;
    }

    public void FocusOnPiece(Piece piece)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (piece != pieces[i])
            {
                //pieces[i].SetMaterial(transparantMat);
            }
        }
    }

    public void LeaveFocus()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            {
                //pieces[i].ResetMaterial();
            }
        }
    }

    IEnumerator FadeIn()
    {
        Debug.Log("Fading In");

        float a = 0;
        while (a <1)
        {
            a += Time.deltaTime * speed;
            color.a = a;
            mat.color = color;
            yield return 0;
        }
        visible = true;
    }
}
