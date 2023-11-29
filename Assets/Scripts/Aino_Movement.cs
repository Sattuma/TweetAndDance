using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aino_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public ParticleSystem landingFX;

    private void Awake()
    {


    }

    private void Start()
    {
        string activeLevelName = SceneManager.GetActiveScene().name.ToString();
        if (activeLevelName == "EndScreen_1")
        {
            WinAnim();
        }
        if(GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Bonus1)
        {
            BonusAnim();
        }
        
        GameModeManager.LevelActiveOn += MoveToPositionLevel;

        GameModeManager.Success += WinAnim;
        GameModeManager.Fail += FailAnim;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        IdleAnim();
        landingFX.Play();
    }

    void MoveToPositionLevel()
    {
        string activeLevelName = SceneManager.GetActiveScene().name.ToString();
        if (activeLevelName != "EndScreen_1")
        {
            IdleAnim();
        }
        rb.velocity = -Vector2.up * 4f + Vector2.right * 3f;
    }

    public void BonusAnim()
    {
        anim.SetTrigger("Bonus");
    }

    public void IdleAnim()
    {
        anim.SetTrigger("Idle");
    }

    public void FailAnim()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        anim.SetTrigger("Fail");
        rb.velocity = Vector2.up * 2f + Vector2.right * 5f;
        Destroy(gameObject, 5f);

    }
    public void WinAnim()
    {
        anim.SetTrigger("Success");
    }
}
