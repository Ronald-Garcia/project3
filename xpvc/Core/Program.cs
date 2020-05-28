using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MashedPotato {
    class Program : ModuleBase<SocketCommandContext> {
        //INSTANCE VARIABLES
        public static DiscordSocketClient _client;
        CommandService _commands;
        
        /*
         * This is the method Main method of the program, and when run it generates and loads up the bot
         */
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        /*
         * This Task is used to load up the bot from the token provided token, and initialize sll of the instance variables
         */
        async Task MainAsync() {

            _client = new DiscordSocketClient(new DiscordSocketConfig {
                LogLevel = LogSeverity.Debug
            });

            _commands = new CommandService(new CommandServiceConfig {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            _client.MessageReceived += ClientMessageReceived;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.Ready += ClientReady;
            _client.Log += ClientLog;

            string token = "token";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        /*
         * This Task is used to log every time a message has been sent in a channel
         */
        async Task ClientLog(LogMessage pessage) {
            Console.WriteLine($"{DateTime.Now} at {pessage.Source}] {pessage.Message}");
            
        }

        /*
         * This Task is used to initialize the _client's statuses, online and game status
         */
        async Task ClientReady() {
            await _client.SetGameAsync("test");
            await _client.SetStatusAsync(UserStatus.Online);
        }

        /*
         * This Task is ran whenever a message is sent, and handles all the arguments and cases for messages, such as checking 
         * if it is a command
         */
        async Task ClientMessageReceived(SocketMessage message) {
            var Message = message as SocketUserMessage;
            var Context = new SocketCommandContext(_client, Message);
            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;
            int argPos = 0;
            
            if (!(Message.HasStringPrefix("%", ref argPos) || !Message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
            var Result = await _commands.ExecuteAsync(Context, argPos);
            if (!Result.IsSuccess)
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");

        }
    }
}