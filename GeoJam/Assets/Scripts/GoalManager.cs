using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public int orbsCollected = 0;
    public Transform explosionPoint;

    public void GoalCheck(string color, GameObject effect)
    {
        orbsCollected++;

        foreach (Transform child in transform.GetChild(0))
        {
            if (child.tag == color)
            {
                Instantiate(effect, explosionPoint.position, Quaternion.identity);
                child.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && orbsCollected == 7)
        {
            FindObjectOfType<GameManager>().Invoke("LevelComplete", 1f);
            col.GetComponent<GoodPlatformerController>().Win();
            //col.transform.position = explosionPoint.position;
        }
    }
}
