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
            move.Add("go forward", Forward);
            move.Add("look up", Up);
            move.Add("look down", Down);
            move.Add("go back", Back);
            move.Add("look left", Left);
            move.Add("look right", Right);
            move.Add("throw", Throw);
            move.Add("go left", WalkLeft);
            move.Add("go right", WalkRight);
            move.Add("switch", Switch);
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

    private void Switch()
    {
        if (m_PlayerController.m_UseVoiceToRotate)
        {
            m_PlayerController.m_UseVoiceToRotate = false;
        } else
        {
            m_PlayerController.m_UseVoiceToRotate = true;
        }
    }

    private void WalkLeft()
    {
        Vector2 input = new Vector2(-1, 0);
        m_PlayerController.move(input);
    }

    private void WalkRight()
    {
        Vector2 input = new Vector2(1, 0);
        m_PlayerController.move(input);
    }

    private void Forward()
    {
        Vector2 input = new Vector2(0, 2);
        m_PlayerController.move(input);
    }

    private void Back()
    {
        Vector2 input = new Vector2(0, -2);
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
