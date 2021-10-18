using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;


[ScriptedImporter(1, "spritesheet")]
public class SpritesheetImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext context) {
        string spriteName = Path.GetFileNameWithoutExtension(context.assetPath);
        Texture2D texture = new Texture2D(0, 0);
        texture.name = spriteName + "_sheet";
        texture.LoadImage(File.ReadAllBytes(context.assetPath));
        context.AddObjectToAsset(spriteName + "_sheet", texture);

        int numberOfDirections = 4;
        int framesPerDirection = 4;
        float size = texture.width / framesPerDirection;

        Sprite main = Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size, 1);
        Debug.Log("adding main sprite with name " + spriteName);
        context.AddObjectToAsset(spriteName, main);
        context.SetMainObject(main);

        for (int row = 0; row < numberOfDirections; row++) {
            string direction = "north";

            if (row == 0) {
                direction = "south";
            } else if (row == 1) {
                direction = "west";
            } else if (row == 2) {
                direction = "east";
            } else if (row == 3) {
                direction = "north";
            }

            AnimationClip clip = new AnimationClip();
            ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[framesPerDirection];
            EditorCurveBinding spriteBinding = new EditorCurveBinding();
            spriteBinding.type = typeof(SpriteRenderer);
            spriteBinding.path = "";
            spriteBinding.propertyName = "sprite";

            for (int column = 0; column < framesPerDirection; column++) {
                string subSpriteName = spriteName + "_" + direction + "_" + column.ToString();
                Rect offset = new Rect(column * size, row * size, size, size);
                Sprite sprite = Sprite.Create(texture, offset, new Vector2(0.5f, 0.5f), size, 1);
                Debug.Log("adding main sprite with name " + subSpriteName);
                sprite.name = subSpriteName;
                context.AddObjectToAsset(subSpriteName, sprite);
                spriteKeyFrames[column] = new ObjectReferenceKeyframe();
                Debug.Log("Setting keyframe at time " + column);
                spriteKeyFrames[column].time = column / 60f;
                spriteKeyFrames[column].value = sprite;
            }

            AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, spriteKeyFrames);
            // context.AddObjectToAsset(spriteName + "_" + direction + "_animationBinding", spriteBinding);
            string clipName = spriteName + "_" + direction + "_animation";
            clip.name = clipName;
            Debug.Log(clip.length);
            context.AddObjectToAsset(clipName, clip);
        }
    }
}
