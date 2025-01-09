using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    
    public bool isGrounded = true;
    public float jumpForce = 5f;
    public float forwardSpeed = 10f;  
    public float laneDistance = 3f;    
    public float laneSwitchSpeed = 10f; 
    
    private int currentLane = 0;
    public Transform startPos;
    
    public List<GameObject> carrots;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -20f, 0); 
        Time.timeScale = 1;
    }

    void Update()
    {
        HandleInput();
        MoveForward();
        MoveToLane();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > -1)
        {
            currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 1)
        {
            currentLane++;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }
    }
    void Jump()
    {
        isGrounded = false;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
    }
    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void MoveToLane()
    {
        float targetX = currentLane * laneDistance;
        Vector3 newPosition = new Vector3(Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * laneSwitchSpeed),
            transform.position.y,
            transform.position.z);
        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Block"))
        {
            Time.timeScale = 0;   
            GameManager.Instance.GameOver();
            gameObject.transform.position = startPos.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Carrot"))
        {
            GameManager.Instance.SetScore(5);
            GameManager.Instance.AddCarrot(other.gameObject);
            other.gameObject.SetActive(false);
        }
        
    }
}
