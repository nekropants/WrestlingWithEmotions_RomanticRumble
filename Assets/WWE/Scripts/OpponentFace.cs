using UnityEngine;
using System.Collections;

public class OpponentFace : SpriteSwitcher
{


        private float timer = 0.0f;
    public SpriteRenderer mouth;
    public SpriteRenderer mouth1;
    public SpriteRenderer head;
    public Sprite headNormal;
    public Sprite headPunched;
    public Sprite smile;

public AudioClip[] hardPunches;
int punchIndex =0;


    private bool running = false;
    public void Hit(Vector3 scale)
    {
        mouth1.gameObject.SetActive(false);

        transform.localScale = scale;
        head.sprite = headPunched;
        timer = 0.3f;
        mouth.enabled = false;
        running = true;
        

        print(punchIndex);
        AudioController.Play(hardPunches[punchIndex]);
        
        punchIndex++;
        punchIndex %= hardPunches.Length;
    }




    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
    void Update()
    {

        if (running == false)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (head.transform.localScale == transform.localScale)
            {
                mouth.enabled = true;
                head.sprite = headNormal;
            }
        }



        if (timer < -1)
        {
            mouth.sprite = smile;
            print(smile);
        }

    }

    public override void SwitchTo(Object[] sprites)
    {
        TrySetSprite(ref headNormal, sprites);
        TrySetSprite(ref headPunched, sprites);
        TrySetSprite(ref smile, sprites);
    }

}
