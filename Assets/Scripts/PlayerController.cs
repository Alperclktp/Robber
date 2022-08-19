using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDamageble
{
    public static PlayerController Instance;

    [SerializeField] PlayerSettings playerSettings;

    [SerializeField] private FloatingJoystick joystick;

    private Animator anim;

    private float maxHealth = 100f;

    [SerializeField] private float currentHealth;

    [Header("Models")]

    public GameObject playerModel;

    [SerializeField] private GameObject ninjaModel;

    [SerializeField] private GameObject flowerModel;

    [Header("UI")]
    public GameObject canvas;

    public GameObject healthBarCanvas;

    public Slider healthBarSlider;

    public Coroutine rippleCoroutine;

    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem hideEffect;

    [SerializeField] private ParticleSystem showEffect;

    public ParticleSystem rippleEffect;

    [Header("Debuggers")]
    public bool canMove;

    public bool canHide;

    public bool inSight;

    public bool isSafe;

    public bool isDie;

    [HideInInspector] public bool isTrigger;

    private void Awake()
    {
        Instance = this;

        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();

        CheckHealth();

        SetHealthBarCanvasPosition();

        CheckTag();
    }

    private void Movement()
    {
        canMove = true;

        Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);

        //Debug.Log("Input: " + input);

        if (moveDirection.magnitude <= 0f)
        {
            //Debug.Log("Idleling");

            anim.SetFloat("Velocity", 0, 0.2f, Time.deltaTime);

            Hiding();

            Rotation(moveDirection);

            //leftTrailer.enabled = false;

            //rightTrailer.enabled = false;
        }

        if (moveDirection.magnitude >= 0.5f)
        {
            //Debug.Log("Walk");

            anim.SetFloat("Velocity", inputMagnitude, 0.05f, Time.deltaTime);

            transform.Translate(moveDirection * playerSettings.SneakWalkSpeed * Time.deltaTime, Space.World);

            Showing();

            Rotation(moveDirection);

            //leftTrailer.enabled = false;

            //rightTrailer.enabled = false;
        }

        if (moveDirection.magnitude >= 1f)
        {
            //Debug.Log("Running");

            transform.Translate(moveDirection * playerSettings.RunSpeed * Time.deltaTime, Space.World);

            inputMagnitude /= 2f;

            Showing();

            Rotation(moveDirection);

            //leftTrailer.enabled = true;

            //rightTrailer.enabled = true;
        }
    }

    private void Rotation(Vector3 rotate)
    {
        if (rotate != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(rotate, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, playerSettings.RotationSpeed * Time.deltaTime);
        }
        else
        {
            canMove = false;
        }
    }

    private void Hiding()
    {
        canHide = true;

        if (canHide) { playerModel.transform.tag = "Flower"; }

        StartCoroutine(IEHideParticleEffect());

        flowerModel.SetActive(true);

        ninjaModel.SetActive(false);
    }

    private void Showing()
    {
        canHide = false;

        StartCoroutine(IEShowParticleEffect());

        ninjaModel.SetActive(true);

        flowerModel.SetActive(false);
    }

    private void CheckTag()
    {
        if (canMove) { playerModel.transform.tag = "Player"; }
    }

    private IEnumerator IEHideParticleEffect()
    {
        hideEffect.Play();

        yield return null;

        hideEffect.Stop();
    }

    private IEnumerator IEShowParticleEffect()
    {
        showEffect.Play();

        yield return null;

        showEffect.Stop();
    }

    public IEnumerator IERippleEffectBetweenTime()
    {
        if (isTrigger) 
        {
            yield return new WaitForSeconds(1f);

            rippleEffect.Play();

            rippleCoroutine = StartCoroutine(IERippleEffectBetweenTime());
        }
    }

    private void CheckHealth()
    {
        healthBarSlider.value = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Die();

        }

        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
    }

    private void SetHealthBarCanvasPosition()
    {
        if (canMove)
        {
            healthBarCanvas.transform.position = new Vector3(healthBarCanvas.transform.position.x, 1f, healthBarCanvas.transform.position.z);
        }
        else if (canHide)
        {
            healthBarCanvas.transform.position = new Vector3(healthBarCanvas.transform.position.x, 1.5f, healthBarCanvas.transform.position.z);
        }
    }

    private void Die()
    {
        Debug.Log("You Died");

        isDie = true;

        canvas.SetActive(false);

        healthBarCanvas.SetActive(false);

        flowerModel.SetActive(false);

        ninjaModel.SetActive(true);

        isTrigger = false;

        GetComponent<Rigidbody>().isKinematic = true;

        GetComponent<PlayerController>().enabled = false;

        GetComponent<CapsuleCollider>().enabled = false;
  
        anim.Play("Dying");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void StopRippleEffect()
    {
        StopCoroutine(rippleCoroutine);
    }
}


