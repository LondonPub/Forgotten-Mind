using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class yapper : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public float typingSpeed = 0.05f;

    private string[] lines;
    private int index;
    private bool isTyping;

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        lines = dialogue.lines;
        index = 0;
        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (dialogueBox.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void NextLine()
    {
        index++;
        if (index < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueBox.SetActive(false);
        }
    }
}
