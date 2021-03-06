using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class CursorScript : MonoBehaviour
{

    private bool previousMouseState;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private RectTransform cursorTransform;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private RectTransform canvasRectTransform;

    [SerializeField]
    private float cursorSpeed = 1500f;

    [SerializeField]
    private float padding = 35f;

    private Mouse virtualMouse;
    private Mouse currentMouse;
    private Camera mainCamera;

    private string previousControlScheme = "";
    private const string gamepadScheme = "Gamepad";
    private const string mouseScheme = "Keyboard&Mouse";


    private void OnEnable()
    {
        mainCamera = Camera.main;
        currentMouse = Mouse.current;
        //Cursor.visible = false;
        if (virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
        playerInput.onControlsChanged += OnControlsChanged;
    }


    private void OnDisable()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
        playerInput.onControlsChanged -= OnControlsChanged;
        if (virtualMouse != null && virtualMouse.added)
        {
            InputSystem.RemoveDevice(virtualMouse);
        }
    }

    private void UpdateMotion()
    {
        if (virtualMouse == null || Gamepad.current == null)
        {
            return;
        }



        Vector2 deltaValue = Gamepad.current.rightStick.ReadValue();
        deltaValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, padding, Screen.width - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height - padding);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);


        /*
        bool xButtonIsPressed = Gamepad.current.xButton.IsPressed();
        if (previousMouseState != xButtonIsPressed)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, xButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = xButtonIsPressed;
        }
        */

        AnchorCursor(newPosition);

    }


    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);

        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out worldPosition);
        Debug.Log("World Position: " + worldPosition);
        
        cursorTransform.anchoredPosition = anchoredPosition;
    }


    private void OnControlsChanged(PlayerInput input)
    {
        if (playerInput.currentControlScheme == mouseScheme && previousControlScheme != mouseScheme)
        {
            cursorTransform.gameObject.SetActive(false);
            Cursor.visible = true;
            currentMouse.WarpCursorPosition(virtualMouse.position.ReadValue());
            previousControlScheme = mouseScheme;
        }
        else if (playerInput.currentControlScheme == gamepadScheme && previousControlScheme != gamepadScheme)
        {
            cursorTransform.gameObject.SetActive(true);
            Cursor.visible = false;
            InputState.Change(virtualMouse.position, currentMouse.position.ReadValue());
            AnchorCursor(currentMouse.position.ReadValue());
            previousControlScheme = gamepadScheme;
        }

    }



    public Vector3 GetCursorPosition()
    {
        return virtualMouse.position.ReadValue();
    }

    
    private void Update()
    {

        Vector3 forward = cursorTransform.TransformDirection(Vector3.forward) * 200;
        Debug.DrawRay(cursorTransform.position, forward, Color.green);


        

        Debug.Log(mainCamera.WorldToScreenPoint(canvasRectTransform.position));
        if (previousControlScheme != playerInput.currentControlScheme)
        {

            Debug.Log("change up!");
            playerInput.onControlsChanged += OnControlsChanged;
            //OnControlsChanged(playerInput);
        }
        previousControlScheme = playerInput.currentControlScheme;

    }
    




}

