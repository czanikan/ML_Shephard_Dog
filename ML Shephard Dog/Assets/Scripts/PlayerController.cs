using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float rotSpeed = 10;
    CharacterController characterController;
    Animator animator;
    private GameManagement gameManagerScript;
    private AudioSource barkSFX;

    private void Start()
    {
        barkSFX = GetComponent<AudioSource>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagement>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        StartCoroutine(DogBark());
    }

    void Update()
    {
        if(!gameManagerScript.gameOver)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            transform.Rotate(Vector3.up * h * rotSpeed * Time.deltaTime);

            Vector3 moveDirection = transform.TransformDirection(Vector3.forward) * v * moveSpeed;
            characterController.SimpleMove(moveDirection);

            if (v != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        } 
    }

    IEnumerator DogBark()
    {
        yield return new WaitForSeconds(Random.Range(1, 10));
        barkSFX.pitch = Random.Range(0.90f, 1.1f);
        barkSFX.Play();
        StartCoroutine(DogBark());
    }
}
