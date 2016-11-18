// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Display : MonoBehaviour
{
    public GestureRecognizer IntroRecognizer { get; private set; }
    public GameObject Scene;

    void Awake()
    {
        //___________ACTION________________
        //Recognizer used to detect the input for transitions -> Neutral state manipulations.
        IntroRecognizer = new GestureRecognizer();
        IntroRecognizer.SetRecognizableGestures(
            GestureSettings.Tap);
        IntroRecognizer.TappedEvent += IntroRecognizer_TappedEvent;

        IntroRecognizer.StartCapturingGestures();
    }

    void Update()
    {
        if (Input.GetButtonDown("NextState"))
        {
            Scene.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        IntroRecognizer.TappedEvent -= IntroRecognizer_TappedEvent;
    }

    private void IntroRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray ray)
    {
        Scene.SetActive(true);
        Destroy(gameObject);
    }

  

}