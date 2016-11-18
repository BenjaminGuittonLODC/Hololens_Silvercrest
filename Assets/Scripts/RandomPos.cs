using UnityEngine;
using System.Collections;

public class RandomPos : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        transform.position = new Vector3(Random.Range(-1, 1), Random.Range(0,2), Random.Range(1, 3));
	}
	
}
