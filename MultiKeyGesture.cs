using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsInput;

namespace DragFinder
{
    public class MultiKeyGesture
    {
        public static HashSet<Keys> currentKeyState = new HashSet<Keys>();
        public static bool bMatched = false;
        public static HashSet<Keys> matchedKeys;

        public static event Action keyUpEvent;

        public static String keyState()
        {
            String res = "";
            foreach (var i in currentKeyState)
            {
                res += i.ToString() + " ";
            }
            return res;
        }

        public static void keyDown(Keys key)
        {
            currentKeyState.Add(key);
        }

        public static void keyUp(Keys key)
        {
            currentKeyState.Remove(key);
            if (keyUpEvent != null)
                keyUpEvent();
            if (currentKeyState.Count == 0)
                bMatched = false;
        }

        public static bool keyMatch(HashSet<Keys> keys, bool bKeyUp = false)
        {
            if (currentKeyState != null && HashSet<Keys>.CreateSetComparer().Equals(keys, currentKeyState))
            {
                matchedKeys = keys;
                bMatched = true;
                return true;
            }
            return false;
        }

        public static void sendKeyAvoidHook(string keys)
        {
            Hook.bSendKey += MultiKeyGesture.matchedKeys.Count + keys.Length * 2;

            foreach (var key in MultiKeyGesture.matchedKeys.Reverse())
                InputSimulator.SimulateKeyUp((VirtualKeyCode)key);

            SendKeys.SendWait(keys);
        }

    }
}
