namespace Decoy.Core.Utilities {
    using System;
    using UnityEngine;
    /// <summary>
    /// CustomSerializableObjects
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Convert From/To string. Usefull for sending through network or saving values in save game.
    /// </summary>
    public class CustomSerializableObjects {
        public static Vector2 Vector2FromString(string vector2String) {
            char[] charsToTrim = { '(', ')' };
            string[] temp = vector2String.Split(',');
            float x = Convert.ToSingle(temp[0].Trim(charsToTrim));
            float y = Convert.ToSingle(temp[1].Trim(charsToTrim));

            return new Vector2(x, y);
        }

        public static Vector3 Vector3FromString(string vector3String) {
            char[] charsToTrim = { '(', ')' };
            string[] temp = vector3String.Split(',');
            float x = Convert.ToSingle(temp[0].Trim(charsToTrim));
            float y = Convert.ToSingle(temp[1].Trim(charsToTrim));
            float z = Convert.ToSingle(temp[2].Trim(charsToTrim));

            return new Vector3(x, y, z);
        }

        public static Vector4 Vector4FromString(string vector4String) {
            char[] charsToTrim = { '(', ')' };
            string[] temp = vector4String.Split(',');
            float x = Convert.ToSingle(temp[0].Trim(charsToTrim));
            float y = Convert.ToSingle(temp[1].Trim(charsToTrim));
            float z = Convert.ToSingle(temp[2].Trim(charsToTrim));
            float w = Convert.ToSingle(temp[3].Trim(charsToTrim));

            return new Vector4(x, y, z, w);
        }

        public static Resolution GetResolutionFromString(string resolution) {
            string[] temp = resolution.Split(',');
            float height = Convert.ToSingle(temp[0]);
            float width = Convert.ToSingle(temp[1]);
            float refreshRate = Convert.ToSingle(temp[2]);

            Resolution res = new Resolution();
            res.height = (int)height;
            res.width = (int)width;
            res.refreshRate = (int)refreshRate;

            return res;
        }

        public static bool ToBoolean(string value) {
            switch (value.ToLower()) {
                case "true":
                case "t":
                case "1":
                    return true;
                case "0":
                case "false":
                case "f":
                    return false;
                default:
                    throw new InvalidCastException("You can't cast a weird value ToBoolean!");
            }
        }
    }
}
