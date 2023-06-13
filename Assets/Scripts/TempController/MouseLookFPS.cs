using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class MouseLookFPS : NetworkBehaviour
{
    public float mouseEnsitivity = 0.08f;
    public Transform playerBody;

    private float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOwned) return;
        Mouse mouse = Mouse.current;
        float mouseX = mouse.delta.x.value * mouseEnsitivity * Time.deltaTime;
        float mouseY = mouse.delta.y.value * mouseEnsitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
