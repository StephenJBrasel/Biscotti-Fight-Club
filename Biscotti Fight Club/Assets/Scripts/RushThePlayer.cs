using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController))]
public class RushThePlayer : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform PlayerPos;
    private CharacterController m_CharacterController;

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();

        //Choose random player to bullrush.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PlayerPos = players[Random.Range(0, players.Length)].transform;
    }

    // Update is called once per frame
    void Update()
    {
        //move towards the player pos.
        transform.LookAt(PlayerPos);
        m_CharacterController.Move(transform.forward * speed * Time.deltaTime);
    }
}
