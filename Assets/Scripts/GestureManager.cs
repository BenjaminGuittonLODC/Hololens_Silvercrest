using System;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class GestureManager : MonoBehaviour
{
    public GestureRecognizer ActionRecognizer { get; private set; }
    public GestureRecognizer ManipulationRecognizer { get; private set; }
    public GestureRecognizer NavigationRecognizer { get; private set; }
    public GestureRecognizer ActiveRecognizer { get; private set; }
    public bool IsManipulating { get; private set; }
    public Vector3 ManipulationPosition { get; private set; }
    public Vector3 NavigationPosition { get; private set; }

    GameObject model;
    public Piece activePiece { get; set; }

    VisualManager visualHolder;

    void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model");
        visualHolder = FindObjectOfType<VisualManager>();

        //___________ACTION________________
        //Recognizer used to detect the input for transitions -> Neutral state manipulations.
        ActionRecognizer = new GestureRecognizer();
        ActionRecognizer.SetRecognizableGestures(
            GestureSettings.Tap);
        ActionRecognizer.TappedEvent += ActionRecognizer_TappedEvent;

        // Instantiate the ManipulationRecognizer. The manipulationRecognizer is used for placing the object to the right place.
        ManipulationRecognizer = new GestureRecognizer();

        //_________MANIPULATION_______________
        // Add the ManipulationTranslate GestureSetting to the ManipulationRecognizer's RecognizableGestures.
        ManipulationRecognizer.SetRecognizableGestures(
            GestureSettings.ManipulationTranslate | GestureSettings.Tap);

        // Register for the Manipulation events on the ManipulationRecognizer.
        ManipulationRecognizer.TappedEvent += ManipulationRecognizer_TappedEvent;
        ManipulationRecognizer.ManipulationStartedEvent += ManipulationRecognizer_ManipulationStartedEvent;
        ManipulationRecognizer.ManipulationUpdatedEvent += ManipulationRecognizer_ManipulationUpdatedEvent;
        ManipulationRecognizer.ManipulationCompletedEvent += ManipulationRecognizer_ManipulationCompletedEvent;
        ManipulationRecognizer.ManipulationCanceledEvent += ManipulationRecognizer_ManipulationCanceledEvent;


        //_________NAVIGATION______________
        //Recognizer used to rotate the piece focused
        NavigationRecognizer = new GestureRecognizer();

        NavigationRecognizer.SetRecognizableGestures(
            GestureSettings.NavigationX);

        NavigationRecognizer.NavigationStartedEvent += NavigationRecognizer_NavigationStartedEvent;
        NavigationRecognizer.NavigationUpdatedEvent += NavigationRecognizer_NavigationUpdatedEvent;
        NavigationRecognizer.NavigationCompletedEvent += NavigationRecognizer_NavigationCompletedEvent;
        NavigationRecognizer.NavigationCanceledEvent += NavigationRecognizer_NavigationCanceledEvent;


        Transition(ActionRecognizer);
    }

    private void ManipulationRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100.0f))
        {
            hit.collider.SendMessage("OnAirTap");
        }

    }


    private void ManipulationRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
            IsManipulating = true;

            ManipulationPosition = position;

            model.SendMessage("PerformManipulationStart", position);
    }

    private void ManipulationRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
            IsManipulating = true;

            ManipulationPosition = position;

            model.SendMessage("PerformManipulationUpdate", position);
    }

    private void ManipulationRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = false;
    }

    private void ManipulationRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = false;
    }


    public void Transition(GestureRecognizer newRecognizer)
    {
        if (newRecognizer == null)
        {
            return;
        }

        if (ActiveRecognizer != null)
        {
            if (ActiveRecognizer == newRecognizer)
            {
                return;
            }

            ActiveRecognizer.CancelGestures();
            ActiveRecognizer.StopCapturingGestures();
        }

        newRecognizer.StartCapturingGestures();
        ActiveRecognizer = newRecognizer;

        //Display or hide the manipulation visual

        if (ActiveRecognizer == ManipulationRecognizer)
        {
            visualHolder.SettingMode();
        }
        else
            visualHolder.LeaveSettingMode();
    }

    public void StartManipulation()
    {
        if (ActiveRecognizer == ManipulationRecognizer)

            Transition(ActionRecognizer);
        else
            Transition(ManipulationRecognizer);

    }

    public void StartNavigation() //Transition into navigation mode, used when a piece is focused to rotate it
    {
        if (ActiveRecognizer == NavigationRecognizer)
            Transition(ActionRecognizer);
        else
            Transition(NavigationRecognizer);
    }

    public void QuitMode() //Transition into Action mode
    {
            Transition(ActionRecognizer);
    }

    void OnDestroy()
    {
        ActionRecognizer.TappedEvent -= ActionRecognizer_TappedEvent;

        ManipulationRecognizer.ManipulationStartedEvent -= ManipulationRecognizer_ManipulationStartedEvent;
        ManipulationRecognizer.ManipulationUpdatedEvent -= ManipulationRecognizer_ManipulationUpdatedEvent;
        ManipulationRecognizer.ManipulationCompletedEvent -= ManipulationRecognizer_ManipulationCompletedEvent;
        ManipulationRecognizer.ManipulationCanceledEvent -= ManipulationRecognizer_ManipulationCanceledEvent;
    }

    private void ActionRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
    {
            model.SendMessage("OnAirTap");
    }

    private void NavigationRecognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        activePiece.isNavigating = true;

        activePiece.navigationPosition = relativePosition;
    }

    private void NavigationRecognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        activePiece.isNavigating = true;

        activePiece.navigationPosition = relativePosition;
    }

    private void NavigationRecognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        activePiece.isNavigating = false;
    }

    private void NavigationRecognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 relativePosition, Ray ray)
    {
        activePiece.isNavigating = false;
    }


}