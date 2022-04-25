using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary;

namespace Sample.Controllers
{
    [Route("telegram")]
    public class TelegramController : Controller
    {
        private readonly ITelegramService _telegramService;

        public TelegramController(ITelegramService telegramService)
        {
            this._telegramService = telegramService;
        }

        public IActionResult Up()
        {
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Index([FromBody] Update update)
        {
            await _telegramService.HandleUpdate(update);
            return Ok();
        }
    }
}
