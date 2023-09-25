using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    public delegate void UiAnim();
    public static UiAnim PauseOn;

    [SerializeField]private PlayerController playerController;
    [SerializeField]private Vector2 mouseVector;
    [SerializeField]private Vector2 inputVector;

    

    [Header("Input Actions")]

    private InputAction abilityMovement;
    private InputAction abilityJump;
    private InputAction abilityFly;
    private InputAction abilityInteract_one;
    private InputAction abilityInteract_two;
    private InputAction abilityInteract_three;
    private InputAction pause;

    [Header("Player Scripts")]
    public PlayerCore core;
    public Ability_Movement moveScript;
    public Ability_Airborne airborneScript;
    public Ability_Interact interactScript;

    public float cursorTimer = 2f;

    private void Awake()
    {
        playerController = new PlayerController();

        //abilityMouse = playerController.UI.Point;
        abilityMovement = playerController.Player.Movement;
        abilityJump = playerController.Player.Jump;
        abilityFly = playerController.Player.Fly;
        abilityInteract_one = playerController.Player.Interact_One;
        abilityInteract_two = playerController.Player.Interact_Two;
        abilityInteract_three = playerController.Player.Interact_Three;
        pause = playerController.Player.Pause;
    }

    private void OnEnable()
    {

        //MOVEMENT
        abilityMovement.Enable();
        //BASIC CONTROLS//enabloidaan input action Airborne Input assetista ja laitetaan komento kun se painetaan => functio JumpInput alempana
        abilityJump.performed += JumpInput;
        abilityJump.canceled += JumpInputCancel;
        abilityFly.performed += FlyInput;
        abilityJump.Enable();
        abilityFly.Enable();
        //INTERACT_ONE//enabloidaan input action Interact_One Input assetista ja laitetaan komento kun se painetaan => functio InteractInputOne alempana
        abilityInteract_one.performed += InteractInputOne;
        abilityInteract_one.canceled += InteractInputCancel;
        abilityInteract_two.canceled += InteractInputCancel;
        abilityInteract_three.canceled += InteractInputCancel;
        abilityInteract_one.Enable();
        //INTERACT_TWO//enabloidaan input action Interact_Two Input assetista ja laitetaan komento kun se painetaan => functio InteractInputTwo alempana
        abilityInteract_two.performed += InteractInputTwo;
        abilityInteract_two.Enable();
        //INTERACT_THREE//enabloidaan input action Interact_Three Input assetista ja laitetaan komento kun se painetaan => functio InteractInputThree alempana
        abilityInteract_three.performed += InteractInputThree;
        abilityInteract_three.Enable();
        //PAUSE
        pause.performed += Pause;
        pause.Enable();
    }



    private void OnDisable()
    {
        //abilityMouse.Disable();
        abilityMovement.Disable();
        abilityJump.Disable();
        abilityFly.Disable();
        abilityInteract_one.Disable();
        abilityInteract_two.Disable();
        abilityInteract_three.Disable();
        pause.Disable();
    }

    private void FixedUpdate()
    {

        //mouseVector = abilityMouse.ReadValue<Vector2>();
        mouseVector = Input.mousePosition;
        GameModeManager.instance.mouseMovementCheck.transform.position = mouseVector;

        if (GameModeManager.instance.mouseMovementCheck.transform.hasChanged)
        {
            GameModeManager.instance.mouseMovementCheck.transform.hasChanged = false;
            Cursor.visible = true;
            cursorTimer = 2f;
        }
        else
        {
            cursorTimer -= Time.deltaTime;
            if(cursorTimer <= 0)
            {
                Cursor.visible = false;
                cursorTimer = 0;
            }
        }

        // inputvector muuttuja alussa lukee inputAction ablilitymovement arvoa Vector 2
        inputVector = abilityMovement.ReadValue<Vector2>();
        // k‰ytet‰‰n t‰t‰ arvoa ja tietoa eri scriptiin functioon movescriptiss‰
        moveScript.Movement(inputVector);
    }

    private void JumpInput(InputAction.CallbackContext obj)
    {
        if(!core.isLanding)
        {
            airborneScript.jumpIsPressed = true;
            airborneScript.JumpAction();
        }

    }
    private void JumpInputCancel(InputAction.CallbackContext obj)
    {
        airborneScript.jumpIsPressed = false;
    }
    private void FlyInput(InputAction.CallbackContext obj)
    {
        airborneScript.FlyAction();
    }

    private void InteractInputOne(InputAction.CallbackContext obj)
    {

        //tieto kulkeutuu napin painautuessa interactscriptiin ja siell‰ olevaan functioon
        interactScript.InteractActionOne();

        if(GameModeManager.instance.levelActive != true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        {
            if(interactScript.collectableObj != null)
            { Destroy(interactScript.collectableObj); }
        }
    }
    private void InteractInputTwo(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa interactscriptiin ja siell‰ olevaan functioon
        interactScript.InteractActionTwo();
    }
    private void InteractInputThree(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa interactscriptiin ja siell‰ olevaan functioon
        interactScript.InteractActionThree();
    }
    private void InteractInputCancel(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa interactscriptiin ja siell‰ olevaan functioon
        interactScript.InteractActionCancel();
    }



    private void Pause(InputAction.CallbackContext obj)
    {
        if (GameModeManager.instance.activeGameMode != GameModeManager.GameMode.cutScene)
        { PauseOn?.Invoke(); }
        else
        { Debug.Log("Do nothing"); }
    }
}

