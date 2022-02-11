using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public void EnablePlatforms(string color)
    {
        foreach (Transform child in GameObject.FindGameObjectWithTag("platform").transform)
        {
            if (child.tag == color)
            {
                child.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
