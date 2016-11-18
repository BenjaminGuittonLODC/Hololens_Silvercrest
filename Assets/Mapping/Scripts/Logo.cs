using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Logo : MonoBehaviour {

    Vector3 defPos;
    float defAngle;
    public float toleranceThreshold;
    public Transform model;

    // Use this for initialization
    void Start () {

        StartCoroutine(DelayedStart());
    }

    public bool Validplace(Vector3 center, Vector3 right, Vector3 left,Vector3 relativeRight)
    {
        float angle0 = SignedAngle(right - center, relativeRight);
        Debug.Log(center+" ; "+right+" ; "+(center - right) + " ; " + relativeRight);
        float angle1 = SignedAngle(center - left, relativeRight);
        /*float angle0 = Mathf.Acos((relativeRight.magnitude/4f)/(right - center).magnitude);
        float angle1 = Mathf.Acos((relativeRight.magnitude/4f)/(center - left).magnitude);
        Debug.Log((right - center).magnitude);
        Debug.Log(relativeRight.magnitude);*/
        Debug.Log("hit : " + center.ToString() + " | relative right : " + relativeRight.ToString() + " | Angle0 : " + angle0 +" | Angle1 : "+ angle1+" | Diff : "+ (Mathf.Abs(angle0 - angle1)));
        if (Mathf.Abs(angle0 - angle1) > toleranceThreshold)
            return false;
        defAngle = (angle0 + angle1) / 2;
        Debug.Log(defAngle);
        return true;
    }

    public bool TestDirection(Vector3 direction, Vector3 relativeRight)
    {
        RaycastHit hit;
        RaycastHit hit1;
        RaycastHit hit2;
        Vector3 modelPos = model.position+Vector3.up*0.5f;
        Vector3 point1 = modelPos + relativeRight/4f;
        Vector3 point2 = modelPos - relativeRight/4f;

        if (Physics.Raycast(modelPos, direction, out hit, 100) && Physics.Raycast(point1, direction, out hit1, 100) && Physics.Raycast(point2, direction, out hit2, 100))
        {
            if (Validplace(hit.point, hit1.point, hit2.point, relativeRight))
            {
                defPos = hit.point;
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(5);

        if (TestDirection(Vector3.forward, Vector3.right))
        {
            transform.position = defPos;
            transform.Rotate(0, defAngle, 0);
        }
        else if (TestDirection(Vector3.back, Vector3.left))
        {
            transform.position = defPos;
            transform.Rotate(0, 180 + defAngle, 0);

        }
        else if (TestDirection(Vector3.right, Vector3.forward))
        {
            transform.position = defPos;
            transform.Rotate(0, 90 + defAngle, 0);
        }
        else if (TestDirection(Vector3.left, Vector3.back))
        {
            transform.position = defPos;
            transform.Rotate(0, -90 + defAngle, 0);
        }
        else
        {
            //gameObject.SetActive(false);
            StartCoroutine(DelayedStart());
        }
    }

    float SignedAngle(Vector3 a, Vector3 b)
    {
        float angle = Vector3.Angle(a, b); // calculate angle
                                         // assume the sign of the cross product's Y component:
        return angle * Mathf.Sign(Vector3.Cross(b, a).y);
    }
}
