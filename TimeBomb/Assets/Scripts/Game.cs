using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerMovement player1;
    [SerializeField] private Player2Movement player2;
    [SerializeField] private CountDown countDown;
    [SerializeField] private PlayerLife player1Life;
    [SerializeField] private Player2Life player2Life;

    private int pv1;
    private int pv2;

    // Start is called before the first frame update
    void Start()
    {
        pv1 = 3;
        pv2 = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown.timeStart <= 0)
        {
            if(pv1 == 0 || pv2 == 0)
            {
                EndGame();
            }
            else
            {
                if (player1Life.bomb == true)
                {
                    pv1--;
                }
                else if (player2Life.bomb == true)
                {
                    pv2--;
                }
                newRound();
            }
        }

    }

    private void EndGame()
    {
        Debug.Log("finjeu");
        SceneManager.LoadScene("End Screen");
        //reset (Die)
        //screen gameOver + gagnant
    }

    private void newRound()
    {
        Debug.Log(pv1);
        Debug.Log(pv2);

        player1Life.ResetPlayer();
        player2Life.ResetPlayer();
        Debug.Log("newround");
        countDown.ResetCountDown();
    }
    
    
    
    
}
