using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace GTDemo
{
    class LevelLoader
    {
        Level level;

        public LevelLoader(Level l)
        {
            level = l;
        }

        public List<CoreObject> LoadLevel(string filename)
        {
            string json = null;
            using (var sr = new StreamReader("Levels/level0.json"))
                json = sr.ReadToEnd();

            List<CoreObject> coreObjects = new List<CoreObject>();
            dynamic coreObjectDefs = JsonConvert.DeserializeObject<dynamic>(json).CoreObjects;

            foreach (var cod in coreObjectDefs)
            {
                coreObjects.Add(ParseCoreObjectDef(cod));
            }

            return coreObjects;
        }

        CoreObject ParseCoreObjectDef(dynamic cod)
        {
            if (cod.ClassName == "CStart")    return ParseCStart(cod);
            if (cod.ClassName == "CSwitch")   return ParseCSwitch(cod);
            if (cod.ClassName == "CPlatform") return ParseCPlatform(cod);
            return null;
        }

        CStart ParseCStart(dynamic cod)
        {
            Vector2 position = new Vector2(cod.PositionX.Value, cod.PositionY.Value);
            return new CStart(level, position);
        }

        CSwitch ParseCSwitch(dynamic cod)
        {
            Vector2 position = new Vector2(cod.PositionX.Value, cod.PositionY.Value);
            return new CSwitch(level, position);
        }

        CPlatform ParseCPlatform(dynamic cod)
        {
            int lb = (int)cod.LeftBound.Value;
            int rb = (int)cod.RightBound.Value;
            int tb = (int)cod.TopBound.Value;
            int bb = (int)cod.BottomBound.Value;
            return new CPlatform(level, lb, rb, tb, bb);
        }
    }
}
