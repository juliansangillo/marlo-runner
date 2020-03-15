using UnityEngine;

public enum Direction {
    Left,
    Right
}

public class SpeedArea : MonoBehaviour {

    [SerializeField] private Direction direction = Direction.Right;

    public Direction GetDirection {
        get {
            return direction;
        }
    }

    private void Start() {
        
        if(direction == Direction.Left) {
            Transform child = transform.GetChild(0);
            child.RotateAround(child.position, Vector3.up, 180);
        }

    }

}
