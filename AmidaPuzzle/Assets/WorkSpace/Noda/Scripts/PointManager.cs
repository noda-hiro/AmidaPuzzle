using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ƒ¿”ÅŒã’Ç‰Á
/// </summary>
public class PointManager : MonoBehaviour
{
    [SerializeField] private List<PointClass> _pointLists = new List<PointClass>();
    [SerializeField] private GameObject playerPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        var lane0 = _pointLists.Where(x => 0 == x.PointNumber).ToList();
        var nearPoint = _pointLists.Min(x => x.GetDistance(playerPrefab.transform));
    }
    
}
