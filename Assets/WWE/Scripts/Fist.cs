using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace WWE
{

    public class Fist : MonoBehaviour
    {

        public bool alternative = false;
        public OpponentFace opponent;
        public Animator animator;
        // Use this for initialization
        public IEnumerator Start()
        {
            animator = GetComponent<Animator>();
           // animator.enabled = false;
            print(animator.enabled);


         //   animator.enabled = true;

            print(animator.enabled);

            yield return null;
          //  animator.SetBool("exit", true);
           // yield return new WaitForSeconds(3 );

         //   SceneManager.LoadScene("EndRingOnFloor");
        }

        public void Trigger()
        {
            animator.enabled = true;
            print(animator.runtimeAnimatorController);
          //  animator.Stop();
            animator.Play("FistAnimation");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Hit()
        {
            if(alternative)
            opponent.Hit(new Vector3(-1, 1,1 ));
            else
            {

                opponent.Hit(transform.localScale);

            }
        }
    }
}