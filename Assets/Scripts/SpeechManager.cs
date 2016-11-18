using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    SoundManager soundManager;
    GestureManager gestureManager;
    Controller controller;
    InfoManager infoManager;
    ShadowModeManager shadowModeManager;
    public bool voiceOne = false;

    public Piece[] pieces;

    GameObject logo;

    // Use this for initialization
    void Start()
    {
        logo = GameObject.Find("Logo");
        logo.SetActive(false);
        soundManager = FindObjectOfType<SoundManager>();
        gestureManager = FindObjectOfType<GestureManager>();
        controller = FindObjectOfType<Controller>();
        infoManager = FindObjectOfType<InfoManager>();
        shadowModeManager = GetComponent<ShadowModeManager>();

        keywords.Add("Move", () =>
        {
            gestureManager.BroadcastMessage("StartManipulation");
            controller.GetComponent<Rotation>().enabled = false;
        });

        keywords.Add("Done", () =>
        {
            gestureManager.BroadcastMessage("QuitMode");
            controller.GetComponent<Rotation>().enabled = true;
        });

        keywords.Add("Number One", () =>
        {
            
            if (pieces.Length>0)
                controller.PieceSelected(pieces[0]);
            voiceOne = true;
        });

        keywords.Add("Number Two", () =>
        {
            if (pieces.Length > 1)
                controller.PieceSelected(pieces[1]);
            voiceOne = false;
        });

        keywords.Add("Number Three", () =>
        {
            if (pieces.Length > 2)
                controller.PieceSelected(pieces[2]);
            voiceOne = false;
        });

        keywords.Add("Number Four", () =>
        {
            if (pieces.Length > 3)
                controller.PieceSelected(pieces[3]);
            voiceOne = false;
        });

        keywords.Add("Number Five", () =>
        {
            if (pieces.Length > 4)
                controller.PieceSelected(pieces[4]);
            voiceOne = false;
        });

        keywords.Add("Number Six", () =>
        {
            if (pieces.Length > 5)
                controller.PieceSelected(pieces[5]);
            voiceOne = false;
        });

        keywords.Add("Extend", () =>
        {
            controller.ScaleUp();
        });

        keywords.Add("Big", () =>
        {
            controller.FixedScaleUp();
        });

        keywords.Add("Down", () =>
        {
            controller.ScaleDown();
        });

        keywords.Add("Small", () =>
        {
            controller.FixedScaleDown();
        });

        keywords.Add("Go Back", () =>
        {
            infoManager.HideInformations();
            controller.GoBackToSplit();
            controller.StopClipDebug();
        });

        keywords.Add("show me", () =>
        {
            infoManager.OnInformationsCommand();
            
        });

        keywords.Add("Pedestal", () =>
        {
            shadowModeManager.PedestalOn();
        });

        keywords.Add("Shadow", () =>
        {
            shadowModeManager.ShadowOn();
        });

        keywords.Add("Rotate", () =>
        {
            controller.GetComponent<Rotation>().enabled = true;
        });

        keywords.Add("Stop", () =>
        {
            controller.GetComponent<Rotation>().enabled = false;
        });

        keywords.Add("Mapping", () =>
        {
            GameObject.Find("SpatialMappingManager").SendMessage("OnBackground");
        });

        keywords.Add("Logo", () =>
        {
            if(logo!=null)
            {
                logo.SetActive(!logo.activeInHierarchy);
            }
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("ScaleUp"))
            controller.ScaleUp();
        else if (Input.GetButtonDown("ScaleDown"))
            controller.ScaleDown();
        else if(Input.GetButtonDown("FocusPieceTwo"))
        {
            if (pieces.Length > 1)
                controller.PieceSelected(pieces[1]);
        }
        else if(Input.GetButtonDown("GoBack"))
        {
            infoManager.HideInformations();
            controller.GoBackToSplit();
        }
        else if(Input.GetButtonDown("Informations"))
        {
            infoManager.OnInformationsCommand();
        }
        else if (Input.GetButtonDown("Settings"))
        {
            gestureManager.BroadcastMessage("StartManipulation");
            //controller.GetComponent<Rotation>().enabled = false;
        }
    }
}