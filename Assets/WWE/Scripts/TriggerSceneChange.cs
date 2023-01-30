using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace WWE
{


    public class TriggerSceneChange : MonoBehaviour
    {
        public string nextScene;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TriggerScene()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
