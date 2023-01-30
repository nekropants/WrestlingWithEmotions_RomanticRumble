using UnityEngine;
using System.Collections;

public class BottleSpin : MonoBehaviour
{
    private int direction = 1;

    public float spins = 3;
    public float speed = 5;

	// Use this for initialization
    IEnumerator Start()
    {
        Quaternion offset = Quaternion.identity;

        while (true)
        {

            float timer = spins * 360;

            while (timer > 0)
            {
                transform.rotation *= Quaternion.Inverse(offset);

                timer -= Time.deltaTime * speed;
                timer = Mathf.Max(timer, 0);
                offset = Quaternion.AngleAxis(timer, Vector3.forward * direction);


                print(timer);
               
                transform.rotation *= offset;



                yield return null;
            }

        }
        yield return new WaitForSeconds(4);

	}
	
	// Update is called once per frame
	
}
