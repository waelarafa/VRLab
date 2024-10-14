using Michsky.MUIP;
using System;
using UnityEngine;

public class InputHandler : IGlobalSingleton<InputHandler>
{
    [SerializeField] private ButtonManager interactionButton;

    private bool _inputEnabled = true;
    private bool _interactionLocked = false;
    private Vector2 _movementInput;
    private Vector2 _mouseInput;

    public event Action InteractionEvent;
    public bool inputEnabled => _inputEnabled;

    public Vector2 mouseInput
    {
        get
        {
            if (!_inputEnabled) return Vector2.zero;

            return _mouseInput;
        }
    }

    public Vector2 movementInput
    {
        get
        {
            if (!_inputEnabled) return Vector2.zero;

            return _movementInput;
        }
    }

    public void InputEnabled(bool enabled)
    {
        _inputEnabled = enabled;
    }

    public void LockUnlockCursor(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void EnterInteractionZone(bool enter)
    {
        interactionButton.gameObject.SetActive(enter);
    }

    public void ShowCursorAndBlockInput()
    {
        _interactionLocked = true;
        LockUnlockCursor(true);
        InputEnabled(false);
    }

    public void HideCursorAndEnableInput()
    {
        _interactionLocked = false;
        LockUnlockCursor(false);
        InputEnabled(true);
    }

    private void Start()
    {
        LockUnlockCursor(false);
    }

    private void Update()
    {
        EscapeInput();

        if (!inputEnabled) return;

        MouseInput();
        InteractionInput();
        MovementInput();
    }

    private void EscapeInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }


    public void Pause()
    {
        GameHUD.instance.PauseClicked();

        if (GameHUD.instance.isPaused)
        {
            Progress.Instance.PauseAudio();
            if (!_interactionLocked)
            {
                if (inputEnabled)
                {
                    LockUnlockCursor(true);
                    InputEnabled(false);
                }
            }
        }
        else
        {
            Progress.Instance.ResumeAudio();
            GameHUD.instance.Resume();
        }
    }

    public void UnlockInput()
    {
        if (!_interactionLocked)
        {
            if (!inputEnabled)
            {
                LockUnlockCursor(false);
                InputEnabled(true);
            }
        }
    }


    private void InteractionInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InteractionEvent?.Invoke();
        }
    }

    private void MovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        _movementInput = new Vector2(x, y);
    }

    private void MouseInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        _mouseInput = new Vector2(x, y);
    }
}