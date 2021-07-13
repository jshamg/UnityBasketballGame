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
    public PlayerController m_PlayerController;

    private void Start()
    {
        move.Add("forward", Forward);
        move.Add("up", Up);
        move.Add ("down", Down);
        move.Add("back", Back);

        keywordRecognizer = new KeywordRecognizer(move.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
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
        transform.Translate(0f, 10f, 0f);
    }

    private void Down()
    {
        transform.Translate(0f, -10f, 0f);
    }

}
