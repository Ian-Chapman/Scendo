using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private Rigidbody2D rigidbody = null;

    public static bool isMoving = false;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        PlayerInput();
    }

    void PlayerInput()
    {
        if (isMoving == true)
        {
            rigidbody.velocity = new Vector2(0, 0);
        }

        if ((isMoving == false) && (canMove == true))
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            rigidbody.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
            //isMoving = true;
        }
    }


}