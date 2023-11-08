using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{

    [SerializeField]private PlayerController playerController;
    [SerializeField]private Vector2 cursorSensitivityVector;
    [SerializeField]private Vector2 mouseVector;
    [SerializeField]private Vector2 inputVector;
    [SerializeField] private float moveX;


    [Header("Input Actions")]
    private InputAction abilityMovement;
    private InputAction abilityJump;
    private InputAction abilityFly;
    private InputAction abilityCollect;
    private InputAction abilityInteract_one;
    private InputAction abilityInteract_two;
    private InputAction abilityInteract_three;
    private InputAction abilityInteract_four;
    private InputAction pause;

    [Header("Player Scripts")]
    public PlayerCore core;
    public Ability_Movement moveScript;
    public Ability_Airborne airborneScript;
    public Ability_Interact interactScript;
    public NoteLineScript bonusOne;

    public float cursorTimer = 2f;
    public float cursorSensitivity;

    private void Awake()
    {
        playerController = new PlayerController();

        abilityMovement = playerController.Player.Movement;
        abilityJump = playerController.Player.Jump;
        abilityFly = playerController.Player.Fly;
        abilityCollect = playerController.Player.Collect;
        abilityInteract_one = playerController.Player.Interact_One;
        abilityInteract_two = playerController.Player.Interact_Two;
        abilityInteract_three = playerController.Player.Interact_Three;
        abilityInteract_four = playerController.Player.Interact_Four;
        pause = playerController.Player.Pause;
    }
    void Start()
    {
        CheckBonusLevelControls();

    }
    private void OnEnable()
    {
        //MOVEMENT
        abilityMovement.Enable();
        //BASIC CONTROLS//enabloidaan input action Airborne Input assetista ja laitetaan komento kun se painetaan => functio JumpInput alempana
        abilityJump.performed += JumpInput;
        abilityJump.canceled += JumpInputCancel;
        abilityFly.performed += FlyInput;
        abilityCollect.performed += CollectInput;
        abilityJump.Enable();
        abilityFly.Enable();
        abilityCollect.Enable();
        //INTERACT_ONE//enabloidaan input action Interact_One Input assetista ja laitetaan komento kun se painetaan => functio InteractInputOne alempana
        abilityInteract_one.performed += InteractInputOne;
        abilityInteract_one.canceled += InteractInputCancelOne;
        abilityInteract_one.Enable();
        //INTERACT_TWO//enabloidaan input action Interact_Two Input assetista ja laitetaan komento kun se painetaan => functio InteractInputTwo alempana
        abilityInteract_two.performed += InteractInputTwo;
        abilityInteract_two.canceled += InteractInputCancelTwo;
        abilityInteract_two.Enable();
        //INTERACT_THREE//enabloidaan input action Interact_Three Input assetista ja laitetaan komento kun se painetaan => functio InteractInputThree alempana
        abilityInteract_three.performed += InteractInputThree;
        abilityInteract_three.canceled += InteractInputCancelThree;
        abilityInteract_three.Enable();
        //INTERACT_FOUR//enabloidaan input action Interact_Four Input assetista ja laitetaan komento kun se painetaan => functio InteractInputThree alempana
        abilityInteract_four.performed += InteractInputFour;
        abilityInteract_four.canceled += InteractInputCancelFour;
        abilityInteract_four.Enable();
        //PAUSE
        pause.performed += Pause;
        pause.Enable();
    }



    private void OnDisable()
    {
        abilityMovement.Disable();
        abilityJump.Disable();
        abilityFly.Disable();
        abilityCollect.Disable();
        abilityInteract_one.Disable();
        abilityInteract_two.Disable();
        abilityInteract_three.Disable();
        abilityInteract_four.Disable();
        pause.Disable();
    }

    private void FixedUpdate()
    {

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

        inputVector = abilityMovement.ReadValue<Vector2>();
        moveX = inputVector.x;
        moveScript.Movement(moveX);
    }

    private void JumpInput(InputAction.CallbackContext obj)
    {
        if(!GameModeManager.instance.isPaused)
        {
            if (!core.isLanding)
            {
                airborneScript.jumpIsPressed = true;
                airborneScript.JumpAction();
            }
        }
    }
    private void JumpInputCancel(InputAction.CallbackContext obj)
    {
        airborneScript.jumpIsPressed = false;
    }
    private void FlyInput(InputAction.CallbackContext obj)
    {
        if (!GameModeManager.instance.isPaused)
        { airborneScript.FlyAction();}
    }

    private void CollectInput(InputAction.CallbackContext obj)
    {
        if (!GameModeManager.instance.isPaused)
        { interactScript.Collect(); }
        /*
        if (GameModeManager.instance.levelActive != true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        {
            if (interactScript.collectableObj != null)
            { Destroy(interactScript.collectableObj); }
        }
        */
    }

    private void InteractInputOne(InputAction.CallbackContext obj)
    {
        if(!GameModeManager.instance.isPaused)
        { interactScript.InteractActionOne();}
    }
    private void InteractInputTwo(InputAction.CallbackContext obj)
    {
        if (!GameModeManager.instance.isPaused)
        { interactScript.InteractActionTwo(); }
    }
    private void InteractInputThree(InputAction.CallbackContext obj)
    {
        if (!GameModeManager.instance.isPaused)
        { interactScript.InteractActionThree();}
    }
    private void InteractInputFour(InputAction.CallbackContext obj)
    {
        if (!GameModeManager.instance.isPaused)
        { interactScript.InteractActionFour(); }
    }
    private void InteractInputCancelOne(InputAction.CallbackContext obj)
    {
        interactScript.CancelOne();
    }
    private void InteractInputCancelTwo(InputAction.CallbackContext obj)
    {
        interactScript.CancelTwo();
    }
    private void InteractInputCancelThree(InputAction.CallbackContext obj)
    {
        interactScript.CancelThree();
    }
    private void InteractInputCancelFour(InputAction.CallbackContext obj)
    {
        interactScript.CancelFour();
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        if (GameModeManager.instance.levelActive && !GameModeManager.instance.cannotResumeFromPause)
        { GameModeManager.instance.InvokePause(); }

    }

    public void CheckBonusLevelControls()
    {
        if (GameModeManager.instance.bonusLevelActive)
        {
            if (GameModeManager.instance.levelIndex > 0 && GameModeManager.instance.levelIndex <= 3)
            {
                bonusOne = GameObject.FindGameObjectWithTag("BonusLevelOne").GetComponent<NoteLineScript>();
                interactScript.bonusLevelButtons[0] = bonusOne.activators[0].transform.GetChild(1).gameObject;
                interactScript.bonusLevelButtons[1] = bonusOne.activators[1].transform.GetChild(1).gameObject;
                interactScript.bonusLevelButtons[2] = bonusOne.activators[2].transform.GetChild(1).gameObject;
                interactScript.bonusLevelButtons[3] = bonusOne.activators[3].transform.GetChild(1).gameObject;
            }
            if (GameModeManager.instance.levelIndex > 3 && GameModeManager.instance.levelIndex <= 6)
            {
                //bonuslevel2 controllit
            }
            if (GameModeManager.instance.levelIndex > 6 && GameModeManager.instance.levelIndex <= 9)
            {
                //bonuslevel3 controllit
            }

        }
    }
}

