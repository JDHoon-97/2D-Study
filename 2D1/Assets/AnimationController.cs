using UnityEngine;
using UnityEngine.PlayerLoop;

public class AnimationController : MonoBehaviour
{
    public SpriteRenderer spriterenderer;
    public Sprite[] spriteUp;
    public Sprite[] spriteDown;
    public Sprite[] spriteLeft;
    public Sprite[] spriteRight;
    public Sprite[] spriteStop;
    private Sprite[] currentSprites;
    
    private int index = 0;
    
    [SerializeField] private  float waitTick = 1;
    private float currentTick;
    
    void Start()
    {
        currentTick = Time.realtimeSinceStartup;
    }
    
    void Update()
    {
        if (Time.realtimeSinceStartup - currentTick < waitTick)
            return;
        currentTick = Time.realtimeSinceStartup;
        
        currentSprites = spriteStop;
        
        if (Input.GetKey(KeyCode.W))
        {
            currentSprites = spriteUp;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSprites = spriteDown;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentSprites = spriteLeft;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentSprites = spriteRight;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            currentSprites = spriteStop;
        }

        if (index >= 4)
        {
            index = 0;
        }
        spriterenderer.sprite = currentSprites[index++];
    }
}
