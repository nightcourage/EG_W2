using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _heightDamping;
    [SerializeField] private float _rotationDamping;
    [SerializeField] private float _height;
    private bool _isAlive;
    private Vector3 _position;
    private float _distance;

    private void Awake()
    {
        _distance = Vector3.Distance(_target.position, transform.position);
    }

    void Update()
    {
        _isAlive = _target.GetComponent<Player>().isAlive;
        
        if (_isAlive)
        {
            float wantedRotationAngle = _target.eulerAngles.y;
            float wantedHeight = _target.position.y + _height;

            float currentRotationAngle = transform.eulerAngles.y;
            _position = transform.position;
            float currentHeight = _position.y;

            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping);

            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            _position = _target.position;
            _position -= currentRotation * Vector3.forward * _distance;
            _position.y = currentHeight;
            transform.position = _position;
        
            transform.LookAt(_target);
        }
        else
        {
            _position = transform.position;
        }
    }
}
