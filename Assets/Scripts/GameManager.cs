using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Ref to client script.
    public ClientScript clientScript;
    public PlayerScript player;
    // Check if clients green and height values have been met.
    [SerializeField]
    private bool clientOneGreenPrefMet, clientTwoGreenPrefMet;
    [SerializeField]
    private bool clientOneHeightPrefMet, clientTwoHeightPrefMet;
    // Store players points, new game starts at 0.
    [SerializeField]
    public int playerPoints = 0;

    private void Update()
    {
        CheckIfClientsPreferencesHaveBeenMet();
    }

    private void CheckIfClientsPreferencesHaveBeenMet()
    {
        // Temp variables created for client scripts fields, as we have to repeat the code.
        bool greenValueIsHigh = clientScript.isGreenValueOverThreshold;
        int client1GreenPref = clientScript.clientOneGreenPreference;
        int client2GreenPref = clientScript.clientTwoGreenPreference;
        

        bool perlinHeightValueIsHigh = clientScript.isPerlinNoiseValueOverThreshold;
        int client1HeightPref = clientScript.clientOneHeightPreference;
        int client2HeightPref = clientScript.clientTwoHeightPreference;
      
        // Check if Client one has high green preference and the green vlaue is over the threshold.
        if (greenValueIsHigh == true && client1GreenPref == 1)
        {
            clientOneGreenPrefMet = true; // If both return true, set green value met to true.
        }
        else clientOneGreenPrefMet = false; // If both return false, set green value met to false.

        // Check if client 2 green pref has been met.
        if (greenValueIsHigh == true && client2GreenPref == 1)
        {
            clientTwoGreenPrefMet = true;
        }
        else clientTwoGreenPrefMet = false;
       

        // Check if Client one has high height preference for terrain and the height value from perlin noise is over the threshold.
        if (perlinHeightValueIsHigh == true && clientScript.clientOneHeightPreference == 1)
        {
            clientOneHeightPrefMet = true; // If both return true, set height is okay bool to true for client 1.
        }
        else clientOneHeightPrefMet = false; // If both return fasle, set height is okay bool to false for client 1.
        // Client 2 height pref check.
        if (perlinHeightValueIsHigh == true && clientScript.clientTwoHeightPreference == 1)
        {
            clientTwoHeightPrefMet = true;
        }
        else clientTwoHeightPrefMet = false;


        // Check if Client one has low green preference and the green vlaue is under the threshold.
        if (greenValueIsHigh == false && client1GreenPref == 0)
        {
            clientOneGreenPrefMet = true; // If both return true, set green value met to true.
        }
        else clientOneGreenPrefMet = false; // If both return false, set green value met to false.
        // Check if client 2 green pref has been met.
        if (greenValueIsHigh == false && client2GreenPref == 0)
        {
            clientTwoGreenPrefMet = true;
        }
        else clientTwoGreenPrefMet = false;


        // Check if Client one has low height preference for terrian and the height value from perlin noise is under the threshold.
        if (perlinHeightValueIsHigh == false && client1HeightPref== 0)
        {
            clientOneHeightPrefMet = true; // If both return true, set height value met to true.
        } else clientOneHeightPrefMet = false; // If both return false, set height value met to false.
        // Check if client 2 height pref has been met.
        if (perlinHeightValueIsHigh == false && client2HeightPref == 0)
        {
            clientTwoHeightPrefMet = true;
        }
        else clientTwoHeightPrefMet = false;

       
    }

    public void SubmitViewForClientOne()
    {
        // If veiw is submitted and green check is met + 50 points.
        if (clientOneGreenPrefMet == true)
        {
            playerPoints += 50;
        }
        // If veiw is submitted and height check is met + 50 points.
        if (clientOneHeightPrefMet == true)
        {
            playerPoints += 50;
        }
        // If both checks are met bonus points are earned.
        if (clientOneGreenPrefMet == true && clientOneHeightPrefMet == true)
        {
            playerPoints += 100;
        }
        // If no checks have been met, player fails and loses 50 points.
        if (clientOneGreenPrefMet == false && clientOneHeightPrefMet == false)
        {
            playerPoints -= 50;
        }
    }

    public void SubmitViewForClientTwo()
    {   
        if (player.viewSubmittedForClient1== true) // Need to check player has submitted view for 1st view, or adds points for both clients.
        {
            if (clientTwoGreenPrefMet == true)
            {
                playerPoints += 50;
            }
            if (clientTwoHeightPrefMet == true)
            {
                playerPoints += 50;
            }
            if (clientTwoGreenPrefMet == true && clientTwoHeightPrefMet == true)
            {
                playerPoints += 100;
            }

            if (clientTwoGreenPrefMet == false && clientTwoHeightPrefMet == false)
            {
                playerPoints -= 50;
            }
        }
        
    }
}
