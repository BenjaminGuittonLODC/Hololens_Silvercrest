using UnityEngine;
using System.Collections;

public class InfoManager : MonoBehaviour {

    SoundManager soundManager;
    Transform[] infos;
    bool displayed = false;
    bool displayAgainNextSate1 = false;

    // Use this for initialization
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        infos =new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            infos[i] = transform.GetChild(i);
        }

        for(int i=0;i<infos.Length;i++)
        {
            infos[i].gameObject.SetActive(false);
        }
    }

    public void DisplayInformations()
    {
        if (displayed ||(Controller.state!=1 && Controller.state != 3))
            return;
        for (int i = 0; i < infos.Length; i++)
        {
            infos[i].gameObject.SetActive(true);
            infos[i].SendMessage("ScaleUp");
        }
        displayed = true;
    }

    public void HideInformations()
    {
        
        if (!displayed)
            return;
        for (int i = 0; i < infos.Length; i++)
        {
            infos[i].SendMessage("ScaleDown",SendMessageOptions.DontRequireReceiver);
        }
        displayed = false;
    }

    public void OnInformationsCommand()
    {
        if (Controller.state != 1 && Controller.state!=3)
            return;
        if (!displayed)
        {
            DisplayInformations();
            soundManager.DelayedPlayVoiceTutoFunction("PieceNumber", 2);
        }
        else
        {
            HideInformations();
            displayAgainNextSate1 = false;
        }
    }

    public void OnStateChanged()
    {
        if(Controller.state==1)
        {
            if (displayAgainNextSate1)
            {
                DisplayInformations();
                displayAgainNextSate1 = false;
            }
        }
        else if (Controller.state == 3)
        {
            if (displayAgainNextSate1)
            {
                StartCoroutine(DelayedDisplayInfos(1f));
                displayAgainNextSate1 = false;
            }
        }
        else
        {
            if (displayed)
                displayAgainNextSate1 = true;
            HideInformations();
        }
    }

    IEnumerator DelayedDisplayInfos(float time)
    {
        yield return new WaitForSeconds(time);
        DisplayInformations();
    }

}
