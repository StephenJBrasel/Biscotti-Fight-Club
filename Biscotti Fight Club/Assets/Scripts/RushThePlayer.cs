using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController))]
public class RushThePlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackThreshold = 2f;
    [SerializeField] private Animator animator;

    private Transform PlayerPos;
    private CharacterController m_CharacterController;


    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponent<Animator>();
        if (animator) animator.SetBool("Chase", true);

        //Choose random player to bullrush.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PlayerPos = players[Random.Range(0, players.Length)].transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if not close to player, move towards the player pos.
        transform.LookAt(PlayerPos);
        if(Vector3.Distance(PlayerPos.position, transform.position) > attackThreshold)
        {
            if (animator) animator.SetBool("Chase", true);
            m_CharacterController.Move(transform.forward * speed * Time.deltaTime);
        }
        else
        {
            //attack
            if (animator) animator.SetBool("Chase", false);
        }
    }
}
