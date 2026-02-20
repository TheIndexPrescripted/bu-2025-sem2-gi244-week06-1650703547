using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam03 : MonoBehaviour
{
    public float speed;
    public float xRange = 10;
    public GameObject projectilePrefab;

    public bool enableAutoFireMode;
    public float autoFireInterval = 0.1f;

    private float horizontalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private float autoFireTimer;   // ? ตัวจับเวลา

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    void Update()
    {
        horizontalInput = moveAction.ReadValue<Vector2>().x;
        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);

        // จำกัดขอบเขตการเคลื่อนที่
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        // ? ระบบ Auto Fire
        if (enableAutoFireMode)
        {
            autoFireTimer += Time.deltaTime;

            if (autoFireTimer >= autoFireInterval)
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
                autoFireTimer = 0f;
            }
        }
        else
        {
            // ยิงปกติเมื่อกดปุ่ม
            if (shootAction.triggered)
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
            }
        }
    }
}

