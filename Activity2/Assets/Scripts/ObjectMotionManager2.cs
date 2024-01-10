using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMotionManager2 : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private float _speed = 100;
    [SerializeField] private float _sineLenght = 10;
    [SerializeField] private float _cosineLenght = 10;

    [SerializeField] private bool _useSine = true;
    [SerializeField] private bool _useCosine = true;

    [SerializeField] private List<GameObject> _targetGameObjects;

    private float _angle = 0;
    private List<Vector3> _initialPositions;
    private List<MotionData> _motionData;

    private void Start()
    {
        _angle = 0;
        _initialPositions = new List<Vector3>();
        _motionData = new List<MotionData>();
        foreach (var targetGameObject in _targetGameObjects)
        {
            _initialPositions.Add(targetGameObject.transform.position);
            _motionData.Add(new MotionData(Random.Range(0f, 360f), Random.Range(0f, 360f)));
        }
    }

    public void AddTargetGameObject(GameObject newTargetGameObject)
    {
        _targetGameObjects.Add(newTargetGameObject);
        _initialPositions.Add(newTargetGameObject.transform.position);
        _motionData.Add(new MotionData(Random.Range(0f, 360f), Random.Range(0f, 360f)));
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
                position.y = Mathf.Sin(Mathf.Deg2Rad * (_angle + _motionData[i].SineAngle)) * _sineLenght + _initialPositions[i].y;
            }

            if (_useCosine)
            {
                position.x = Mathf.Cos(Mathf.Deg2Rad * (_angle + _motionData[i].CosineAngle)) * _cosineLenght + _initialPositions[i].x;
            }

            _targetGameObjects[i].transform.position = position;
        }
    }
}

[System.Serializable]
public class MotionData
{
    public float SineAngle;
    public float CosineAngle;

    public MotionData(float sineAngle, float cosineAngle)
    {
        SineAngle = sineAngle;
        CosineAngle = cosineAngle;
    }
}