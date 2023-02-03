using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using TMPro;

[Serializable]
public class DialogueElement
{
    [Multiline]
    public string DialogueLine;
}
public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Look")]
    public DialogueBox DialogueBoxPrefab;
    public Color TextBackgroundColor = Color.black;
    public Color TextColor = Color.white;
    public TMP_FontAsset textfontPro;
    public Font TextFont;
    public int TextSize = 40;
    public TextAnchor Alignment = TextAnchor.MiddleCenter;

    [Header("Dialogue Speed (in seconds)")]
    public float FadeDuration = 0.2f;
    public float TransitionTime = 0.2f;
    public float MessageDuration = 3f;


    [Header("Dialogue Position")]
    public Transform dialoguePosition;

    [Header("Dialogue Variable")]
    public int _currentIndex = 0;
    public int _maxDialogueParty;
    public bool isNextDialogue;


    public DialogueElement[] Dialogue;
   // public DialogueElement[] Dialogue1;
    public bool ButtonHandled;

    /// private variables
    public DialogueBox _dialogueBox;

    public bool isFinalDialogue;
    public Collider2D boxDialogue;



    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.GetComponent<PlayerController>().target = this.gameObject;
    //        collision.GetComponent<PlayerController>().SetDialogue();
    //_dialogueBox = Instantiate(DialogueBoxPrefab);
    //_dialogueBox.transform.position = dialoguePosition.position;
    //_dialogueBox.ChangeColor(TextBackgroundColor, TextColor);
    //_dialogueBox.DialogueText.text = Dialogue[_currentIndex].DialogueLine;

    //if (TextFont != null)
    //{
    //    _dialogueBox.DialogueText.font = TextFont;
    //}
    //if (TextSize != 0)
    //{
    //    _dialogueBox.DialogueText.fontSize = TextSize;
    //}
    //_dialogueBox.DialogueText.alignment = Alignment;
    //  }
    // StartCoroutine(PlayNextDialogue());
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {


    //    }
    //}

    private void Start()
    {
        SetCreateDialogue();
        StartCoroutine(AutoNextDialogue());
    }

    protected virtual IEnumerator AutoNextDialogue()
    {
        // we wait for the duration of the message
        yield return new WaitForSeconds( MessageDuration);
        StartCoroutine(PlayNextDialogue());
    }

    public void SetCreateDialogue()
    {
        //Time.timeScale = 0;
        _dialogueBox = Instantiate(DialogueBoxPrefab);

        _dialogueBox.transform.position = dialoguePosition.position;
        _dialogueBox.ChangeColor(TextBackgroundColor, TextColor);
        _dialogueBox.DialogueText1.text = Dialogue[_currentIndex].DialogueLine;
        if (textfontPro != null)
        {
            _dialogueBox.DialogueText1.font = textfontPro;
        }
        if (TextSize != 0)
        {
            _dialogueBox.DialogueText1.fontSize = TextSize;
        }
        _dialogueBox.DialogueText.alignment = Alignment;
     
    }
   

    public IEnumerator PlayNextDialogue()
    {
       
       if (_dialogueBox == null)
        {
            yield break;
        }

        //VERIFICAR DEPOIS COM TIME.TIMESCALE, POIS NÃO FUNCIONA CORRETAMENTE

        //usado para dividir o fade da conversa em 2 dialogos por partes
        //if (_currentIndex != 0  && _currentIndex != 2)
        //{
        //    print("entrou aqui");
        //    _dialogueBox.FadeOut(FadeDuration);
        //    yield return new WaitForSeconds(TransitionTime);
        //}

        //if (_currentIndex >= Dialogue.Length)
        //{
        //    _currentIndex = 0;
        //    Destroy(_dialogueBox.gameObject);
        //    Time.timeScale = 1;
        //    yield break;
        //}

        //usado para dividir o a conversa em 2 dialogos por partes
        if (_currentIndex >= _maxDialogueParty)
        {
            if (isNextDialogue)
            {
                _currentIndex = 2;
                _maxDialogueParty = 4;
            }
            else 
            {
                _currentIndex = 0;
                _maxDialogueParty = 2;

            }
            Destroy(_dialogueBox.gameObject);
            Time.timeScale = 1;
       
            if (isFinalDialogue)
            {
                Destroy(this.gameObject);
            }
            
            yield break;
        }

        if (_dialogueBox.DialogueText != null)
        {
       
            _dialogueBox.FadeIn(FadeDuration);
        
            _dialogueBox.DialogueText.text = Dialogue[_currentIndex].DialogueLine;
        }
    
        _currentIndex++;

        if (!ButtonHandled)
        {
            StartCoroutine(AutoNextDialogue());
        }

    }
   
}
