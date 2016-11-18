using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {

    public static Vector3 ChangeReference(Vector3 vector, float angle)
    {
        Debug.Log(angle);
        angle *= Mathf.Deg2Rad;
        float cosA = Mathf.Cos(angle);
        float sinA = Mathf.Sin(angle);
        return new Vector3(cosA * vector.x + sinA * vector.z,
                           vector.y,
                           -sinA * vector.x + cosA * vector.z);
    }

    public static Vector3 ChangeReference(Vector3 vector, Vector3 forward)
    {
        float angle = Vector3.Angle(Vector3.forward, forward);
        Debug.Log(Vector3.Cross(Vector3.forward, forward));
        if (Vector3.Cross(Vector3.forward, forward).y > 0)
            angle *= -1;
        return ChangeReference(vector, angle);
    }
}
