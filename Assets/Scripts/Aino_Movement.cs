using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aino_Movement : MonoBehaviour
{

    public GameObject startPosLevel2;
    public GameObject endPosLevel2;
    public GameObject startPosLevel3;
    public GameObject endPosLevel3;

    public GameObject endPos2Collider;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [SerializeField] float transitionSpeed;

    public bool isFailed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("LevelTwo"))
        {
            anim.SetBool("FlyDown", false);
            GameModeManager.instance.ainoOnMove = false;
            collision.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
        transform.position = startPosLevel2.transform.position;

        GameModeManager.Fail += FailedBool;
        GameModeManager.Fail += FailAnim;

    }

    private void FixedUpdate()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1 || GameModeManager.instance.activeGameMode == GameModeManager.GameMode.cutScene1)
        {
            transform.position = startPosLevel2.transform.position;
        }

        if (GameModeManager.instance.ainoOnMove && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            MoveEndPos2();
        }
        else if (isFailed && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            MoveStartPos2();
        }
    }

    public void MoveStartPos2()
    {
        transform.position = Vector3.Lerp(transform.position, startPosLevel2.transform.position, .6f * Time.deltaTime);
    }

    public void MoveEndPos2()
    {
        endPos2Collider.SetActive(true);
        transform.position = Vector3.Lerp(transform.position, endPosLevel2.transform.position, transitionSpeed * Time.deltaTime);
        anim.SetBool("FlyDown", true);
    }

    public void MoveStartPos3()
    {

    }

    public void MoveEndPos3()
    {

    }

    public void FailedBool()
    {
        isFailed = true;
    }

    public void FailAnim()
    {
        anim.SetTrigger("Fail");
    }
    public void WinAnim()
    {
        anim.SetTrigger("Win");
    }

}
