using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : MonoBehaviour
{
    public Transform targetPosObj;
    public Vector3 originalPos { get; set; }
    float rotationSpeed = 10f;
    Vector3 originalScale;
    Quaternion originalRotation;
    public Vector3 navigationPosition { get; set; } //Target position given by the hand gesture. Set by GestureManger script
    bool rotate;
    public bool isNavigating { get; set; } //True if the user is rotating the piece

    void Start()
    {
        originalPos = transform.localPosition;
        originalScale = transform.localScale;
        originalRotation = transform.localRotation;
    }

    //Moves into its split position, when we split the model
    public IEnumerator MoveToSplitPos()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            Vector3 pos;
            pos.x = Mathfx.Hermite(originalPos.x, targetPosObj.localPosition.x, t);
            pos.y = Mathfx.Hermite(originalPos.y, targetPosObj.localPosition.y, t);
            pos.z = Mathfx.Hermite(originalPos.z, targetPosObj.localPosition.z, t);
            transform.localPosition = pos;
            yield return 0;
        }
    }

    //Moves back into its original location.
    public IEnumerator BackToPos()
    {
        float t = 1;

        Vector3 currentPos = transform.localPosition;

        while (t > 0)
        {
            t -= Time.deltaTime;
            Vector3 pos;
            pos.x = Mathfx.Hermite(originalPos.x, currentPos.x, t);
            pos.y = Mathfx.Hermite(originalPos.y, currentPos.y, t);
            pos.z = Mathfx.Hermite(originalPos.z, currentPos.z, t);
            transform.localPosition = pos;
            yield return 0;
        }
    }

    //Move the piece to the focus location (when it's the only piece visible)
    public IEnumerator MoveToFocusPos()
    {
        float t = 0;

        Vector3 currentPos = transform.position;
        Vector3 focusPos = currentPos + (Camera.main.transform.position- currentPos) / 2;

        StartCoroutine(Rotate());          //Start rotating the piece

        while (t<1)
        {
            t += Time.deltaTime/1.5f;
            Vector3 pos;
            pos.x = Mathfx.Hermite(currentPos.x, focusPos.x, t);
            pos.y = Mathfx.Hermite(currentPos.y, focusPos.y, t);
            pos.z = Mathfx.Hermite(currentPos.z, focusPos.z, t);
            transform.position = pos;
            yield return 0;
        }
    }

    //Move the piece back to its split position
    public IEnumerator BackToSplitPos()
    {
        rotate = false;         //stop the rotation coroutine
        float t = 1;

        Vector3 currentPos = transform.localPosition;
        Quaternion startRotation = transform.localRotation;

        while (t > 0)
        {
            t -= Time.deltaTime;
            Vector3 pos;
            pos.x = Mathfx.Hermite(targetPosObj.localPosition.x, currentPos.x, t);
            pos.y = Mathfx.Hermite(targetPosObj.localPosition.y, currentPos.y, t);
            pos.z = Mathfx.Hermite(targetPosObj.localPosition.z, currentPos.z, t);
            transform.localRotation = Quaternion.Lerp(originalRotation, startRotation, t);
            transform.localPosition = pos;
            yield return 0;
        }
    }

    public IEnumerator ScaleDown()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime*2;
            Vector3 scale;
            scale.x = Mathfx.Hermite(originalScale.x, 0, t);
            scale.y = Mathfx.Hermite(originalScale.y, 0, t);
            scale.z = Mathfx.Hermite(originalScale.z, 0, t);
            transform.localScale = scale;
            yield return 0;
        }

        gameObject.SetActive(false);
    }

    public IEnumerator ScaleUp()
    {
        float t = 1;

        yield return new WaitForSeconds(0.5f);

        while (t > 0)
        {
            t -= Time.deltaTime*2;
            Vector3 scale;
            scale.x = Mathfx.Hermite(originalScale.x, 0, t);
            scale.y = Mathfx.Hermite(originalScale.y, 0, t);
            scale.z = Mathfx.Hermite(originalScale.z, 0, t);
            transform.localScale = scale;
            yield return 0;
        }
    }



    IEnumerator Rotate()
    {
        rotate = true;
        while (rotate)
        {
            if (isNavigating)
            {
                transform.Rotate(new Vector3(0, -5 * navigationPosition.x, 0));
            }
            else
            {
                transform.Rotate(Vector3.up, rotationSpeed*Time.deltaTime);
            }
            yield return 0;
        }
    }
}
