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
        controller.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y + (Input.GetAxis("Fly") * speed), controller.transform.position.z);
    }
}
