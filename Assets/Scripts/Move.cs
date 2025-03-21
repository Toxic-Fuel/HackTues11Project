using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    CharacterController controller;
    public float speed = 5f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        controller.Move(new Vector3(0, (Input.GetAxis("Fly") * speed), 0));
    }
}
