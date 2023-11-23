using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCollision : MonoBehaviour
{

    public GameObject objCollider;
    public GameObject secretTagCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NestArea")
        {
            secretTagCollider.tag = "SecretFound";
            GameModeManager.instance.secretCurrentForMenu += 1;
            if (GameModeManager.instance.secretCurrentForMenu >= GameModeManager.instance.secretTotalForMenu)
            { GameModeManager.instance.secretCurrentForMenu = GameModeManager.instance.secretTotalForMenu; }
            GameModeManager.instance.InvokeSecretCountForMenu();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NestArea")
        {
            secretTagCollider.tag = "Secret";
            GameModeManager.instance.secretCurrentForMenu -= 1;
            if (GameModeManager.instance.secretCurrentForMenu <= 0)
            { GameModeManager.instance.secretCurrentForMenu = 0; }
            GameModeManager.instance.InvokeSecretCountForMenu();
        }
    }

}
