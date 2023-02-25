using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;

public class CatCollider : MonoBehaviour
{
    // Y speed where the animation changes to falling
    public float FallSpeedAnimPoint;

    public GameOver endGame;
    // Game Variables
    private bool deadest = false;
    private bool dead = false;
    private bool win = false;
    private float Score;
    private float AirTime;
    private float GrindTime;

    // Option Variables
    public int HatType;
    public int BoardType;
   
    // Animation and Movement
    private bool IsLaunched = false;
    private bool IsFalling = false;
    private bool IsSlam = false;
    private bool IsGrinding = false;
    private bool IsSlopedUp = false;
    private bool IsSlopedDown = false;
    private bool IsShine = false;
    //public AnimationClip gofly;
    //public AnimationClip sleepers;
    //public AnimationClip slide;

    private float ShakeStrength;
    private float vcamtimer;

    
    private SpriteRenderer sprite;
    private Shader original;

    public CinemachineVirtualCamera vcamNormal;
    public CinemachineVirtualCamera vcamZoom;
    public TextMeshProUGUI displayScore;
    public GameObject SplashText;
    public Animator animator;
    public GameObject allnonplayer;
    public GameObject SceneControlObject;

    // particles

    public ParticleSystem pLand;
    public ParticleSystem pGSlam;
    public ParticleSystem pLandS;
    public ParticleSystem pSpark;
    public ParticleSystem pLaunch;

