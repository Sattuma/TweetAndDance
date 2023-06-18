using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    public delegate void UiAnim();
    public static UiAnim InfoBoxAnim;
    public static UiAnim PauseOn;


    [SerializeField]private PlayerController playerController;
    [SerializeField]private Vector2 inputVector;

    public PlayerCore core;

    [Header("Input Actions")]
    public InputAction abilityMovement;
    public InputAction abilityJump;
    public InputAction abilityInteract_one;
    public InputAction abilityInteract_two;
    public InputAction abilityInteract_three;
    public InputAction pause;

    [Header("Ability Scripts")]
    public Ability_Movement moveScript;
    public Ability_Airborne airborneScript;
    public Ability_Interact interactScript;


    private void Awake()
    {
        playerController = new PlayerController();

        abilityMovement = playerController.Player.Movement;
        abilityJump = playerController.Player.Jump;
        abilityInteract_one = playerController.Player.Interact_One;
        abilityInteract_two = playerController.Player.Interact_Two;
        abilityInteract_three = playerController.Player.Interact_Three;
        pause = playerController.Player.Pause;
    }

    private void OnEnable()
    {

        abilityMovement.Enable();

        //enabloidaan input action Airborne Input assetista ja laitetaan komento kun se painetaan => functio JumpInput alempana
        abilityJump.performed += JumpInput;
        abilityJump.Enable();

        //enabloidaan input action Interact_One Input assetista ja laitetaan komento kun se painetaan => functio InteractInputOne alempana
        abilityInteract_one.performed += InteractInputOne;
        abilityInteract_one.canceled += InteractInputCancel;
        abilityInteract_two.canceled += InteractInputCancel;
        abilityInteract_three.canceled += InteractInputCancel;
        abilityInteract_one.Enable();

        //enabloidaan input action Interact_Two Input assetista ja laitetaan komento kun se painetaan => functio InteractInputTwo alempana
        abilityInteract_two.performed += InteractInputTwo;
        abilityInteract_two.Enable();

        //enabloidaan input action Interact_Three Input assetista ja laitetaan komento kun se painetaan => functio InteractInputThree alempana
        abilityInteract_three.performed += InteractInputThree;
        abilityInteract_three.Enable();

        pause.performed += Pause;
        pause.Enable();
    }



    private void OnDisable()
    {
        abilityMovement.Disable();
        abilityJump.Disable();
        abilityInteract_one.Disable();
        abilityInteract_two.Disable();
        abilityInteract_three.Disable();
        pause.Disable();
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        if(GameModeManager.instance.activeGameMode != GameModeManager.GameMode.cutScene1)
        {
            PauseOn?.Invoke();
        }
        else
        {
            Debug.Log("Do nothing");
        }

    }

    private void FixedUpdate()
    {
        // inputvector muuttuja alussa lukee inputAction ablilitymovement arvoa Vector 2
        inputVector = abilityMovement.ReadValue<Vector2>();
        // k‰ytet‰‰n t‰t‰ arvoa ja tietoa eri scriptiin functioon movescriptiss‰
        moveScript.Movement(inputVector);
    }

    private void JumpInput(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa airbornescriptiin ja siell‰ olevaan functioon
        if (GameModeManager.instance.levelActive != true)
        {
            if(GameModeManager.instance.activeGameMode == GameModeManager.GameMode.cutScene1)
            {
                GameObject.Find("TextBox1").GetComponent<Animator>().SetTrigger("Fade");
                GameModeManager.instance.levelActive = true;
                GameModeManager.instance.Level1Active();
                InfoBoxAnim?.Invoke();
            }
            if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.cutScene2)
            {
                if(GameModeManager.instance.level2Retry != true) { moveScript.onMove = true; }
                moveScript.level2Pos.SetActive(true);
                GameObject.Find("TextBox2").GetComponent<Animator>().SetTrigger("Fade");
                GameModeManager.instance.Level2Active();
                GameModeManager.instance.InvokeLevel1End();
                InfoBoxAnim?.Invoke();
            }
            if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.cutScene3)
            {
                //GameObject.Find("TextBox3").GetComponent<Animator>().SetTrigger("Fade");
                GameModeManager.instance.levelActive = true;
                GameModeManager.instance.Level3Active();
                InfoBoxAnim?.Invoke();
            }
        }
        else
        {
            airborneScript.JumpAction();
            airborneScript.FlyAction();
        }
    }

    private void InteractInputOne(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa interactscriptiin ja siell‰ olevaan functioon
        interactScript.InteractActionOne();

        if(GameModeManager.instance.levelActive != true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            if(interactScript.collectableObj != null)
            {
                Destroy(interactScript.collectableObj);
            }
        }
    }
    private void InteractInputCancel(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa interactscriptiin ja siell‰ olevaan functioon
        interactScript.InteractActionCancel();
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



}

