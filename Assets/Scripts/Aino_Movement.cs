using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aino_Movement : MonoBehaviour
{

    public GameObject startPosLevel2;
    public GameObject endPosLevel2;
    public GameObject startPosLevel3;
    public GameObject endPosLevel3;

    public GameObject startPos2Collider;
    public GameObject endPos2Collider;

    [SerializeField] private Rigidbody2D rb;
    public Animator anim;

    [SerializeField] float transitionSpeed;

    public bool isFailed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("LevelTwo"))
        {
            anim.SetBool("FlyDown", false);
            collision.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
        transform.position = startPosLevel2.transform.position;
        startPos2Collider.SetActive(false);

        GameModeManager.Fail += FailedBool;
        GameModeManager.Fail += FailAnim;

    }

    private void FixedUpdate()
    {

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
