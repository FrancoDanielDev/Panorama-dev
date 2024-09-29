using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public event Action EndConversationEvent;
    public event Action EndedConversationEvent;

    [Range(0f, 0.1f)]
    [SerializeField] private float _textDisplayCooldown;
    [Space]
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _speakersExpression;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private Animator _animator;

    // FIFO (First In, First Out)
    private Queue<string> _sentences;
    private string _currentSentence;

    private string[] _names = new string[0];
    private Sprite[] _expressions = new Sprite[0];
    private int _counter;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        _sentences = new Queue<string>();
        LocalizationManager.instance.TranslationEvent += UpdateText;
    }

    public void StartDialogue(DialogueFile dialogue)
    {
        _animator.SetBool("IsOpen", true);
        AudioManager.instance.Play("Talk");

        _sentences.Clear();

        MenuManager.instance.InteractionsInCooldown(true);

        _counter = 0;

        foreach (DialogueLines sentence in dialogue.secondDialogueLines)
        {
            _sentences.Enqueue(sentence.line);
        }

        _names = new string[_sentences.Count];
        _expressions = new Sprite[_sentences.Count];

        for (int i = 0; i < _sentences.Count; i++)
        {
            _names[i] = dialogue.secondDialogueLines[i].character.Name;
            _expressions[i] = dialogue.secondDialogueLines[i].character
                .GetExpression(dialogue.secondDialogueLines[i].expressions);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            EndedConversationEvent?.Invoke();
            return;
        }

        if (_sentences.Count == 1)
            EndConversationEvent?.Invoke();

        _nameText.text = _names[_counter];
        _speakersExpression.sprite = _expressions[_counter];

        _counter++;
        _currentSentence = _sentences.Dequeue();
        UpdateText();
    }

    private void UpdateText()
    {
        if (_currentSentence == null) return;

        StopAllCoroutines();
        string localizedSentence = LocalizationManager.instance.GetTranslation(_currentSentence);
        StartCoroutine(TypeSentence(localizedSentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;

            yield return new WaitForSeconds(_textDisplayCooldown);
        }
    }

    private void EndDialogue()
    {
        _animator.SetBool("IsOpen", false);
        MenuManager.instance.InteractionsInCooldown(false);
    }

    public void TriggerDialogue(DialogueFile dialogue)
    {
        _dialogueBox.SetActive(true);
        StartDialogue(dialogue);

        if (FindObjectOfType<Player>() != null)
        {
            Player.instance.OnEnabled(false);
            Player.instance.animator.SetBool("Walking", false);
        }
    }
}
