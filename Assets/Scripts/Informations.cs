using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Informations : MonoBehaviour {

    public Transform piece;
    public Vector3 offset;
    Text text;

    void Awake()
    {
        transform.localPosition = new Vector3 (piece.localPosition.x*1/transform.parent.localScale.x,transform.localPosition.y, piece.localPosition.z * 1 / transform.parent.localScale.z);
        text = GetComponentInChildren<Text>();
    }

	// Update is called once per frame
	void Update () {
        Vector3 lookPos = Camera.main.transform.position;
        lookPos.y = transform.position.y;

        transform.LookAt(lookPos, Vector3.up);
	}

    IEnumerator ScaleUp()
    {
        Color color = text.color;
        color.a = 0;
        text.color = color;

        //Scaling the white bar
        transform.localPosition = new Vector3(piece.localPosition.x * 1 / transform.parent.localScale.x+offset.x, transform.localPosition.y+offset.y, piece.localPosition.z * 1 / transform.parent.localScale.z + offset.z);
        float t = 0;
        Vector3 scale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        transform.localScale = scale;
        while(t<1)
        {
            t += Time.deltaTime;

            scale.y = Mathfx.Hermite(0, 1, t);
            transform.localScale = scale;

            yield return 0;
        }

        //Fading the text
        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            color.a = Mathfx.Hermite(0, 1, t);
            text.color = color;

            yield return 0;
        }
    }

    IEnumerator ScaleDown()
    {
        //Fade out the text
        float t = 1;
        Color color = text.color;
        while (t >0)
        {
            t -= Time.deltaTime;
            color.a = Mathfx.Hermite(0,1,t);
            text.color = color;

            yield return 0;
        }


        //Scale down the bar
        t = 1;
        Vector3 scale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        transform.localScale = scale;
        while (t >0)
        {
            t -= Time.deltaTime;

            scale.y = Mathfx.Hermite(0, 1, t);
            transform.localScale = scale;

            yield return 0;
        }
        gameObject.SetActive(false);

    }
}
