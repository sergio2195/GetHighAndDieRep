using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{

    [SerializeField]
    private float ghostDelay, ghostDelaySeconds;
    [SerializeField]
    private GameObject ghostInstannce;
    private bool makeGhost;
    private Rigidbody2D playerRB;
    
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
        playerRB = GetComponent<Rigidbody2D>();     
    }

    void Update()
    {
        MakeGhost();   
    }

    void MakeGhost()
    {
        if (((playerRB.velocity.x != 0 || playerRB.velocity.y != 0) && GetComponent<DrugsMechanics>().GetDrugActive()))
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghostInstannce, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale = this.transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
    }
}