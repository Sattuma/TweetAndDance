using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASS INCLUDE PLAYER INTERACT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Interact : MonoBehaviour
{
    [Header("Game Level Actions")]
    public PlayerCore core;
    public Transform dropPoint;
    public Transform collectPoint;
    public Transform collectableObj;
    public Transform collectableChildObj;
    public bool canCollect;
    public bool pickedUp;

    [Header("Bonus Level 1  Actions")]
    public float buttonTimer;

        
    [Header("Bonus Level Buttons")]
    public GameObject[] bonusLevelButtons = new GameObject[3];

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable") || collision.gameObject.CompareTag("NestObject"))
        {
            if(collectableObj == null)
            {
                canCollect = true;
                collectableObj = collision.gameObject.transform; // on triggerstay collectable object is collision which player collides
                collectableChildObj = collectableObj.transform.GetChild(0).gameObject.transform;
                collectableObj.GetComponent<CollectableCollision>().HighLightOn();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable") || collision.gameObject.CompareTag("NestObject"))
        {
            if(canCollect)
            {
                collectableObj.GetComponent<CollectableCollision>().HighLightOff();
                canCollect = false;
                collectableObj = null;
                collectableChildObj = null;
            }
        }
    }

    void Start()
    {
        core = GetComponent<PlayerCore>();
        collectableObj = null;
        buttonTimer = 0.2f;
    }

    public void Collect()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel && GameModeManager.instance.levelActive == true)
        {
            if (canCollect)
            {
                collectableObj.GetComponent<CollectableCollision>().HighLightOff();
                StartCoroutine(CollectDelay());
                collectableObj.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
                collectableObj.gameObject.tag = "Untagged";
                collectableChildObj.gameObject.tag = "Untagged";
                core.myAnim.SetTrigger("Pickup");
                collectableObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                collectableObj.gameObject.transform.parent = transform; // if canCollect bool is active when collect button is pressed, player pick that collectable
                collectableObj.transform.position = collectPoint.transform.position;
                canCollect = false;
                pickedUp = true;
            }
            else if (pickedUp)
            {
                collectableObj.gameObject.transform.parent = null; // if collectable is already picked up in Collect button is pressed again, player drops the collectable object
                collectableObj.GetComponentInChildren<CapsuleCollider2D>().enabled = true;
                collectableObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                collectableObj.transform.position = dropPoint.position;
                collectableObj = null;
                pickedUp = false;
            }
        }
    }

    //BONUS LEVEL ABILITTIES BUTTON
    public void InteractActionOne()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive == true)
        {
            bonusLevelButtons[0].GetComponent<Collider2D>().enabled = true;
            bonusLevelButtons[0].GetComponentInParent<Animator>().SetTrigger("Response");
            StartCoroutine(OnePressFalse());
        }
    }
    IEnumerator OnePressFalse()
    {
        yield return new WaitForSeconds(buttonTimer);
        bonusLevelButtons[0].GetComponent<Collider2D>().enabled = false;
    }

    public void InteractActionTwo()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive == true)
        {
            bonusLevelButtons[1].GetComponent<Collider2D>().enabled = true;
            bonusLevelButtons[1].GetComponentInParent<Animator>().SetTrigger("Response");
            StartCoroutine(TwoPressFalse());
        }
    }
    IEnumerator TwoPressFalse()
    {
        yield return new WaitForSeconds(buttonTimer);
        bonusLevelButtons[1].GetComponent<Collider2D>().enabled = false;
    }
    public void InteractActionThree()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive == true)
        {
            bonusLevelButtons[2].GetComponent<Collider2D>().enabled = true;
            bonusLevelButtons[2].GetComponentInParent<Animator>().SetTrigger("Response");
            StartCoroutine(ThreePressFalse());
        }
    }
    IEnumerator ThreePressFalse()
    {
        yield return new WaitForSeconds(buttonTimer);
        bonusLevelButtons[2].GetComponent<Collider2D>().enabled = false;
    }
    public void InteractActionFour()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive == true)
        {
            bonusLevelButtons[3].GetComponent<Collider2D>().enabled = true;
            bonusLevelButtons[3].GetComponentInParent<Animator>().SetTrigger("Response");
            StartCoroutine(FourPressFalse());
        }
    }
    IEnumerator FourPressFalse()
    {
        yield return new WaitForSeconds(buttonTimer);
        bonusLevelButtons[3].GetComponent<Collider2D>().enabled = false;
    }

    public void CancelOne()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive == true)
        { bonusLevelButtons[0].GetComponent<Collider2D>().enabled = false; StopCoroutine(OnePressFalse()); }
    }
    public void CancelTwo()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive == true)
        { bonusLevelButtons[1].GetComponent<Collider2D>().enabled = false; StopCoroutine(TwoPressFalse()); }
    }
    public void CancelThree()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive == true)
        { bonusLevelButtons[2].GetComponent<Collider2D>().enabled = false; StopCoroutine(ThreePressFalse()); }
    }
    public void CancelFour()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive == true)
        { bonusLevelButtons[3].GetComponent<Collider2D>().enabled = false; StopCoroutine(FourPressFalse()); }
    }

    public IEnumerator CollectDelay()
    {
        core.isCollecting = true;
        yield return new WaitForSecondsRealtime(0.3f);
        core.isCollecting = false;
    }
}
