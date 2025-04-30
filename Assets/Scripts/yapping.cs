using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yapping : MonoBehaviour
{
    public Dialogue dialogue;
    private bool playerInRange;
    private yapper dialogueManager; // Reference to yapper script
    
    void Start()
    {
        dialogueManager = FindObjectOfType<yapper>(); // Find yapper in the scene
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogueManager.StartDialogue(dialogue); // Trigger dialogue
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
