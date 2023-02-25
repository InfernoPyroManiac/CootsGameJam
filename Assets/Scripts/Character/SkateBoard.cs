using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateBoard : MonoBehaviour
{

    // Movement and Animation
    private bool IsLaunched = false;
    private bool IsFalling = false;
    private bool IsSlam = false;
    private bool IsSlopedUp = false;
    private bool IsSlopedDown = false;

    public Animator boardanimator;

    // Correct my annoying mistake with the idle sprites being a couple pixels off
    private Vector2 CorrectIdle = new Vector2((float)-0.125, (float)-0.75);
    private Vector2 CorrectNormal = new Vector2((float)-0.0625, (float)-0.875);

    // Start is called before the first frame update
    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {
        IsLaunched = transform.parent.gameObject.GetComponent<CatCollider>().GetLaunched();
        IsFalling = transform.parent.gameObject.GetComponent<CatCollider>().GetFalling();
        IsSlam = transform.parent.gameObject.GetComponent<CatCollider>().GetSlam();
        IsSlopedUp = transform.parent.gameObject.GetComponent<CatCollider>().GetSlopedUp();
        IsSlopedDown = transform.parent.gameObject.GetComponent<CatCollider>().GetSlopedDown();


        //  Corrects my dumb mistake with idle anims; benign
       if (IsLaunched || IsFalling || IsSlopedUp || IsSlopedDown) { transform.localPosition = CorrectNormal; }
       else { transform.localPosition = CorrectIdle; }

        boardanimator.SetBool("IsKickBoard", (IsLaunched && !IsFalling));
        boardanimator.SetBool("IsUpBoard", IsSlopedUp);
        boardanimator.SetBool("IsUpBoard", IsSlopedUp);
    }

    void Shine()
    {
        transform.parent.gameObject.GetComponent<CatCollider>().SetShine(true);
    }
    void ShineEnd()
    {
        transform.parent.gameObject.GetComponent<CatCollider>().SetShine(false);
    }
    void KickFlipComplete()
    {
        transform.parent.gameObject.GetComponent<CatCollider>().KickFlipComplete();
    }
   


}
