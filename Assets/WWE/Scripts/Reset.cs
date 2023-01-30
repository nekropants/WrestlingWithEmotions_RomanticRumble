using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WWE
{
    public class Reset : MonoBehaviour
    {



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.F6))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}