using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speedMove = 0.1f;
    public float speedTurn = 3f;
    private float speedMultiplier = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Move(moveVertical);
        Turn(moveHorizontal);
    }

    private void Move(float input)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier = 5f;
        }
        else
        {
            speedMultiplier = 3f;
        }

        Vector3 movement = new Vector3(0f, 0f, input * speedMove * speedMultiplier);
        movement = transform.TransformDirection(movement);
        controller.Move(movement * Time.deltaTime);
    }

    private void Turn(float input)
    {
        if (input != 0)
        {
            float turn = input * speedTurn;
            transform.Rotate(0f, turn, 0f);
        }
    }
}
