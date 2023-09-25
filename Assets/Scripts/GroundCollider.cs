using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public PlayerCore core;

    private void Start()
    {
        core = GetComponentInParent<PlayerCore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && GetComponentInParent<Rigidbody2D>().velocity.y <= 0)
        { StartCoroutine(ResetDelay());}

        // TRIGGERS FOR AUDIO EFFECTS ON LANDING
        if(collision.gameObject.CompareTag("GrassGround"))
        { AudioManager.instance.PlaySoundFX(1);}

        if (collision.gameObject.CompareTag("RockGround"))
        { AudioManager.instance.PlaySoundFX(3);}

        if (collision.gameObject.CompareTag("WoodGround"))
        { AudioManager.instance.PlaySoundFX(4);}
    }

    public IEnumerator ResetDelay()
    {
        core.isLanding = true;
        core.LandingAnimOn();
        yield return new WaitForSecondsRealtime(0.25f);
        core.isLanding = false;
        core.JumpAnimOff();
        core.FlyAnimOff();
    }
}
