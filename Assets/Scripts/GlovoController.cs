using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlovoController : MonoBehaviour
{
    [SerializeField] private float _steerSpeed = 150;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _movementMultiplyer = 1.03f;
    [SerializeField] private float _speedMultyTimerValue = 3;
    [SerializeField] private Color32 _hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] private Color32 _noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    private SpriteRenderer _spriteRenderer;
    private DileveryManager _dileveryManager;
    private int _score = 0;
    private float _speedMultyTimer = 0;
    public bool _hasPackage = false;

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _dileveryManager = GameObject.FindObjectOfType<DileveryManager>();
        _speedMultyTimer = _speedMultyTimerValue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        _speedMultyTimer -= Time.deltaTime;
        if (_speedMultyTimer <= 0) {
            _moveSpeed *= _movementMultiplyer;
            _steerSpeed *= _movementMultiplyer;
            _speedMultyTimer = _speedMultyTimerValue;
        }

        float steerAmount = (- Input.GetAxis("Horizontal") ) * _steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime;

        transform.Rotate(new Vector3(0, 0, steerAmount));
        transform.Translate(new Vector3(0, moveAmount, 0), Space.Self);

        _scoreText.SetText(_score.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Package" && !_hasPackage) {
            Debug.Log("Package is picked up");
            _spriteRenderer.color = _hasPackageColor;

            collision.gameObject.SetActive(false);

            _hasPackage = true;
            _dileveryManager.hasEnabledPackage = false;
           
        }
        
        if(collision.tag == "Customer" && _hasPackage) {
            Debug.Log("Package is deliverd");
            _spriteRenderer.color = _noPackageColor;

            collision.gameObject.SetActive(false);

            _hasPackage = false;
            _dileveryManager.hasEnabledCustomer = false;
            _score++;
        }
    }
}
