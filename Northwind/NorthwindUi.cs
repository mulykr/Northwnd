﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwnd
{
    public class UiAction
    {
        public int Key { get; set; }
        public string Info { get; set; }
        public Func<string> Action { get; set; }

        public string Invoke()
        {
            return Action?.Invoke();
        }
    }

    public class NorthwndConsoleUi
    {
        private List<UiAction> _actions;
        private int _finishAction;
        public static string Separator = ".";

        public NorthwndConsoleUi()
        {
            _actions = new List<UiAction>();
            _finishAction = 1;
        }

        public void Add(Func<string> action, string info)
        {
            if (action != null)
            {
                var uiActuion = new UiAction { Key = _finishAction, Info = info, Action = action };
                _actions.Add(uiActuion);
                _finishAction++;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void Launch()
        {
            try
            {
                int key = -1;
                do
                {
                    ShowMenu();
                    key = int.Parse(Console.ReadLine());
                    if (_actions.Count(item => item.Key == key) != 0)
                    {
                        _actions.First(item => item.Key == key).Invoke();
                    }
                    else if (key == _finishAction)
                    {
                        Console.WriteLine("Bye ;)");
                    }
                    else
                    {
                        Console.WriteLine("Incorrect action...");
                    }
                }
                while (key != _finishAction);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Launch();
            }
        }

        private void ShowMenu()
        {
            foreach(var action in _actions)
            {
                Console.WriteLine($"{action.Key}{Separator} {action.Info}");
            }
            Console.WriteLine($"{_finishAction}{Separator} Exit");
        }
    }
}
