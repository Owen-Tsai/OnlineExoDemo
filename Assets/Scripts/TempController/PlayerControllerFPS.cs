using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerControllerFPS : NetworkBehaviour
{
    public CharacterController controller;
    public float speedFactor = 0.05f;
    public float mouseEnsitivity = 0.08f;
    public Transform _camera;

    private float xRotation = 0f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void OnStartAuthority()
    {
        _camera.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOwned) return;
        Look();
        Move();
    }

    private void Move()
    {
        Keyboard kb = Keyboard.current;
        float x = 0, z = 0;

        if (kb.wKey.isPressed)
        {
            z += 1f;
        }
        if (kb.sKey.isPressed)
        {
            z -= 1f;
        }
        if (kb.aKey.isPressed)
        {
            x -= 1f;
        }
        if (kb.dKey.isPressed)
        {
            x += 1f;
        }

        Vector3 movement = transform.right * x + transform.forward * z;
        controller.Move(speedFactor * Time.deltaTime * movement);
    }

    private void Look()
    {
        Mouse mouse = Mouse.current;
        float mouseX = mouse.delta.x.value * mouseEnsitivity * Time.deltaTime;
        float mouseY = mouse.delta.y.value * mouseEnsitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        _camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
