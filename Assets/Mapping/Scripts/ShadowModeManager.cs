using UnityEngine;
using System.Collections;

public class ShadowModeManager : MonoBehaviour {

    Shadow shadow;
    Pedestal pedestal;
	// Use this for initialization
	void Start () {
        shadow = FindObjectOfType<Shadow>();
        pedestal = FindObjectOfType<Pedestal>();
        ShadowOn();

        StartCoroutine(DelayedUpdate(5));
    }

    public void ShadowOn ()
    {
        shadow.gameObject.SetActive(true);
        pedestal.gameObject.SetActive(false);
    }

    public void PedestalOn ()
    {
        pedestal.gameObject.SetActive(true);
        shadow.gameObject.SetActive(false);
    }

    public void Transition()
    {
        if (shadow.gameObject.activeInHierarchy)
        {
            PedestalOn();
        }
        else
        {
            ShadowOn();
        }
    }

    IEnumerator DelayedUpdate(float time)
    {
        yield return new WaitForSeconds(time);
        UpdatePositions();
    }

    public void UpdatePositions()
    {
        shadow.UpdatePos();
        pedestal.UpdatePos();
    }
}
