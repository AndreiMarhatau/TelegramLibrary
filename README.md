# TelegramLibrary
This is one more library for creating Telegram bots.

But with this you don't have to write tons of code for it.

The idea is simple: you create windows where users can switch between them using the way you specified. Windows can send messages with text, buttons, etc as default.

All controls (text, command, click on button, etc.) by default are dedicated between the windows so that if user tries to use one button in other window - it will be useless. Or not, it's up to you.

And for all controls you can create any handler for any action you want to be done using .NET!

## Sample
There is a sample project inside the library where you can find initial configuration of the ASP .NET Core project for the Telegram bot.

## Further notice
The library is in development stage and has a lack of functionality, but you have freedom in creating your bots.

For that in event args in any handler you have TelegramInteractor property and the TelegramBotClient inside of it.
