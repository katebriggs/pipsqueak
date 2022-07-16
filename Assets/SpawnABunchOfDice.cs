using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnABunchOfDice : MonoBehaviour
{
    public Rigidbody DicePrefab;
    public int DiceCount;

    public int fireForce = 200;
    public int spinForce = 500;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = -8; i <= 8; i += 4)
        {
            for (int j = -8; j<=8; j += 4)
            {
                if (i == j || j == -i) continue;
                SpawnDicePattern(transform.position + new Vector3(i, 0, j));
            }
        }
    }

    IEnumerator SpawnDice()
    {
        for (int i = 0; i < DiceCount; i++)
        {
            var dice = Instantiate(DicePrefab, transform.position, Quaternion.identity);
            dice.AddForce(Random.onUnitSphere * fireForce);
            dice.AddTorque(Random.onUnitSphere * spinForce);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void SpawnDicePattern(Vector3 position)
    {
        var index = Random.Range(0, 6) + 1;
        bool flipped = Random.value > 0.5f;
        switch (index)
        {
            case 1:
                SpawnSingleDie(position);
                break;
            case 2:
                SpawnSingleDie(position + Vector3.left + (flipped ? Vector3.forward : Vector3.back));
                SpawnSingleDie(position + Vector3.right + (!flipped ? Vector3.forward : Vector3.back));
                break;
            case 3:
                SpawnSingleDie(position);
                SpawnSingleDie(position + Vector3.left + (flipped ? Vector3.forward : Vector3.back));
                SpawnSingleDie(position + Vector3.right + (!flipped ? Vector3.forward : Vector3.back));
                break;
            case 4:
                SpawnSingleDie(position + Vector3.left + Vector3.forward);
                SpawnSingleDie(position + Vector3.right + Vector3.forward);
                SpawnSingleDie(position + Vector3.left + Vector3.back);
                SpawnSingleDie(position + Vector3.right + Vector3.back);
                break;
            case 5:
                SpawnSingleDie(position);
                SpawnSingleDie(position + Vector3.left + Vector3.forward);
                SpawnSingleDie(position + Vector3.right + Vector3.forward);
                SpawnSingleDie(position + Vector3.left + Vector3.back);
                SpawnSingleDie(position + Vector3.right + Vector3.back);
                break;
            default: //6
                SpawnSingleDie(position + Vector3.left + Vector3.forward);
                SpawnSingleDie(position + Vector3.right + Vector3.forward);
                SpawnSingleDie(position + Vector3.left + Vector3.back);
                SpawnSingleDie(position + Vector3.right + Vector3.back);
                SpawnSingleDie(position + (flipped ? Vector3.left : Vector3.forward));
                SpawnSingleDie(position + (flipped ? Vector3.right : Vector3.back));
                break;
        }
    }

    Rigidbody SpawnSingleDie(Vector3 position)
    {
        return Instantiate(DicePrefab, position, RandomAxisRotation());
    }

    Quaternion RandomAxisRotation()
    {
        var lookVectors = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        return Quaternion.FromToRotation(Vector3.up, lookVectors[Random.Range(0, lookVectors.Length)]);
    }
}
