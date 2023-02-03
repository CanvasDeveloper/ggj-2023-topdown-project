using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using MoreMountains.Tools;

public class DialogueBox : MonoBehaviour
    {
        [Header("Dialogue Box")]
        /// the text panel background
        [Tooltip("the text panel background")]
        public CanvasGroup TextPanelCanvasGroup;
        /// the text to display
        [Tooltip("the text to display")]
        public Text DialogueText;
        public TextMeshProUGUI DialogueText1;
        /// the Button A prompt
        [Tooltip("the Button A prompt")]
        public CanvasGroup Prompt;
        /// the list of images to colorize
        [Tooltip("the list of images to colorize")]
        public List<Image> ColorImages;

        protected Color _backgroundColor;
        protected Color _textColor;


        public virtual void ChangeText(string newText)
        {
        DialogueText1.text = newText;
        }

       
        public virtual void ButtonActive(bool state)
        {
            Prompt.gameObject.SetActive(state);
        }

     
        public virtual void ChangeColor(Color backgroundColor, Color textColor)
        {
            _backgroundColor = backgroundColor;
            _textColor = textColor;
            
            foreach(Image image in ColorImages)
            {
                image.color = _backgroundColor;
            }
        DialogueText1.color = _textColor;
        }

       
        public virtual void FadeIn(float duration)
        {
        
        if (TextPanelCanvasGroup != null)
            {
                StartCoroutine(MMFade.FadeCanvasGroup(TextPanelCanvasGroup, duration, 1f));
            }
            if (DialogueText1 != null)
            {
                StartCoroutine(MMFade.FadeText(DialogueText1, duration, _textColor));
            }
            if (Prompt != null)
            {
                StartCoroutine(MMFade.FadeCanvasGroup(Prompt, duration, 1f));
            }
        }

       
        public virtual void FadeOut(float duration)
        {

     
            Color newBackgroundColor = new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, 0);
            Color newTextColor = new Color(_textColor.r, _textColor.g, _textColor.b, 0);

            StartCoroutine(MMFade.FadeCanvasGroup(TextPanelCanvasGroup, duration, 0f));
            StartCoroutine(MMFade.FadeText(DialogueText1, duration, newTextColor));
            StartCoroutine(MMFade.FadeCanvasGroup(Prompt, duration, 0f));
        }
    }
