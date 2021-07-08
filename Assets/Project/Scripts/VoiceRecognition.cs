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
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    private void Back()
    {
        transform.Translate(-10f, 0f, 0f);
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
