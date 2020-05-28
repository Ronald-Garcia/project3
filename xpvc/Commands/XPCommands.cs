using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using stuf.Core.UserAccounts;

namespace stuf.Commands {
    public class inforole : ModuleBase<SocketCommandContext> {
        /*
         * This Task is a command that, when ran, will send the user ID,
         * XP, and the day they joined discord and joined the server, as well as other information
         */
        [Command("whois"), Alias("info")]
        public async Task info(SocketGuildUser user = null) {
           
            if (user == null) user = Context.User as SocketGuildUser;
            
            var User = UserAccounts.GetAccount(user as SocketUser);
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithTitle($"{user.Nickname} info:")
                .AddInlineField("Bot stats", $"**XP:** {User.XP}\n **Level:** {User.Lvl}")
                .WithDescription($"**User id:** {user.Id}\n" +
                    $"**Joined Discord:** {user.CreatedAt}\n" +
                    $"**Joined Server:** {user.JoinedAt}\n" +
                    $"Status: **{user.Status}**\n" +
                    $"Is playing **{user.Game}**")
                .WithColor(Color.Blue)
                .WithFooter($"{Context.Guild.Name}", Context.Guild.Owner.GetAvatarUrl())
                .WithCurrentTimestamp()
                .WithThumbnailUrl(user.GetAvatarUrl());
            await Context.Channel.SendMessageAsync("", false, Embed.Build());

        }
    }
}
    

