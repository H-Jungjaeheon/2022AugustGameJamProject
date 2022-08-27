using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCenter : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            SceneTransition obj = gameObject.AddComponent<SceneTransition>();
            obj.scene = "GameScene";
            obj.PerformTransition();
        }
    }
}
