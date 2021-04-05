﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.Models.WindowControls;

namespace TelegramLibrary.Builders
{
    public class MessageCallbackControlBuilder : IMessageCallbackControlBuilder
    {
        private IMessagePropertiesSaver _telegramMessageBuilder;
        private List<List<IPositionalControl>> _positionalControls = new List<List<IPositionalControl>>();

        internal MessageCallbackControlBuilder(IMessagePropertiesSaver telegramMessageBuilder)
        {
            this._telegramMessageBuilder = telegramMessageBuilder;
        }

        public IMessageCallbackControlBuilder CreateRow()
        {
            _positionalControls.Add(new List<IPositionalControl>());
            return this;
        }

        public IMessageCallbackControlBuilder UseCallbackButtonControl(string text, string data, EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null)
        {
            var control = new CallbackButton(text, data);
            control.HandleEvent += handler;
            _positionalControls.Last().Add(control);

            if (limiterDelay.HasValue)
            {
                (control as ILimitable).Limiter = new ConnectionLimiter(limiterDelay.Value);
            }

            return this;
        }

        public IMessageBuilder SaveControls()
        {
            return this._telegramMessageBuilder.SaveControls(_positionalControls);
        }
    }
}
