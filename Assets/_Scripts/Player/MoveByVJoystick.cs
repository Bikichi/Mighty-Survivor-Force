using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MoveByVJoystick : MonoBehaviour
{
    private const string runParaname = "Run";

    public CharacterController characterController;
    public Animator anim;
    public Joystick joystick;

    public float movingSpeed;
    private void Awake()
    {
        transform.position = new Vector3(0.02f, 1.580615f, -2.5f);
    }

    private void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Move();
    }

    public void Move()
    {

        float hInput = joystick.Horizontal;
        float vInput = joystick.Vertical;
        Vector3 directionOfMovement = new Vector3(hInput, 0, vInput);

        characterController.SimpleMove(directionOfMovement * movingSpeed);

        if (directionOfMovement != Vector3.zero)
        {
            anim.SetBool(runParaname, true);
        }
        else
        {
            anim.SetBool(runParaname, false);
        }
    }
}
