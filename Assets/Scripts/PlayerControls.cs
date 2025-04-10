using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControls : MonoBehaviour
{
    private Vector2 _input;
    public UnityEvent<Vector2> onInput = new UnityEvent<Vector2>();
    private ChatMovement chatMovement;
    
    void Start()
    {
        chatMovement = GetComponent<ChatMovement>(); // Récupère le script ChatMovement sur le même GameObject
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        _input = new Vector2(inputX, inputY).normalized;

        // Invoke event to send input
        onInput.Invoke(_input);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            Debug.Log("Press Space");
            chatMovement.Jump();
        }

    }
}