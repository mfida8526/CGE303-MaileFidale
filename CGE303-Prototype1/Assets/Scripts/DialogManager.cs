using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//must add this to use TMP_Text
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dialogPanel;

    void OnEnable()
    {
        continueButton.SetActive(false);
        StartCoroutine(Type());
    }

    //coroutine for our typewriter effect
    IEnumerator Type()
    {
        //set the initial text for textbox to empty string
        textbox.text = "";
        foreach (char letter in sentences[index])
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length-1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textbox.text = "";
            dialogPanel.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
