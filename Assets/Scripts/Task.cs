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
        if (IsTaskActive() && Input.GetKeyDown(KeyCode.E))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, (Screen.height / 2)));
            Instantiate(task, new Vector3(position.x, position.y, 0), Quaternion.identity, null);
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

    private bool IsTaskActive()
    {
        return playerClose && !GameObject.FindWithTag("Task");
    }
}

