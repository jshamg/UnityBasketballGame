using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour 
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> move = new Dictionary<string, Action>();
    public float rotationSensitivity;
    public PlayerController m_PlayerController;
    public Player m_Player;
    private GameObject m_Cam;
    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;

    private void Start()
    {
        if (move.Count == 0)
        {
            move.Add("forward", Forward);
            move.Add("up", Up);
            move.Add("down", Down);
            move.Add("back", Back);
            move.Add("left", Left);
            move.Add("right", Right);
            move.Add("throw", Throw);
        }

        keywordRecognizer = new KeywordRecognizer(move.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        m_Cam = m_Player.playerCamera;
        m_CharacterTargetRot = m_PlayerController.transform.localRotation;
        m_CameraTargetRot = m_Cam.transform.localRotation;
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        move[speech.text].Invoke();
    }

    private void Forward()
    {
        Vector2 input = new Vector2(0, 1);
        m_PlayerController.move(input);
    }

    private void Back()
    {
        Vector2 input = new Vector2(0, -1);
        m_PlayerController.move(input);
    }

    private void Up()
    {
        m_CharacterTargetRot *= Quaternion.Euler(0f, 0f, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-(rotationSensitivity), 0f, 0f);

        m_PlayerController.transform.localRotation = m_CharacterTargetRot;
        m_Cam.transform.localRotation = m_CameraTargetRot;
    }

    private void Down()
    {
        m_CharacterTargetRot *= Quaternion.Euler(0f, 0f, 0f);
        m_CameraTargetRot *= Quaternion.Euler(rotationSensitivity, 0f, 0f);

        m_PlayerController.transform.localRotation = m_CharacterTargetRot;
        m_Cam.transform.localRotation = m_CameraTargetRot;
    }

    private void Left()
    {
        m_CharacterTargetRot *= Quaternion.Euler(0f, -(rotationSensitivity), 0f);
        m_CameraTargetRot *= Quaternion.Euler(0f, 0f, 0f);

        m_PlayerController.transform.localRotation = m_CharacterTargetRot;
        m_Cam.transform.localRotation = m_CameraTargetRot;
    }

    private void Right()
    {
        m_CharacterTargetRot *= Quaternion.Euler(0f, rotationSensitivity, 0f);
        m_CameraTargetRot *= Quaternion.Euler(0f, 0f, 0f);

        m_PlayerController.transform.localRotation = m_CharacterTargetRot;
        m_Cam.transform.localRotation = m_CameraTargetRot;
    }

    private void Throw()
    {
        m_Player.Throw();
    }
}
