using UnityEditor;
using UnityEngine;


public class MovableCharacter : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Sprite sprite;
    private AnimationClip clip;

    public int framesPerDirection = 4;
    public int numberOfDirections = 4;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
        LoadAnimationClip();
    }

    void LoadAnimationClip() {
        clip = new AnimationClip();
        ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[framesPerDirection];
        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        Texture2D texture = sprite.texture;
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "sprite";

        float size = texture.width / framesPerDirection;

        for (int row = 0; row < numberOfDirections; row++) {
            string direction = "north";

            if (row == 0) {
                direction = "south";
            }
            else if (row == 1) {
                direction = "west";
            }
            else if (row == 2) {
                direction = "east";
            }
            else if (row == 3) {
                direction = "north";
            }

            for (int column = 0; column < framesPerDirection; column++) {
                string subSpriteName = spriteRenderer.name + "_" + direction + "_" + column.ToString();
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
            // context.AddObjectToAsset(spriteName + "_" + direction + "_animationBinding", spriteBinding);
            string clipName = sprite.name + "_" + direction + "_animation";
            clip.name = clipName;
        }


    }

    protected virtual Vector2 getMovementDirection() {
        return Vector2.zero;
    }

    void Update() {

    }
}