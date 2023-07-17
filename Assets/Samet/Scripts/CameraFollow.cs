using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    #region Singleton
    public static CameraFollow Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is more than one CameraFollow in the scene!");
            Destroy(this);
        }
    }
    #endregion
    public Transform player1;
    public Transform player2;
    public float minDistance = 5f;
    public float maxDistance = 10f;
    public float smoothSpeed = 5f;

    private Vector3 offset;
    private Vector3 midpoint;
    private Vector3 desiredPosition;
    [SerializeField] private Transform cameraTransform;
    
    void Init()
    {
        // Calculate the initial offset between the camera and the midpoint of the players
        Vector3 initialMidpoint = (player1.position + player2.position) / 2f;
        offset = cameraTransform.position - initialMidpoint;
    }

    public void InitPlayertransforms(Transform player1, Transform player2)
    {
        this.player1 = player1;
        this.player2 = player2;
        Init();
    }

    void LateUpdate()
    {
        if(player1 == null || player2 == null)
        {
            return;
        }

        midpoint = (player1.position + player2.position) / 2f;

        // float distance = Vector3.Distance(player1.position, player2.position);
        // float clampedDistance = Mathf.Clamp(distance, minDistance, maxDistance);

        desiredPosition = midpoint + offset;

        // desiredPosition = midpoint + offset;


        // if(cameraComponent == null) cameraComponent = Camera.main;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
