using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining;
    
    
   
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if space key is pressed down
       if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }
    // Fixed update is called once per physics update
    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);
        // Move the player left and right
        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        

        if (jumpKeyWasPressed)
        {
          float jumpPower = 7f;
          if(superJumpsRemaining > 0)
          {
            jumpPower *= 2f;
            superJumpsRemaining--;
          }  
          rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
          jumpKeyWasPressed = false;
        }
    }
    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Coin")
        {
            Destroy(Col.gameObject);
            superJumpsRemaining++;
        }
        
    }
    
    

}
