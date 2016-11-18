using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    public float speed;
    public bool rotate;
    public Vector3 Axis;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (rotate)
            transform.Rotate(Axis * speed*Time.deltaTime);
	}
}
