using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MoveByVJoystick : MonoBehaviour
{
    private const string runParaname = "Run";
    private const string runRightParaname = "RunRight";

    public CharacterController characterController;
    public Animator anim;
    public Joystick joystick;

    public Transform playerTransform;
    public float movingSpeed;

    private void Awake()
    {
        transform.position = new Vector3(0.02f, 1.58f, -2.5f);
    }

    private void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        float hInput = joystick.Horizontal;
        float vInput = joystick.Vertical;

        Vector3 directionOfMovement = new Vector3(hInput, 0f, vInput);

        characterController.SimpleMove(directionOfMovement * movingSpeed);

        if (directionOfMovement.sqrMagnitude < 0.0001f)
        {
            anim.SetBool(runParaname, false);
            anim.SetBool(runRightParaname, false);
            return;
        }

        //hướng của Player (chỉ lấy XZ)
        Vector3 pointerDir = playerTransform.forward;
        pointerDir.y = 0f;
        pointerDir.Normalize();

        //hướng di chuyển (chỉ lấy XZ)
        Vector3 moveDir = directionOfMovement;
        moveDir.y = 0f;
        moveDir.Normalize();

        //góc giữa hướng di chuyển và hướng Player
        float angle = Vector3.Angle(moveDir, pointerDir);

        //Debug.Log($"Angle between moveDir and pointerDir: {angle}");

        bool isRunForward = angle <= 45 || angle >= 135;


        if (isRunForward)
        {
            anim.SetBool(runParaname, true);
            anim.SetBool(runRightParaname, false);
        }
        else
        {
            anim.SetBool(runParaname, false);
            anim.SetBool(runRightParaname, true);
        }
    }
}
