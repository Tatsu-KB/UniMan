using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Player_Move player;
    //[SerializeField] ContactFilter2D filter2D;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<Player_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        player.IsGround();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.NotGround();
    }
}
