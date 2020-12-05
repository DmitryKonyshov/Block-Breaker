using UnityEngine;

public class Ball : MonoBehaviour
{
    //Config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] private float randomFactor = 0.7f;

    //State

    Vector2 _paddleToBallVector;
    bool _hasStarted;

    // Cashed component

    AudioSource _myAudioSource; 
    Rigidbody2D _myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _paddleToBallVector = transform.position - paddle1.transform.position;
        _myAudioSource = GetComponent<AudioSource>();
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasStarted) return;
        LockBallToPaddle();
        LaunchOnMouseClick();
    }

    private void LaunchOnMouseClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        _hasStarted = true;
        _myRigidbody2D.velocity = new Vector2(xPush, yPush);
    }

    private void LockBallToPaddle()
    {
        var position = paddle1.transform.position;
        var paddlePos = new Vector2(position.x, position.y);
        transform.position = paddlePos + _paddleToBallVector;
    }

    private void OnCollisionEnter2D()
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        if (!_hasStarted) return;
        var clip = ballSounds[Random.Range(0, ballSounds.Length)];
        _myAudioSource.PlayOneShot(clip);
        _myRigidbody2D.velocity += velocityTweak;
    }
}
