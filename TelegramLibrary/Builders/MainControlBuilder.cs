using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.Models.MainControls;

namespace TelegramLibrary.Builders
{
    public class MainControlBuilder : IMainControlBuilder
    {
        private ITelegramServiceBuilder _telegramServiceBuilder;
        private List<MainControlBase> _controls = new List<MainControlBase>();

        internal MainControlBuilder(ITelegramServiceBuilder telegramServiceBuilder)
        {
            this._telegramServiceBuilder = telegramServiceBuilder;
        }

        public ITelegramServiceBuilder SaveControls()
        {
            return _telegramServiceBuilder.SaveMainControls(_controls);
        }

        public IMainControlBuilder UseCommandControl(string command, EventHandler<ControlHandlingEventArgs> handler)
        {
            if (_controls.Any(control => (control as Command)?.CommandText == command))
            {
                throw new InvalidOperationException("This command has been already registered.");
            }
            var control = new Command(command);
            control.HandleEvent += handler;
            _controls.Add(control);
            return this;
        }
    }
}