    // sound
    public AudioSource PowerUp;
    public AudioSource Aud;
    public AudioClip land;
    public AudioClip hop;
    public AudioClip roll;
    public AudioClip boing;
    public AudioClip sprkl;
    public AudioClip kill;
    public AudioClip grind;
    public AudioClip scorechange;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        sprite = GetComponent<SpriteRenderer>();
        original = sprite.material.shader;
    }

    //landing 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Grounded") 
        {
            if (pSpark) { pSpark.Stop(); }
            IsLaunched = false;
            IsFalling = false;
            IsGrinding = false;
            IsSlopedDown = false;
            IsSlopedUp = false;
            if (!IsSlam) { if (pLand)
                {
                    pLand.Play();
                } } else { if (pLandS)
                {
                    pLandS.Play();
                } }
            if (!(dead||win)){ Aud.PlayOneShot(land); } // Play land
            if (!(dead || win)) { Aud.clip = roll; Aud.Play(); } // Play roll
            if (AirTime > 200) { Score += (AirTime - 200); }
            AirTime = 0;
            Score += GrindTime;
            GrindTime = 0;
            if (deadest) 
            {
                animator.Play("sleepycat");
                //animator.SetBool("IsSlideFloor", true);
            }
        }
        else if (collision.gameObject.tag == "SlopeDown")
        {
            IsLaunched = false;
            IsFalling = false;
            IsSlopedDown = true;
            if (AirTime > 200) { Score += (AirTime - 200); }
            AirTime = 0;
            Score += GrindTime;
            GrindTime = 0;
        }
        else if (collision.gameObject.tag == "Grind")
        {
            IsLaunched = false;
            IsFalling = false;
            if (pSpark) { pSpark.Play(); }
            if (!(dead || win)) { Aud.clip = grind; Aud.Play(); } 
            if (AirTime > 200) { Score += (AirTime - 200); }
            AirTime = 0;
            IsGrinding = true;
            IsSlopedUp = true;

        }
        
    }
    // Update is called once per frame
    void Update()
    {

        if (!(dead || win))
        {
            // Decrease zoom timer
            if (vcamtimer >= 0) { vcamtimer -= Time.deltaTime; }
            else if (vcamNormal.Priority == 9){ ZoomOut(); }
            

            if (transform.localPosition.y < 0.03 && transform.localPosition.y != 0)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, 0);// Correct to floor
            }

            /////////////////////////////////////////////

            // Airtime
            if (transform.localPosition.y >= 0.1 && (IsFalling || IsLaunched)) { AirTime += 120 * Time.deltaTime; } 
          

            // Grindtime
            if (IsGrinding) { GrindTime += 40 * Time.deltaTime; } 

            if (AirTime > 200)
            {
                
                
                TextPopup("+"+(int)(AirTime - 200) + " Airtime!", 0.5f, 1.5625f);
            }
            if (GrindTime > 1)
            {
                
                TextPopup("+"+(int)(GrindTime) + " Grind!", 0.5f, 1.5625f);
            }
            //////////////////////////////////////////////

            // Falling
            if (GetComponent<Rigidbody2D>().velocity.y < FallSpeedAnimPoint && AirTime > 0)
            {
                
                IsFalling = true;
                IsSlopedDown = false;
                IsSlopedUp = false;
                IsGrinding = false;
                Score += GrindTime;
                GrindTime = 0;
                Aud.clip = null;

            }

            // If waiting for slam launch
            if (IsSlam && !IsFalling)
            {
                if (!GetComponentInChildren<TextBehaviour>()) { TextPopup("Slam!", 0.5f, 1.5625f); }
                IsShine = false;
                
            }

            // Spacebar Input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpAction();
            }

            
            // Update Score Display
            displayScore.text = ((int)Score).ToString("D7");

            // Update Animator
            animator.SetBool("IsLaunched", IsLaunched);
            animator.SetBool("IsFalling", IsFalling);
            animator.SetBool("IsSlam", IsSlam);
            animator.SetBool("IsSlopedUp", IsSlopedUp);
            animator.SetBool("IsSlopedDown", IsSlopedDown);
        }
    }
    private void JumpAction()
    {
        if (!IsFalling && !IsGrinding) // While not falling and not grinding
        {

            if (IsLaunched) //Slam if Launched
            {
                if (IsShine)
                {
                    if (!(dead || win)) { Aud.PlayOneShot(sprkl); PowerUp.Play(); } // Play shine
                    SceneControlObject.GetComponent<SceneControl>().SpeedBoost(0.5f, 1f);
                    Score += 10;
                    TextPopup("+10 Speed Boost!", 0.6f, 1.5625f);
                    if (pGSlam) { pGSlam.Play(); }
                }
                IsLaunched = false;
                IsSlam = true;
                IsFalling = true;
                ZoomIn();
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5.5f), ForceMode2D.Impulse);
                if (pLaunch)
                {
                    pLaunch.Stop();
                } 

            }
            else if (!(IsSlam && IsFalling))  // if not slamfalling
            {
                IsLaunched = true;  // jump with appropriate height
                if (IsSlam)
                {
                    Aud.Stop();
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4.9f), ForceMode2D.Impulse);
                    IsSlam = false;
                        if (pLaunch)
                        {
                            pLaunch.Play();
                        }
                    if (!(dead || win)) { Aud.PlayOneShot(boing); } // Play boing
                    ZoomOut();
                }
                else
                {
                    Aud.Stop();
                    if (!(dead || win)) { Aud.PlayOneShot(hop); } // Play hop
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3.8f), ForceMode2D.Impulse);
                    
                }

            }

        }
        else if (!IsGrinding)  // While falling 
        {
            if (IsLaunched) //Slam if Launched
            {
                if (IsShine)
                {
                    if (!(dead || win)) { Aud.PlayOneShot(sprkl); PowerUp.Play(); } // Play shine
                    SceneControlObject.GetComponent<SceneControl>().SpeedBoost(1f,1f);
                    Score += 10;
                    TextPopup("+10 Speed Boost!", 0.6f, 1.5625f);
                    if (pGSlam)
                    {
                        pGSlam.Play();
                    }
                 }
                IsLaunched = false;
                IsSlam = true;
                IsFalling = true;
                ZoomIn();
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -5.5f), ForceMode2D.Impulse);
                if (pLaunch)
                {
                    pLaunch.Stop();
                }
             }
        }
    }

    void TextPopup(string textToShow, float duration, float yposition)
    {
        TextBehaviour textscript = transform.GetComponentInChildren<TextBehaviour>(includeInactive: true);
            if (textscript != null) // prevents from being null I think???
            {
                textscript.TextUpdate(textToShow, duration, transform.position.y + yposition);
            }
            else
            {
            GameObject SplashTextObject = Instantiate(SplashText, new Vector2(transform.position.x, transform.position.y + yposition), Quaternion.identity, transform);
            TextPopup(textToShow, duration, yposition);
            }

    }
    
    void LaunchZoneEnd() //The point where the launch is unavailable
    {
        IsSlam = false;
        if (vcamNormal.Priority == 9) { ZoomOut(); }

    }
    void LaunchStart() //The point where the launch par of kickflip anim starts
    {
        


    }
    public void ZoomIn()
    {
        vcamNormal.Priority = 9;
        vcamZoom.Priority = 11;
        vcamtimer = 0.7f;
        
    }
    public void ZoomOut()
    {
        vcamZoom.Priority = 9;
        vcamNormal.Priority = 11;
    }
    public void KickFlipComplete()
    {
        Score += 50;
        TextPopup("+50 Kickflip!", 0.7f, 1.5625f);
    }
    public void Kill()
    {
        // you is  kill :(
        if (!(dead || win))
        {
            if (vcamNormal.Priority == 9) { ZoomOut(); }
            dead = true;
            // Fancy animation stuff here
            Aud.Stop();
            Aud.PlayOneShot(kill); // Play kill
            GameObject.Destroy(allnonplayer);
            if (GetComponentInChildren<TextBehaviour>(includeInactive: true)) { GameObject.Destroy(transform.GetComponentInChildren<TextBehaviour>(includeInactive: true).transform.gameObject); } 
            // Hide all other sprites in player here
            var children = transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                if (child.CompareTag("Player")) { Destroy(child.GetComponent<CatCharacter>()); }
                if (!child.CompareTag("CatCollider"))
                {
                    var OtherSprites = child.GetComponents<SpriteRenderer>();
                    foreach (SpriteRenderer Destroyablesprite in OtherSprites)
                    { Destroy(Destroyablesprite); }
                }
            }
            GameObject.Destroy(pGSlam); GameObject.Destroy(pLand); GameObject.Destroy(pLandS); GameObject.Destroy(pLaunch); GameObject.Destroy(pSpark);

            Time.timeScale = 0;
            // SWAP SPRITE OUT FOR WHITE SPRITE HERE
            
            sprite.material.shader = Shader.Find("GUI/Text Shader");
            // SCREEN SHAKE
            StartCoroutine(Shake(0.4f));
            
            StartCoroutine(WaitTimeResume(0.4f));

            //PLAY DEATH ANIMATION
            
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4.9f), ForceMode2D.Impulse);
            endGame.GameOverScreen(Score, false);
            // Back to Main Menu
            //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

    }
    IEnumerator Shake(float strength)
    {
        Vector2 startTransformPos = transform.position;
        ShakeStrength = strength;
        while (Time.timeScale < 1)
        {
            transform.position = startTransformPos + Random.insideUnitCircle * ShakeStrength;
            StartCoroutine(WaitTimeShake(0.03f));
            yield return null;
        }
        transform.position = startTransformPos;
    }
    IEnumerator WaitTimeResume(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        Time.timeScale = 1;
        sprite.material.shader = original;
        animator.SetBool("IsLaunched",false);
        animator.SetBool("IsFalling", false);
        animator.SetBool("IsSlam", false);
        animator.SetBool("IsSlopedUp", false);
        animator.SetBool("IsSlopedDown", false); 
        //animator.SetBool("IsSlideFloor", false);
        //animator.SetBool("IsFly", true);
        animator.Play("flycat");
        deadest = true;
    }
    IEnumerator WaitTimeShake(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        ShakeStrength = 0.6f * ShakeStrength;
    }
   
    public void FinishLine()
    {
        // you is  win :)
        // Fancy animation stuff here
        if (!(dead || win))
        {
            win = true;
            Aud.Stop();
            GameObject.Destroy(allnonplayer);
            if (GetComponentInChildren<TextBehaviour>(includeInactive: true)) { GameObject.Destroy(transform.GetComponentInChildren<TextBehaviour>(includeInactive: true).transform.gameObject); }
            GameObject.Destroy(pGSlam); GameObject.Destroy(pLand); GameObject.Destroy(pLandS); GameObject.Destroy(pLaunch); GameObject.Destroy(pSpark);

            endGame.GameOverScreen(Score, true);
        }

        }

    public bool GetLaunched()
    {
        return IsLaunched;
    }
    public bool GetFalling()
    {
        return IsFalling;
    }
    public bool GetSlam()
    {
        return IsSlam;
    }
    public bool GetSlopedUp()
    {
        return IsSlopedUp;
    }
    public bool GetSlopedDown()
    {
        return IsSlopedDown;
    }
    public void SetShine(bool Value)
    {
        IsShine = Value;
    }
}
