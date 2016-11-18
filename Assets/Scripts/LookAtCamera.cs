using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    public IEnumerator LookAtCam()
    {
        while (true)
        {
            transform.LookAt(2 * transform.position - Camera.main.transform.position);
            yield return 0;
        }
    }

    public void StopLookingAtCam()
    {
        StopAllCoroutines();
       transform.eulerAngles = Vector3.zero;
    }
}
