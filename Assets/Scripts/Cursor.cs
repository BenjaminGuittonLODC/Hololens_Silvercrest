using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

    public Transform sphere;
    public Transform airTapHand;
    public Transform rotation;
    int mode = 0; //0 sphere, 1 hand

	// Update is called once per frame
	void Update () {
            RaycastHit hit;
        if(mode==0)
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f))
            {
                transform.position = Vector3.Lerp(transform.position,hit.point,0.2f);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position,Camera.main.transform.position + Camera.main.transform.forward * 5,0.5f);
            }
        else if(mode==1)
        {
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + Camera.main.transform.forward * 1.5f, 0.5f);
            transform.LookAt(Camera.main.transform);
        }
    }

    public void ShowHand()
    {
        sphere.gameObject.SetActive(false);
        airTapHand.gameObject.SetActive(true);
        mode = 1;
    }

    IEnumerator ShowHandDelayed()
    {
        yield return new WaitForSeconds(10);

        sphere.gameObject.SetActive(false);
        airTapHand.gameObject.SetActive(true);
        mode = 1;
    }

    public void ShowSphere()
    {
        sphere.gameObject.SetActive(true);
        airTapHand.gameObject.SetActive(false);
        mode = 0;
    }
}
