using UnityEngine;
using UnityEngine.Events;

public class PlayerControls : MonoBehaviour
{
    private Vector2 _input;
    public UnityEvent<Vector2> onInput = new UnityEvent<Vector2>();

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        _input = new Vector2(inputX, inputY).normalized;

        // Invoke event to send input
        onInput.Invoke(_input);
    }
}