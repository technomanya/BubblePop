using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayerControl;
    public BubbleController currentBubble;

    public List<BubbleData> remainingBubbles;

    [SerializeField] private float shootSpeed;

    [SerializeField] private float turnAngleDelta;

    private Vector3 shootDirection;

    private float mouseStartX, mouseDifferenceX;
    
    // Start is called before the first frame update
    void Start()
    {
        isPlayerControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBubble.isMoving)
        {
            currentBubble.MoveBubble(currentBubble.transform,transform.up,shootSpeed);
            
        }
        
        if (isPlayerControl )
        {
            if(Input.GetMouseButtonDown(0))
            {
                mouseStartX = Input.mousePosition.x;
                mouseDifferenceX = 0f;
            }
            if (Input.GetMouseButton(0))
            {
                mouseDifferenceX = mouseStartX-Input.mousePosition.x ;
                transform.Rotate(transform.forward,turnAngleDelta*mouseDifferenceX*Time.deltaTime);
                mouseStartX = Input.mousePosition.x;
                  
            }
            if(Input.GetMouseButtonUp(0))
            {
                currentBubble.isMoving = true;
                isPlayerControl = false;
            } 
            
        }
    }

    
}
