using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResearchersXamarin.UI.Common
{
    public class CommandBase : Command
    {
        public CommandBase(Action<object> execute)
            : base(execute)
        {
        }

        public CommandBase(Action execute)
            : this(o => execute())
        {
        }

        public CommandBase(Action<object> execute, Func<object, bool> canExecute, INotifyPropertyChanged npc = null)
            : base(execute, canExecute)
        {
            if (npc != null)
                npc.PropertyChanged += delegate { ChangeCanExecute(); };
        }

        public CommandBase(Action execute, Func<bool> canExecute, INotifyPropertyChanged npc = null)
            : this(o => execute(), o => canExecute(), npc)
        {
        }
    }
}