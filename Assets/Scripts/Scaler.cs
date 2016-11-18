using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour {

    Vector3 originalScale;
    float scaleTime = 1;

	// Use this for initialization
	void Awake () {
        originalScale = transform.localScale;
        Debug.Log(originalScale);
	}
	
	public IEnumerator Show()
    {
        float s = 0;
        while(s<1)
        {
            s += Time.deltaTime/scaleTime;
            transform.localScale = Mathfx.Sinerp(Vector3.zero, originalScale, s);

            yield return 0;
        }
    }

    public IEnumerator Hide()
    {
        float s = 1;
        while (s >0)
        {
            s -= Time.deltaTime / scaleTime;
            transform.localScale = Mathfx.Coserp(Vector3.zero, originalScale, s);

            yield return 0;
        }
        gameObject.SetActive(false);
    }
}
