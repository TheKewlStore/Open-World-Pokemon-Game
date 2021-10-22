using UnityEditor;
using UnityEngine;


public abstract class MovableCharacter : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Sprite sprite;

    private AnimationClip north;
    private AnimationClip south;
    private AnimationClip east;
    private AnimationClip west;

    private Animation myAnimation;

    public int framesPerDirection = 4;
    public int numberOfDirections = 4;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnimation = GetComponent<Animation>();
        sprite = spriteRenderer.sprite;
        LoadAnimationClips();
    }

    void LoadAnimationClips() {
        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[framesPerDirection];
        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        Texture2D texture = sprite.texture;

        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "sprite";

        float size = texture.width / framesPerDirection;

        for (int row = 0; row < numberOfDirections; row++) {
            AnimationClip clip = new AnimationClip();

            string directionName = "north";

            if (row == 0) {
                directionName = "south";
            }
            else if (row == 1) {
                directionName = "west";
            }
            else if (row == 2) {
                directionName = "east";
            }
            else if (row == 3) {
                directionName = "north";
            }

            CardinalDirection direction = CardinalDirections.getDirectionFromString(directionName);

            for (int column = 0; column < framesPerDirection; column++) {
                string subSpriteName = spriteRenderer.name + "_" + directionName + "_" + column.ToString();
                Rect offset = new Rect(column * size, row * size, size, size);
                Sprite sprite = Sprite.Create(texture, offset, new Vector2(0.5f, 0.5f), size, 1);
                Debug.Log("adding main sprite with name " + subSpriteName);
                sprite.name = subSpriteName;
                spriteKeyFrames[column] = new ObjectReferenceKeyframe();
                Debug.Log("Setting keyframe at time " + column);
                spriteKeyFrames[column].time = column / 60f;
                spriteKeyFrames[column].value = sprite;
            }

            AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);
            clip.name = sprite.name + "_" + directionName + "_animation";

            switch (direction) {
                case CardinalDirection.NORTH:
                    north = clip;
                    break;
                case CardinalDirection.SOUTH:
                    south = clip;
                    break;
                case CardinalDirection.EAST:
                    east = clip;
                    break;
                case CardinalDirection.WEST:
                    west = clip;
                    break;
                case CardinalDirection.UNKNOWN:
                default:
                    break;
            }
        }
    }

    protected abstract Vector2 getMovementDirection();

    void Update() {
        Vector2 directionVector = getMovementDirection();
        CardinalDirection direction = CardinalDirections.getDirectionFromVector(directionVector);

        switch (direction) {
            case CardinalDirection.NORTH:
                myAnimation.clip = north;
                myAnimation.Play();
                break;
            case CardinalDirection.SOUTH:
                myAnimation.clip = south;
                myAnimation.Play();
                break;
            case CardinalDirection.EAST:
                myAnimation.clip = east;
                myAnimation.Play();
                break;
            case CardinalDirection.WEST:
                myAnimation.clip = west;
                myAnimation.Play();
                break;
        }
    }
}
