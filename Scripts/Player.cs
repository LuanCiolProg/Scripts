using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool isSpeedBoostActive = false;
    public bool shieldsActive = false;
    public int lives = 3;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private GameObject _ShieldGameObject;

    [SerializeField]
    private float _fireRate = 0.25f;
    
    private float _canFire = 0.0f;
    
    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;


    void Start()
    {
        (transform.position) = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.Updatelives(lives);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot(); 
        }
    }
   
    private void Shoot()
    {
        if (Time.time > _canFire)
        {
           if (canTripleShot == true)
            {
                Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //if speed boost enable
        //move 1.5x the normal speed
        //else
        //move normal speed

        if(isSpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
       else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        

        //if player on the y is greater than 0
        //set player position on the Y to 0
       
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }


        //if player position on the x is greater than 9.5
        //position on the x needs to be -9.5
        
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }
    
    public void Damage()
    {
        //subtract 1 life from the player
        //if player has shields
        //do nothing
        if (shieldsActive == true)
        {
            shieldsActive = false;
            _ShieldGameObject.SetActive(false);
            return;
        }

        lives--;
        _uiManager.Updatelives(lives);

        if (lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
        //if lives < 1 (meaning 0)
        //destroy this object
    }
    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
   

    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    
    public void EnableShields()
    {
        shieldsActive = true;
        _ShieldGameObject.SetActive(true);
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }

}
