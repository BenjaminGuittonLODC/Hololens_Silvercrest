using UnityEngine;
using System.Collections;

public class DesactivateAfterFrame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Delay());
	} 

    IEnumerator Delay()
    {
        yield return 0;
        gameObject.SetActive(false);
    }
}
