using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {
        UpdatePos();
    }

    public void UpdatePos()
    {
        RaycastHit hit;
        GameObject model = GameObject.FindGameObjectWithTag("Model");
        if (Physics.Raycast(model.transform.position, -Vector3.up, out hit, 100))
        {
            transform.position = hit.point;
        }
    }
}
