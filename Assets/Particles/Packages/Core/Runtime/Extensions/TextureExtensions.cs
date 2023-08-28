using UnityEngine;

namespace Particles.Packages.Core.Runtime.Extensions
{
    public static class TextureExtension
    {
        /// <summary>
        /// Samples a texture at a given point by normalizing it against the size.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="point"></param>
        /// <param name="size"></param>
        /// <returns>Filtered pixel color</returns>
        public static float SampleTexturePosition(this Texture2D texture, Vector3 point, Vector3 size, TextureChannel channel = TextureChannel.A)
        {
            float textureX = Mathf.Clamp01(point.x / size.x);
            float textureY = Mathf.Clamp01(point.y / size.y);

            var pixelColor = channel switch
            {
                TextureChannel.R => texture.GetPixelBilinear(textureX, textureY).r,
                TextureChannel.G => texture.GetPixelBilinear(textureX, textureY).g,
                TextureChannel.B => texture.GetPixelBilinear(textureX, textureY).b,
                TextureChannel.A => texture.GetPixelBilinear(textureX, textureY).a,
                _ => texture.GetPixelBilinear(textureX, textureY).a,
            };

            return pixelColor;
        }
    }
}