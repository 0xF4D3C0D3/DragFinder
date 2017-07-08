using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using WindowsInput;

namespace DragFinder
{
    class Hook
    {
        public static int bSendKey = 0;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private static LowLevelKeyboardProc _proc = HookCallBack;
        private static IntPtr _hookID = IntPtr.Zero;

        private static List<KeyValuePair<HashSet<Keys>, String>> multiKeyGestureList = new List<KeyValuePair<HashSet<Keys>, String>>
        {
            new KeyValuePair<HashSet<Keys>, string>(new HashSet<Keys>{Keys.LWin, Keys.F}, "Find"),
            new KeyValuePair<HashSet<Keys>, string>(new HashSet<Keys>{Keys.LWin, Keys.T}, "Translate"),
            new KeyValuePair<HashSet<Keys>, string>(new HashSet<Keys>{Keys.LWin, Keys.Escape}, "Exit")
        };

        private static bool keyMapping(Keys key, bool peek = false)
        {
            if (MultiKeyGesture.bMatched)
                return true;

            foreach (var shortcut in multiKeyGestureList)
            {
                if (MultiKeyGesture.keyMatch(shortcut.Key, peek))
                {
                    if (!peek)
                    {
                        switch (shortcut.Value)
                        {
                            case "Find":
                                ShortCutManager.find();
                                break;

                            case "Translate":
                                ShortCutManager.translate();
                                break;

                            case "Exit":
                                ShortCutManager.exit();
                                break;

                            case "Test":
                                Console.WriteLine("Test Line");
                                break;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private static IntPtr HookCallBack(int nCode, IntPtr wParam, IntPtr lParam)
        {
            IntPtr res = CallNextHookEx(_hookID, nCode, wParam, lParam);
            Keys vkCode = (Keys)Marshal.ReadInt32(lParam);

            if (nCode >= 0 && bSendKey > 0)
            {
                Console.WriteLine(bSendKey.ToString() + " STATE : " + MultiKeyGesture.keyState() + " -> " + vkCode.ToString());
                bSendKey--;
                return res;
            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                MultiKeyGesture.keyDown(vkCode);
                if (keyMapping(vkCode))
                    res = (IntPtr)1;
            }

            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYUP))
            {
                MultiKeyGesture.keyUp(vkCode);
            }
            return res;
        }

        public static void HookStart()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                _hookID = SetWindowsHookEx(13, _proc, GetModuleHandle(curModule.ModuleName), 0);
            }

        }

        public static void HookEnd()
        {
            UnhookWindowsHookEx(_hookID);
        }

        public static string getSelection()
        {
            try
            {
                string prevClipboardText = Clipboard.GetText();
                MultiKeyGesture.sendKeyAvoidHook("^c");
                Thread.Sleep(100);

                string selectedText = Clipboard.GetText();
                selectedText = selectedText.Trim();
                selectedText = selectedText.Replace(Environment.NewLine, " ");

                Clipboard.SetText(prevClipboardText);
                if (prevClipboardText == selectedText)
                    return "";
                else
                    return selectedText;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
