using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelection : MonoBehaviour
{

    public List<int> PlayerCount = new List<int>();
    public void JoinTeamOne()
    {
        
    }
    public void JoinTeamTwo()
    {

    }
    public void FourPlayer()
    {
        PlayerCount.Add(0);
        PlayerCount.Add(1);
        PlayerCount.Add(2);
        PlayerCount.Add(3);
    }
}
