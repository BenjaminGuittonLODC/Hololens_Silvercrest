using UnityEngine;
using System.Collections;

public class Pedestal : MonoBehaviour
{

    MeshRenderer meshRenderer;
    // Use this for initialization
    void Start()
    {
        meshRenderer = transform.GetComponentInChildren<MeshRenderer>();
        UpdatePos();       
    }

    public void UpdatePos()
    {
        RaycastHit hit;        
        GameObject model = GameObject.FindGameObjectWithTag("Model");
        Vector3 empty = model.transform.position - new Vector3(0, 0.13f, 0);
        if (Physics.Raycast(model.transform.position, -Vector3.up, out hit, 100))
        {
            meshRenderer.enabled = true;
            transform.position = hit.point;
            float distance = Vector3.Distance(hit.point, empty);
            transform.localScale = new Vector3(1, distance, 1);
        }
        else
        {
            if(meshRenderer!=null)
             meshRenderer.enabled = false;
        }
    }
}
