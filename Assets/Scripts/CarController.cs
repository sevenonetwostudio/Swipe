using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed;

    bool movingLeft = true;

    bool firstInput = true;

    bool gameOver = false;

    public GameObject pickUpEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameStarted)
        {
            Move();
            CheckInput();
        }

        if(transform.position.y <= -2)
        {
            if(!gameOver)
            {
                gameOver = true;

               GameManager.instance.GameOver();
            }
        
        }
    }

    void Move()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void CheckInput()
    {
        //if first input then ignore
        if (firstInput)
        {
            firstInput = false;
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        if (movingLeft)
        { 
            movingLeft = false;
            transform.rotation = Quaternion.Euler(0,90,0);
        }
        else
        {
            movingLeft = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Diamond")
        {
            GameManager.instance.IncrementScore();

            Instantiate(pickUpEffect, other.transform.position, pickUpEffect.transform.rotation);

            other.gameObject.SetActive(false);
        }
    }

}
