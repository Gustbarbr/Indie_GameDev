using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform Player;


    void Update()
    {
        // Pega a posi��o X e Y do player (skin) e mantem o Z no -1 para que tudo apare�a
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -1);
    }
}
