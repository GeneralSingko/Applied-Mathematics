using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlanetButton : MonoBehaviour
{
    public GameObject planetPrefab;
    //public ObjectMotionManager objectMotionManager;
    public ObjectMotionManager2 objectMotionManager;

    public void AddPlanet()
    {
        GameObject newPlanet = Instantiate(planetPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newPlanet.GetComponent<Renderer>().material.color = Random.ColorHSV();

        // Add the new planet to the list of target game objects
        objectMotionManager.AddTargetGameObject(newPlanet);

        // Add random value to the _sineLenght and _cosineLenght
        /*objectMotionManager.SineLenght = Random.Range(1, 10);
        objectMotionManager.CosineLenght = Random.Range(1, 10);*/
    }
}
