using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Config
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float screenWidthInUnits = 16f;
    
    // Cached references
    Ball _theBall;
    
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<GameSession>();
        _theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        var transform1 = transform;
        var position = transform1.position;
        var paddlePos = new Vector2(position.x, position.y) {x = Mathf.Clamp(GetXPos(), minX, maxX)};
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (FindObjectOfType<GameSession>().IsAutoPlayEnabled())
        {
            return _theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
