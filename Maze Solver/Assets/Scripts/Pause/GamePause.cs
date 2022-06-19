using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{


    public void SetPause()
    {
        Time.timeScale = 0;
    }

    public void ResetPause()
    {
        Time.timeScale = 1f;
    }
}
