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

}
