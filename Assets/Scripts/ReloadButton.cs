using UnityEngine;
using System.Collections;

public class ReloadButton : MonoBehaviour {

    public Vector3 scale;
    public AudioClip clip;
    KeepPositionOnReload keepPosition;

    void Start()
    {
        keepPosition = FindObjectOfType<KeepPositionOnReload>();
    }

	public void OnAirTap()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);

        keepPosition.SavePosition();
    }

    public IEnumerator Show()
    {
        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime * 2;

            transform.localScale= Mathfx.Hermite(Vector3.zero, scale, t);
            yield return 0;
        }
    }

    public IEnumerator Hide()
    {
        float t = 1;
        while (t >0)
        {
            t -= Time.deltaTime * 2;

            transform.localScale = Mathfx.Hermite(Vector3.zero, scale, t);
            yield return 0;
        }
        gameObject.SetActive(false);
    }
}
