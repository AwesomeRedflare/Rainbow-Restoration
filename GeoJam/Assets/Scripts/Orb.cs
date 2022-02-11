using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Orb");
            foreach (Transform child in col.transform.GetChild(0))
            {
                if (child.tag == transform.tag)
                {
                    child.GetComponent<TrailRenderer>().enabled = true;
                }
            }

            FindObjectOfType<GoalManager>().GoalCheck(gameObject.tag, explosion);

            FindObjectOfType<PlatformManager>().EnablePlatforms(gameObject.tag);

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
