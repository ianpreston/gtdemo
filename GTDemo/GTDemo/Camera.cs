using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GTDemo
{
    public class Camera
    {
        public Vector2 Position = new Vector2(0, 0);
        public Matrix Translation
        {
            get { return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)); }
        }

        /**
         * This method applies the opposite of the Camera's transformation to the given vector.
         * 
         * This can be used to convert a position that is relative to the screen (for example, mouse coordinates)
         * to a position relative to the camera location.
         * 
         * This can also be used to convert a position relative to the camera into a position relative
         * to the screen, for example, to draw GUI/HUD elements at an absolute screen position
         * regardless of the camera's position.
         */
        public Vector2 TransformRealPosition(Vector2 real)
        {
            return real + Position;
        }
    }
}
