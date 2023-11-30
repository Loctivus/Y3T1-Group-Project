using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public Vector3 spawnPoint;
    public CharacterController charController;
    public float speed = 10f;
    public float gravity = -9.81f;
    bool isGrounded;
    Vector3 velocity;
    public bool moveLock = true;

    public Transform groundDetect;
    float groundDist = 0.2f;
    public LayerMask groundMask;

    public GameObject mesh;
    public Animator anim;

    #endregion

    private void Start()
    {
        GameStateEvents.inst.OnGameInit += LockMovement;
        GameStateEvents.inst.OnGameStart += UnlockMovement;
        GameStateEvents.inst.OnPause += LockMovement;
        GameStateEvents.inst.OnUnPause += UnlockMovement;
        GameStateEvents.inst.OnPlayerDied += LockMovement;
        GameStateEvents.inst.OnGameEnd += LockMovement;
        GameStateEvents.inst.OnEndDay += LockMovement;
        GameStateEvents.inst.OnStartDay += UnlockMovement;
        GameStateEvents.inst.OnStartDay += ResetChar;
    }
    void SetUp()
    {
    }
    void ResetChar()
    {
        charController.enabled = false;
        transform.position = spawnPoint;
        charController.enabled = true;
    }

    void UnlockMovement()
    {
        moveLock = false;
    }
    void LockMovement()
    {
        moveLock = true;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundDetect.position, groundDist, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z);
        //Vector3.Normalize(move);
        

        if (!moveLock)
        {
            charController.Move(move.normalized * speed * Time.deltaTime);
        

            if (!isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
                charController.Move(velocity * Time.deltaTime);
            }
            if(x != 0 || z != 0)
            {
                anim.SetBool("Run", true);
                
            }else
            {
                anim.SetBool("Run", false);
            }
            if (x > 0)
            {
                mesh.transform.localScale = new Vector3(25f, mesh.transform.localScale.y, mesh.transform.localScale.z);
            }
            else
            {
                mesh.transform.localScale = new Vector3(-25f, mesh.transform.localScale.y, mesh.transform.localScale.z);
            }
        }

    }
}
