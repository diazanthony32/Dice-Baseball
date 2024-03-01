using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public GameObject dice;
    private List<GameObject> diceList = new List<GameObject>();

    public int diceAmount = 2;

    [SerializeField] public float maxRandomForceValue = 1000;
    [SerializeField] private float startingRollingForce = 1000;

    private float forceX, forceY, forceZ;

    private int diceTotal;

    private bool readyToRoll = true;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // spawns all the dice dictated above and stacks them nicely on top of eachother with a slight random rotation
        for (int i = 0; i < diceAmount; i++) 
        {
            GameObject die = PrefabUtility.InstantiatePrefab(dice) as GameObject;

            // sleep the physics calculateion for a single frame to allow position modification
            die.GetComponent<Rigidbody>().Sleep();

            die.transform.position += Vector3.Scale((Vector3.up * (i+1)) , die.GetComponent<BoxCollider>().size);
            die.transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0.0f, 360.0f));

            // adds die to total die list
            diceList.Add(die);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (diceList != null)
        {
            if (Input.GetMouseButtonDown(0) && readyToRoll)
            {
                // prevent multiple rolls
                readyToRoll = false;

                foreach (GameObject die in diceList) 
                {
                    RollDie(die);
                }

                StartCoroutine("WaitForResults");
            }
        }
    }

    // Waits for and gets the roll total for the dice
    IEnumerator WaitForResults()
    {

        // small delay to allow dice to start rolling to prevent reading the previous roll immediately
        yield return new WaitForSeconds(0.25f);

        // reset dice roll
        diceTotal = 0;

        // wait for each dice to stop moving to then add to the total roll number
        foreach (GameObject die in diceList)
        {
            yield return new WaitUntil(() => !die.GetComponent<Die>().isMoving);

            diceTotal += die.GetComponent<Die>().landedValue;
        }

        Debug.Log("Dice Total is : " + diceTotal);

        DetermineRoll(diceTotal);
    }

    // imparts a random force on the die
    void RollDie(GameObject die) 
    {
        die.GetComponent<Rigidbody>().isKinematic = false;

        forceX = Random.Range(0, maxRandomForceValue);
        forceY = Random.Range(0, maxRandomForceValue);
        forceZ = Random.Range(0, maxRandomForceValue);

        die.GetComponent<Rigidbody>().AddForce(Vector3.up * startingRollingForce);
        die.GetComponent<Rigidbody>().AddTorque(forceX, forceY, forceZ);
    }

    // evaluates what the roll of the dice equates to
    void DetermineRoll(int diceNum) 
    {
        switch (diceNum)
        {
            case 2:
                Debug.Log("<color=red>OUT! / DOUBLE-PLAY!</color>");
                break;
            case 3:
                Debug.Log("<color=green>Triple!</color>");
                break;
            case 4:
                Debug.Log("<color=red>OUT!</color>");
                break;
            case 5:
                Debug.Log("<color=red>OUT!</color>");
                break;
            case 6:
                Debug.Log("<color=green>Single!</color>");
                break;
            case 7:
                Debug.Log("<color=red>OUT!</color>");
                break;
            case 8:
                Debug.Log("<color=red>OUT!</color>");
                break;
            case 9:
                Debug.Log("<color=green>Double!</color>");
                break;
            case 10:
                Debug.Log("<color=red>OUT!</color>");
                break;
            case 11:
                Debug.Log("<color=red>OUT!</color>");
                break;
            case 12:
                Debug.Log("<color=green>Home-Run!</color>");
                break;
            default:
                Debug.LogError("ERROR : Invalid Roll");
                break;
        }

        readyToRoll = true;
    }
}
