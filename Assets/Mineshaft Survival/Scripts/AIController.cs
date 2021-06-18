using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AIController : MonoBehaviour {

   [Header("AI Stats")]
    public float Health; //Amount of health they have
    public float Damage; //Amount of damage they deal
    public float AttackSpeed; //How fast can they attack | 1 = second
    public float DetectionRange; //From how many meters can they detect players

    [Header("AI Extra's")]
    public bool Agressive = true; //If they should attack first
    public bool PathFinder = true; //If they should walk when in ambient mode

    [Header("Needed Scripts")]
    public AICharacterControl Target; //AICharacterControl script
    public ThirdPersonCharacter thirdChar; //ThirdPersonCharacter script

    public GameObject pathRotator; //RandomPath Gen rotation cube
    public GameObject pathLeader; //RandomPath Gen sphere

    public GameObject AIObj; //Model of the AI
    public GameObject Player;//Player
    Rigidbody rb; //Automaticly finds the rigidbody
    float distance; //distance float for distance calculations

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(PathFinder == true) //if Pathfinder is enabled AI will automaticly generate his path 
        {
            StartCoroutine(Path());
        }
        StartCoroutine(Attack());

    }

	void Update ()
    {
        if(Health <= 0) //if healht is 0 or less it will die and change models rotation to -90
        {
            AIObj.transform.eulerAngles = new Vector3(-90, -90, 0);
            Die();
        }

        distance = Vector3.Distance(gameObject.transform.position, Player.transform.position); //calulates the distance between player and the AI

        if(Agressive == true) //when agressive
        {
            if (distance <= DetectionRange) //if player is in his detection range
            {
                Target.target = Player.transform; //set player as target
            }
            else
            {
                if (PathFinder == true) //If player is not in range
                {
                    Target.target = pathLeader.transform; //set path finding as target
                }
                else
                {
                    Target.target = null; //if not in range and not pathfinidng set target to null(nothing)
                }
            }
        }
        else
        {
            if(PathFinder == true) //If not agressive but path finidng
            {
                Target.target = pathLeader.transform; //set path finding as target
            }
            else
            {
                Target.target = null; //if not agressive nor pathfinding set target to null(nothing)
            }
        }

	}
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(AttackSpeed); //Number of seconds to wait between attacks
        if (Agressive && distance <= 2 && Health >1) //if player is in range of 2meters and AI has more than 1 health
        {
            Player.transform.GetComponent<LifeStats>().Health -= Damage; //deal damage to player
            StartCoroutine(Attack()); //start new attack
        }
        else
        {
            StartCoroutine(Attack()); //if not agressive or not in range try attacking again
        }

    }

    IEnumerator Path()
    {
        pathRotator.transform.position = gameObject.transform.position; //set path's rotators position to current AI position
        pathRotator.transform.Rotate(Vector3.up * Random.Range(-220, 220)); //randomly rotate path rotator
        yield return new WaitForSeconds(5); //wait 5 seconds before repeating 
        StartCoroutine(Path()); //start path finding again
    }
    public void Die()
    {
        rb.isKinematic = true; //set rigidbody to kinematic when dead
        thirdChar.enabled = false; //disable all scripts after death
        Target.enabled = false; // ^
        this.enabled = false; // ^
    }
}
