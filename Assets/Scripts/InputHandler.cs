using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    [SerializeField]private PlayerController playerController;
    [SerializeField]private Vector2 inputVector;

    [Header("Input Actions")]
    public InputAction abilityMovement;
    public InputAction abilityJump;
    public InputAction abilityInteract_one;
    public InputAction abilityInteract_two;
    public InputAction abilityInteract_three;

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
    }

    private void OnEnable()
    {
        //enabloidaan input action movement
        abilityMovement.Enable();

        //enabloidaan input action Airborne Input assetista ja laitetaan komento kun se painetaan => functio JumpInput alempana
        abilityJump.performed += JumpInput;
        abilityJump.Enable();

        //enabloidaan input action Interact_One Input assetista ja laitetaan komento kun se painetaan => functio InteractInputOne alempana
        abilityInteract_one.performed += InteractInputOne;
        abilityInteract_one.Enable();

        //enabloidaan input action Interact_Two Input assetista ja laitetaan komento kun se painetaan => functio InteractInputTwo alempana
        abilityInteract_two.performed += InteractInputTwo;
        abilityInteract_two.Enable();

        //enabloidaan input action Interact_Three Input assetista ja laitetaan komento kun se painetaan => functio InteractInputThree alempana
        abilityInteract_three.performed += InteractInputThree;
        abilityInteract_three.Enable();
    }

    private void OnDisable()
    {
        abilityMovement.Disable();
        abilityJump.Disable();
        abilityInteract_one.Disable();
        abilityInteract_two.Disable();
        abilityInteract_three.Disable();
    }

    private void FixedUpdate()
    {
        // inputvector muuttuja alussa lukee inputACtion ablilitymovement arvoa Vector 2
        inputVector = abilityMovement.ReadValue<Vector2>();
        // k‰ytet‰‰n t‰t‰ arvoa ja tietoa eri scriptiin functioon movescriptiss‰
        moveScript.Movement(inputVector);
    }

    private void JumpInput(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa airbornescriptiin ja siell‰ olevaan functioon
        airborneScript.JumpAction();
        airborneScript.FlyAction();
    }

    private void InteractInputOne(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa interactscriptiin ja siell‰ olevaan functioon
        interactScript.InteractActionOne();
    }

    private void InteractInputThree(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa interactscriptiin ja siell‰ olevaan functioon
        interactScript.InteractActionTwo();
    }
    private void InteractInputTwo(InputAction.CallbackContext obj)
    {
        //tieto kulkeutuu napin painautuessa interactscriptiin ja siell‰ olevaan functioon
        interactScript.InteractActionThree();
    }

}

