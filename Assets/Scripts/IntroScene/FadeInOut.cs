using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

    public float fadeTime = 1;
    Text text;
    float dir = 1;
    Color color;
    float t = 0;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        color = text.color;
    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime/fadeTime*dir;
        if (t > 1)
        {
            dir = -1;
            t = 1;
        }
        else if(t < 0)
        {
            dir = 1;
            t = 0;
        }
        color.a = Mathfx.Hermite(0, 1, t);
        text.color = color;

    }
}
