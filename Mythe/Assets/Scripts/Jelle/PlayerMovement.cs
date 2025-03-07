﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool automaticWalk = true;
    Rigidbody playerRB;
    Vector3 jumpForce = new Vector3(0,700,0);
    public float movementSpeed = 6;
    float standartMoveSpeed = 6;
    float impairedMoveSpeed = 2;
    public bool impairedMovement = false;
    float counter,wearOffTIme = 1.5f;

    public GameObject rightButton;
    public GameObject leftButton;
    public GameObject jumpButton;

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        //Automatic/Manual walking.
        if (automaticWalk == false)
        {
            rightButton.SetActive(true);
            leftButton.SetActive(true);

            //if (Input.GetKey(KeyCode.D))
            //{
            //    transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            //}
            //if (Input.GetKey(KeyCode.A))
            //{
            //    transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
            //}
        }
        else
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
        }


        //Impaired Movement
        if (impairedMovement)
        {
            movementSpeed = impairedMoveSpeed;
            counter += Time.deltaTime;
            if (counter > wearOffTIme)
            {
                movementSpeed = standartMoveSpeed;
                counter = 0;
                impairedMovement = false;
            }
        }
    }

    /// <summary>
    /// Checks if grounded.
    /// </summary>
    /// <returns><c>true</c>, if if grounded was checked, <c>false</c> otherwise.</returns>
    bool CheckIfGrounded()
    {
        //Will return True if this object exists above another object.
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, 0.7f);                
    }
    /// <summary>
    /// Moves the right.
    /// </summary>
    public void moveRight()
    {
        transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// Moves the left.
    /// </summary>
    public void moveLeft()
    {
        transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
    }

    /// <summary>
    /// Jump this instance.
    /// </summary>
    public void Jump()
    {
        if (CheckIfGrounded() == true)
        {
            playerRB.AddForce(jumpForce);
        }
    }

    /// <summary>
    /// Ons the collision enter.
    /// </summary>
    /// <param name="other">Other.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Cave")
        {
            automaticWalk = false;
        }
    }
}
