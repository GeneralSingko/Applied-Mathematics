using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMotionManager3 : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float _speed;
    [SerializeField] private float _sineLenght = 1;
    [SerializeField] private float _cosineLenght = 1;

    [SerializeField] private bool _useSine;
    [SerializeField] private bool _useCosine;

    [SerializeField] private List<GameObject> _targetGameObjects;
    [SerializeField] private SineCosineGenerator _sineCosineGenerator;

    private float _angle = 0;
    private List<Vector3> _initialPositions;

    private void Start()
    {
        _angle = 0;
        _initialPositions = new List<Vector3>();
        foreach (var targetGameObject in _targetGameObjects)
        {
            _initialPositions.Add(targetGameObject.transform.position);
        }
    }

    public void AddTargetGameObject(GameObject newTargetGameObject)
    {
        _targetGameObjects.Add(newTargetGameObject);
        _initialPositions.Add(newTargetGameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        _angle += Time.deltaTime * _speed;

        for (int i = 0; i < _targetGameObjects.Count; i++)
        {
            Vector3 position = _initialPositions[i];

            if (_useSine)
            {
                _targetGameObjects[i].transform.position = new Vector3(_targetGameObjects[i].transform.position.x, Mathf.Sin(_angle * _sineCosineGenerator.sineValue) * _sineLenght + _initialPositions[i].y,
                    _targetGameObjects[i].transform.position.z);
            }

            if (_useCosine)
            {
                _targetGameObjects[i].transform.position = new Vector3(Mathf.Cos(_angle * _sineCosineGenerator.cosineValue) * _cosineLenght + _initialPositions[i].x, _targetGameObjects[i].transform.position.y,
                     _targetGameObjects[i].transform.position.z);
            }
        }
    }
}