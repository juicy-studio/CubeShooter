using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;

    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    public VirtualJSAim JS;
    public VirtualJSAim JsAim;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        // 추가 아예 변경
        float h = JS.Horizontal();
        float v = JS.Vertical();
        float a = JsAim.Horizontal();
        float b = JsAim.Vertical();
        Move(h, v);
        //Turning();

        JSTurning(a,b);
        //Animating(h, v);
    }
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void JSTurning(float a, float b)
    {
        if (JsAim.isPressed)
        {
            Vector3 position = new Vector3(a, 0f, b);
            position.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(position);
            playerRigidbody.MoveRotation(newRotation);
        }


    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }
    
}
