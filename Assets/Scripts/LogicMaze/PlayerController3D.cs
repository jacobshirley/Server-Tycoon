using UnityEngine;
using System.Collections;

public class PlayerController3D : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        float h = 10f * Input.GetAxis("Mouse X");
        transform.Rotate(0, h, 0);
        float v = -5f * Input.GetAxis("Mouse Y");
        Camera.main.transform.Rotate(v, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftTrigger"))
        {
            Debug.Log("Left");

        }
        else if (other.CompareTag("RightTrigger"))
        {
            Debug.Log("Right");
        }
    }
}