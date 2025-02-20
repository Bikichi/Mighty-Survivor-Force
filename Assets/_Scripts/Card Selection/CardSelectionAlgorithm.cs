using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionAlgorithm : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _cardUpgrade;
    [SerializeField]
    private GameObject[] _cardPositionInGame;

    private int randomIndexMidF;
    private int randomIndexMidS;
    public static int randomIndex;

    public static GameObject[] _instanceCard = new GameObject[3]; 

    private void Start()
    {
        randomIndexMidF = 100;
        randomIndexMidS = 200;
    }

    private void Update()
    {
        if (VariableStatic.chooseCard)
        {

            for (int i = 0; i < 3; i++)
            {
                do
                {
                    randomIndex = Random.Range(0, _cardUpgrade.Length);
                } while (randomIndex == randomIndexMidF || randomIndex == randomIndexMidS);

                if (i == 0)
                {
                    randomIndexMidF = randomIndex;
                }
                if (i == 1)
                {
                    randomIndexMidS = randomIndex;
                }

                _instanceCard[i] = Instantiate(_cardUpgrade[randomIndex], _cardPositionInGame[i].transform.position, _cardUpgrade[randomIndex].transform.rotation, _cardPositionInGame[0].transform.parent);
            }

            VariableStatic.chooseCard = false;
        }
    }
}
