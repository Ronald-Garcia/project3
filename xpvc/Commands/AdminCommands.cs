using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace xpvc.Commands {
    public class AdminCommands : ModuleBase<SocketCommandContext> {

        /*
         * This Task is a command that, when used, will allow a user to make the bot send a message, assuming they
         * have correct permissions to do so, making sure the message does not contain @everyone and @here, and deleting
         * the command afterwards.
         */
        [Command("say")]
        public async Task say([Remainder] string text = "") {
            if (!(Context.User as SocketGuildUser).GuildPermissions.BanMembers && Context.User.Id != 347015092854063115) return;

            if (text.Contains("@everyone") || text.Contains("@here")) return;

            await Context.Message.DeleteAsync();
            await ReplyAsync(text);
        }

    }
}
