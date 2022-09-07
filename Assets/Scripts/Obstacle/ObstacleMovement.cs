using UnityEngine;

public class ObstacleMovement : LeftMovement
{
    private float _leftBound = -15f;
    protected override void Update()
    {
        base.Update();
        if (transform.position.x < _leftBound)
        {
            gameObject.SetActive(false);
        }
    }
}
