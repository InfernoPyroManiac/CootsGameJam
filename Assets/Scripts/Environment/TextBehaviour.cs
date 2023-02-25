using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBehaviour : MonoBehaviour
{
    TMP_Text Text;
    private float TextTimer = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TextTimer > 0) { TextTimer -= Time.deltaTime;  }
        else { gameObject.GetComponent<Animator>().enabled = false; transform.localScale -= new Vector3(Time.deltaTime * 3, Time.deltaTime * 3, 0);  }

        if (transform.localScale.x < 0.23) { Destroy(gameObject); }
    }


    public void TextUpdate(string displayText, float duration, float yposition)
    {
        Text = GetComponent<TMP_Text>();
        Text.text = displayText;
        TextTimer = duration;
        transform.position = new Vector2(transform.position.x, yposition);

    }
    public void TextForceEnd()
    {
        TextTimer = 0;
    }
}
