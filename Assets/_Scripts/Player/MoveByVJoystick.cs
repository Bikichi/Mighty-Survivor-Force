using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        playerStats = ActivePlayerManager.Instance.CurrentPlayerInstance.GetComponent<PlayerStats>();

        movingSpeed = playerStats.baseMoveSpeed;
        playerStats.onMoveSpeedChanged += UpdateMovingSpeed;

        characterController = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();

        anim = GetComponentInChildren<Animator>();
        joystick = FindObjectOfType<Joystick>();
    }

    private void OnDestroy()
    {
        if (playerStats != null)
            playerStats.onMoveSpeedChanged -= UpdateMovingSpeed;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (joystick == null || characterController == null || playerTransform == null)
        {
            Debug.LogWarning($"Move skipped: joystick={joystick}, characterController={characterController}, playerTransform={playerTransform}");
            return;
        }

        if (anim == null)
        {
            Debug.LogWarning("Animator is null!");
            return;
        }

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

        Vector3 pointerDir = playerTransform.forward;
        pointerDir.y = 0f;
        pointerDir.Normalize();

        Vector3 moveDir = directionOfMovement;
        moveDir.y = 0f;
        moveDir.Normalize();

        float angle = Vector3.Angle(moveDir, pointerDir);
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


    private void UpdateMovingSpeed()
    {
        if (playerStats != null)
            movingSpeed = playerStats.baseMoveSpeed;
    }
}
