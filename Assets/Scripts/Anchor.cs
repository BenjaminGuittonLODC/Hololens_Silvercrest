using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour {


    WorldAnchorManager anchorManager;
    public string anchorName;

    void Start()
    {
        anchorManager = WorldAnchorManager.Instance;
        SaveAnchor();
    }

    public void SaveAnchor()
    {
        anchorManager.AttachAnchor(gameObject, anchorName);
    }
}
