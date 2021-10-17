import argparse
import os

from PIL import Image


def collect_sprite_images(sprite_folder):
    sprite_images = []
    sprite_size = None
    
    for filepath in os.listdir(sprite_folder):
        if not filepath.endswith(".png"):
            continue
           
        sprite_image = Image.open(os.path.join(sprite_folder, filepath))
        
        if sprite_size is not None:
            if sprite_image.size != sprite_size:
                print("Sprite image at {0} size was {1} but expected {2}, ignoring".format(filepath, sprite_image.size, sprite_size))
                continue
        else:
            sprite_size = sprite_image.size

        sprite_images.append(sprite_image)
    
    return sprite_images, sprite_size


def combine_sprites(sprite_images, sprite_size):
    combined_size = (sprite_size[0], sprite_size[1] * len(sprite_images))
    combined_image = Image.new("RGB", combined_size)
    png_info = sprite_images[0].info
    
    for i, sprite_image in enumerate(sprite_images):
        sprite_offset = (0, i * sprite_size[1])
        combined_image.paste(sprite_image, sprite_offset)

    return combined_image, png_info


def save_image(combined_image, output_path, png_info):
    combined_image.save(output_path, "PNG")


def main(sprite_folder, output_path):
    sprite_images, sprite_size = collect_sprite_images(sprite_folder)
    combined_image, png_info = combine_sprites(sprite_images, sprite_size)
    save_image(combined_image, output_path, png_info)


if __name__ == "__main__":
    parser = argparse.ArgumentParser("Utility to combine spritesheets together into one mega image.")
    parser.add_argument("--sprite_folder", help="The location of the sprite folder to merge together", default="C:/Users/iwasf/Documents/game-development/open-world-pokemon-game/Unity/Open World Pokemon Game/Assets/Sprites/Pokemon/Overworld")
    parser.add_argument("--output_path", help="The filepath of the output combined sprite image", default="C:/Users/iwasf/Documents/game-development/open-world-pokemon-game/Unity/Open World Pokemon Game/Assets/Sprites/Pokemon/overworld-combined.png")
    args = parser.parse_args()
    main(args.sprite_folder, args.output_path)
