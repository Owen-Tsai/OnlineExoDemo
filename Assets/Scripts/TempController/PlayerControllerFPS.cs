using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEngine.Rendering.PostProcessing;

public class PlayerControllerFPS : NetworkBehaviour
{
    public float speedFactor = 0.05f;
    public float mouseEnsitivity = 0.08f;
    public Transform _camera;

    private float xRotation = 0f;

    public Animator animator;
    NetworkAnimator networkAnimator;

    Keyboard kb;
    Mouse mouse;

    bool isControlLocked = false;
    private bool cameraMoved = false;

    public void LockControl()
    {
        isControlLocked = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        var volume = _camera.GetComponent<PostProcessVolume>();
        volume.profile.GetSetting<Blur>().enabled.value = true;
    }

    public void UnlockControl()
    {
        isControlLocked = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        var volume = _camera.GetComponent<PostProcessVolume>();
        volume.profile.GetSetting<Blur>().enabled.value = false;
    }

    private bool IsWaving()
    {
        return (animator.GetCurrentAnimatorStateInfo(0).length >
           animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
           && animator.GetCurrentAnimatorStateInfo(0).IsName("Waving");
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        kb = Keyboard.current;
        mouse = Mouse.current;
        networkAnimator = GetComponent<NetworkAnimator>();
        UnlockControl();
    }

    public override void OnStartAuthority()
    {
        _camera.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOwned) return;
        if (isControlLocked) return;
        if (IsWaving())
        {
            if (!cameraMoved)
            {
                _camera.localPosition = new Vector3(0, 1.7f, -1.034f);
                _camera.localRotation = Quaternion.Euler(Vector3.zero);
                cameraMoved = true;
            }
        }
        else
        {
            if (cameraMoved)
            {
                _camera.localPosition = new Vector3(0, 1.7f, 0.134f);
                cameraMoved = false;
            }
            Look();
            Move();
            Waving();

            if (mouse.rightButton.wasPressedThisFrame)
            {
                SetCameraZoom(40, 0);
            }
            if (mouse.rightButton.wasReleasedThisFrame)
            {
                SetCameraZoom(60, 0);
            }
        }
    }

    private void Move()
    {
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
        // controller.Move(speedFactor * Time.deltaTime * movement);
        transform.position += movement * speedFactor * Time.deltaTime;

        // set animation parameters
        animator.SetFloat("Vertical", z * speedFactor, 0.1f, Time.deltaTime);
        animator.SetFloat("Horizontal", x * speedFactor, 0.1f, Time.deltaTime);

        animator.SetFloat("WalkSpeed", 2);
    }

    private void Look()
    {
        float mouseX = mouse.delta.x.value * mouseEnsitivity * Time.deltaTime;
        float mouseY = mouse.delta.y.value * mouseEnsitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        _camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void Waving()
    {
        if (kb.spaceKey.wasPressedThisFrame)
        {
            animator.SetTrigger("Waving");
            networkAnimator.SetTrigger("Waving");
        }
    }

    private void SetCameraZoom(float fov, float t)
    {
        if (t > 1f) return;
        Camera c = _camera.GetComponent<Camera>();
        float currentFov = c.fieldOfView;

        c.fieldOfView = Mathf.Lerp(currentFov, fov, t);
        t += 0.1f;

        SetCameraZoom(fov, t);
    }
}
