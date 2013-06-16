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
    class LightningLine
    {
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }
    }

    public class Lightning
    {
        const int OFFSET_PER_DISTANCE = 6;

        Random rand = new Random();
        List<LightningLine> lineSegments = new List<LightningLine>();

        Texture2D texture;
        int numGenerations;

        public Lightning(Texture2D t, int ng)
        {
            texture = t;
            numGenerations = ng;
        }

        /**
         * Draw a line betweeen the specified two points, with the specified texture. If a maxLength is specified, the drawn line
         * will not be longer than maxLength, even if that means it will not reach `b'.
         */
        void DrawLine(SpriteBatch spriteBatch, Vector2 a, Vector2 b, int? maxLength=null)
        {
            float angle = (float)Math.Atan2(a.Y - b.Y, a.X - b.X);
            float distance = (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

            if (maxLength != null)
                distance = Math.Min(distance, (int)maxLength);

            spriteBatch.Draw(texture, new Rectangle((int)a.X, (int)a.Y, (int)distance, 1), null, Color.White, angle, new Vector2(1, 1), SpriteEffects.None, 0f);
        }

        Vector2 Midpoint(Vector2 a, Vector2 b)
        {
            return new Vector2((a.X + b.X) / 2, (a.Y + b.Y) / 2);
        }

        Vector2 Perpendicular(Vector2 v)
        {
            return new Vector2(v.X, 0 - v.Y);
        }

        LightningLine ShortenLine(LightningLine line, int maxLength)
        {
            // Determine the direction vector between the two points, and get the length
            // of that vector
            Vector2 direction = line.Start - line.End;
            float length = direction.Length();
            direction.Normalize();

            // Determine the difference between the length of the direction vector (the length
            // of the line), and the maximum allowed length
            var amtAboveMax = length - maxLength;

            // If the line is below the maximum length, no transformation is necessary
            if (amtAboveMax <= 0)
                return line;

            // Build a new vector, with blackjack and hookers
            Vector2 end = new Vector2(line.End.X + (direction.X * amtAboveMax), line.End.Y + (direction.Y * amtAboveMax));

            return new LightningLine() { Start = line.Start, End = end };
        }

        /**
         * Lightning renderer. This is an implementation of Drilian's excellent lightning code,
         * from http://drilian.com/2009/02/25/lightning-bolts/ .
         */
        void CalculateLightning(Vector2 a, Vector2 b, int maxLength)
        {
            List<LightningLine> segments = new List<LightningLine>();
            LightningLine initialLine = ShortenLine(new LightningLine() { Start = a, End = b }, maxLength);
            segments.Add(initialLine);

            float distance = (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
            int offsetAmount = (int)(distance / OFFSET_PER_DISTANCE);

            for (var i = 0; i < numGenerations; i++)
            {
                var newSegments = new List<LightningLine>();
                foreach (LightningLine s in segments)
                {
                    // Determine the midpoint of the line
                    var midpoint = Midpoint(s.Start, s.End);

                    // Generate a random offset for the midpoint
                    var delta = (s.Start - s.End);
                    delta.Normalize();
                    var perpendicular = Perpendicular(delta);
                    var r = rand.Next(-offsetAmount, offsetAmount);
                    var midpointOffset = new Vector2((float)(perpendicular.X * r), (float)(perpendicular.Y * r)); // TODO the way randomness is calculated and applied kind of sucks (?)

                    // Offset the midpoint by the generated offset
                    midpoint += midpointOffset;

                    // Create two lines -- one from start to the offset midpoint, and one from the offset midpoint to the
                    // end
                    LightningLine linea = new LightningLine() { Start = s.Start, End = midpoint };
                    LightningLine lineb = new LightningLine() { Start = midpoint, End = s.End };

                    newSegments.Add(linea);
                    newSegments.Add(lineb);
                }

                segments = newSegments;
                offsetAmount /= 2;
            }

            lineSegments = segments;
        }

        public void DrawLightning(SpriteBatch spriteBatch, Vector2 a, Vector2 b, int maxLength)
        {
            CalculateLightning(a, b, maxLength);
            foreach (LightningLine s in lineSegments)
            {
                DrawLine(spriteBatch, s.Start, s.End);
            }
        }
    }
}
