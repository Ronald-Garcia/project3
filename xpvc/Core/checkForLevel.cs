using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;


namespace stuf.Core.lvl_up {
    internal static class checkForLevel {

        /*
         * This method is used to track the xp gained from users that are active in voice chat,
         * active meaning that they are not muted and that they are not alone in a voice chat
         * If a user is active, then add a random amount of xp to them for every message sent in a channel,
         * and if they leveled up, send the level up message in the pre-chosen channel.
         */
        internal static async void UserSentMessage(SocketGuildUser user, SocketGuild guild) {

            foreach (var channel in guild.VoiceChannels) {
                foreach (var usa in channel.Users) {
                    if (usa.IsDeafened || usa.IsMuted || usa.IsSelfDeafened || usa.IsSelfMuted || channel.Users.Count <= 1) 
                        continue;
                    else {
                        Random r = new Random();
                        uint x = (uint)r.Next(5, 20);
                        var User = UserAccounts.UserAccounts.GetAccount(usa);
                        uint oldLevel = User.Lvl;
                        User.XP += x;

                        if (oldLevel != User.Lvl) {
                            var embed = new EmbedBuilder();
                            embed.WithColor(Color.Magenta)
                                .WithTitle("LVL UP!")
                                .WithCurrentTimestamp()
                                .WithFooter(usa.Username, usa.GetAvatarUrl())
                                .AddInlineField("Current XP:", User.XP)
                                .WithDescription($"{usa.Mention} lvled up to {User.Lvl} from {oldLevel}");
                            await (guild.GetChannel(667577129348628491) as SocketTextChannel).SendMessageAsync("", false, embed.Build());
                        }


                    }
                }
            }
        }
    }
}
