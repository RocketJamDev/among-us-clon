using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{

    public GameObject task;
    bool playerClose;

    // Update is called once per frame
    void Update()
    {
        if (isTaskActive() && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(task);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose = false;
        }
    }

    private bool isTaskActive()
    {
        return playerClose && !GameObject.FindWithTag("Task");
    }
}

