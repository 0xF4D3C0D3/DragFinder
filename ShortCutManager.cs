﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Threading;
namespace DragFinder
{
    class ShortCutManager
    {

        public static void find()
        {
            var thread_find = new Thread(new ThreadStart(find_impl));
            thread_find.SetApartmentState(ApartmentState.STA);
            thread_find.Start();
        }
        private static void find_impl()
        {
            MenuForm menuForm = new MenuForm();
        }

        public static void exit()
        {
            Program.myForm.exit();
        }
    }
}
