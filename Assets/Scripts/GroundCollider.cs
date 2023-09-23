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
        {
            Debug.Log("osun maahan");
            StartCoroutine(ResetDelay());
        }
    }

    public IEnumerator ResetDelay()
    {
        core.isLanding = true;
        core.LandingAnimOn();
        AudioManager.instance.PlaySoundFX(1);
        yield return new WaitForSecondsRealtime(0.25f);
        core.isLanding = false;
        core.JumpAnimOff();
        core.FlyAnimOff();
    }
}
