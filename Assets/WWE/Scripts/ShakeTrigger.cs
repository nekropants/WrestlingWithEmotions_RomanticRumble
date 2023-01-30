using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WWE
{
    public class ShakeTrigger : SpriteSwitcher
    {

        public Shake shake;
        public Flash flash;

        public SpriteRenderer FaceRenderer;
        public Fist leftFist;
        public Fist rightFist;
        public SpriteRenderer fistLeft;
        public SpriteRenderer fistRight;
        public Sprite smile;

        public GameObject punchController;
        
        
        int punchIndex = 0;
        public AudioClip[] punchSounds;
        

        // Use this for initialization
        IEnumerator Start () {
	    yield return new WaitForSeconds(2);
            GetComponent<Animator>().SetBool("stopPunching", true);
            yield return new WaitForSeconds(1);

            FaceRenderer.sprite = smile;
            yield return new WaitForSeconds(1);

         //   fistLeft.gameObject.SetActive(false);
           // fistRight.gameObject.SetActive(false);

            punchController.SetActive(true);
            //SceneManager.LoadScene("RingFight");
            int count = 0;
            while(count < 7)
            {
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    count++;
                    if(count % 2 == 0)
                        leftFist.Trigger();
                    else
                        rightFist.Trigger();
                    
                    //yield return new WaitForSeconds(0.05f);
                    
                }
                yield return null;
            }
                    yield return new WaitForSeconds(0.8f);

            SceneManager.LoadScene("EndRingOnFloor");
        }

        public override void SwitchTo(Object[] sprites)
        {
            TrySetSprite(ref smile, sprites);
        }

        // Update is called once per frame
        public void Trigger () {
            shake.AddShake(true);
            flash.TriggerFlash();
            
           
            
            AudioController.Play(punchSounds[punchIndex]);
            
             punchIndex++;
            
            punchIndex %= 2;
    }
}
}
