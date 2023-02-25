using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI displayScore;
    public TextMeshProUGUI displayChat;
    public TextMeshProUGUI displayWin;

    private string hold;
    private string Chat1;
    private string Chat2;
    private string Chat3;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

        Chat1 = "<color=red>Slime enjoyer</color> You can do it coots!\n<color=yellow>LudwigFan0651</color> Get back up coots!\n<color=blue>clayboiparti</color>PogUcoots!\n<color=yellow>YardEnthusiast12345</color>look at the controls again!\n<color=red> epicgamer19 </color> You got this\n<color=red>dotdotdotdot</color> Put the L in Ludwig\n<color=blue>AllTimeFella</color> I bet on coots\n<color=yellow>Brunjo</color> I believe in you coots\n<color=red>LuddersWiggum</color> DON’T GIVE UP COOTS!\n<color=blue>CootsFan66691</color>Permission to motivate?\n";
        Chat2 = "<color=red>CertifiedYardBoi</color> Look at controls???\n<color=green>Candymann123</color> We're gonna be here a while\n<color=yellow>Wasabi senior</color> I LOVE YOU COOTS\n<color=red>minker</color> Try again coots!\n<color=blue>RageLord3000</color> Better luck next time\n<color=yellow>PresidentBoolean</color> CMON COOTS YOU CAN DO THIS!\n<color=green>hxntxr</color> Minor setback before common coots W\n<color=red>SaddlePaddle</color> Dw coots you got this!\n<color=red>LudDog458</color> Temporary L\n<color=yellow>genco</color> Get up cat\n";
        Chat3 = "<color=yellow>Monk-sei</color> I believe in you\n<color=green>Luddeeers</color> Its okay coots (:\n<color=red>Coots-kun420</color> Coots, coots, coots, coots, coots, coots!\n<color=blue>RedditUser1203</color> Cmon coots!\n<color=red>DubDub</color> Don’t take this L coots!\n<color=red>Dudeimations</color> ON YOUR FEET COOTS\n<color=green>LudWigsFan9038</color> Its okay just try again (:\n<color=red>RefinedCootEnjoyer</color> The fruits of this labour shall be sweet young feline\n<color=red>NxvxrBeTrux</color> YOU GOT THIS COOTS!\n<color=blue>MJtheGOAT</color> Don’t stop till you get enough!\n";
        

        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Do Stuff

            hold = Chat1;
            Chat1 = Chat2;
            Chat2 = Chat3;
            Chat3 = hold;
            
            
            displayChat.text = Chat1 + Chat2 + Chat3;
            timer = 0.5f;
        }
    
    }

    
    public void GameOverScreen(float Score, bool didwin)
    {
        gameObject.SetActive(true);
        // Update Score Display
        displayScore.text = ((int)Score).ToString("D7");
        if (didwin) { Chat1 = ""; Chat2 = ""; Chat3 = ""; hold = ""; Destroy(displayChat); displayWin.text = "You Win!"; }
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("HighStreetHighJinks", LoadSceneMode.Single);
    }

    public void OnMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }


        
}
