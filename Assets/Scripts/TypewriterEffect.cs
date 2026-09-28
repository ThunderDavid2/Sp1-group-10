using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text _textBox;
    [SerializeField] private string[] texts;
    [SerializeField] private float delayBetweenTexts = 2f;
    private int currentTextIndex = 0;
    private int _currentVisableCharacterIndex;
    private Coroutine _typewriterCoroutine;
    private WaitForSecondsRealtime _simpleDelay;
    private WaitForSecondsRealtime _interpunctuationDelay;
    [SerializeField] private float charactersPerSecond = 2;
    [SerializeField] private float interpunctuationDelay = 0.5f;

    private void Start()
    {
        _simpleDelay = new WaitForSecondsRealtime(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSecondsRealtime(interpunctuationDelay);
        SetText(texts[currentTextIndex]);
    }


    public void SetText(string text)
    {
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);


        _textBox.text = text;
        _textBox.maxVisibleCharacters = 0;
        _currentVisableCharacterIndex = 0;
        _typewriterCoroutine = StartCoroutine(routine: Typewriter());
    }

    private IEnumerator Typewriter()
    {
        _textBox.ForceMeshUpdate();
        TMP_TextInfo textInfo = _textBox.textInfo;

        while(_currentVisableCharacterIndex < textInfo.characterCount)
        {
            char character = textInfo.characterInfo[_currentVisableCharacterIndex].character;

            _textBox.maxVisibleCharacters++;

            if(
                (character == '?' || character == '.' || character == ',' || character == ':' || character == ';' || character == '!' || character == '-'))
            {
                yield return _interpunctuationDelay;
            }
            else
            {
                yield return _simpleDelay;
            }


            _currentVisableCharacterIndex++;

            
        }
        yield return new WaitForSecondsRealtime(delayBetweenTexts);
        
        currentTextIndex++;
        if (currentTextIndex < texts.Length)
        {
            _textBox.text = "";
            SetText(texts[currentTextIndex]);
        }

    }

}
