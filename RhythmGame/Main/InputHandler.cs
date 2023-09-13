using Raylib_cs;
using RhythmGame.TextureBindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGame.Main
{
    public static class InputHandler
    {
        public static Dictionary<KeyboardKey, HitObject> bindingToObject = new Dictionary<KeyboardKey, HitObject>();

        static KeyboardKey[] keyboardKeys = { KeyboardKey.KEY_A, KeyboardKey.KEY_S, KeyboardKey.KEY_K, KeyboardKey.KEY_L };

        public static void SetBinds() {

           for(int i = 0; i < MainGame.baseCircleStatic.Count; i++) {

                bindingToObject.Add(keyboardKeys[i], MainGame.baseCircleStatic[i]);
            }

        }
    }
}
