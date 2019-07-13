using UnityEngine;
using System.Collections.Generic;

public class PlayersPath
{
    public List<Vector2> points;
    public List<float> timeSpent;

    public PlayersPath()
    {
        points = new List<Vector2>();
        timeSpent = new List<float>();
    }

    public void Clear()
    {
        points.Clear();
        timeSpent.Clear();
    }
}