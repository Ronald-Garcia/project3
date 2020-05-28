using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Questers;


namespace xpvc.Commands {
    public class RoleCommands : ModuleBase<SocketCommandContext> {

        /*
         * This Task is a command that, when ran, will generate a new role with the name and
         * color of the hexcode provided and will at it to the user
         */
        [Command("^^")]
        public async Task getColorRole(string hexcode) {
            uint hexCode = 0;
            SocketGuildUser user = (SocketGuildUser)Context.User;
            if (hexcode[0] == '#')
                hexCode = uint.Parse(hexcode.Substring(1));
            else
                hexCode = uint.Parse(hexcode);

            var r = await Context.Guild.CreateRoleAsync(hexcode.Substring(1), color: new Discord.Color(hexCode));
            await user.AddRoleAsync(r);
        }
        /*
         * This Task is a command that, when ran, will send the name of every role in the server
         * seperated by a \n, excluding the role named @everyone
         */
        [Command("toy")]
        public async Task getRoleNames() {
            string s = "";
            foreach (var role in Context.Guild.Roles)
                if (role.Name != "@everyone") s += role.Name + "\n";
            await ReplyAsync(s);
        }
    }
}
