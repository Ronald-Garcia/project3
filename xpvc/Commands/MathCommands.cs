using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace xpvc.Commands {
    public class MathCommands : ModuleBase<SocketCommandContext> {

        /* 
         * This Task is a command that, when ran, will use the Law Of Cossines depending on the type given,
         * type being the type of triangle it is (sss, sas) and it will use the numbers and assume them to be 
         * the ones needed in each equation of each case
         */
        [Command("loc"), Alias("lawofcos")]
        public async Task loc(string type, int a, int b, int c) {
            if (type == "sss") {
                double num = Math.Pow(a, 2) + Math.Pow(c, 2) - Math.Pow(b, 2);
                double den = 2 * a * c;
                await ReplyAsync($"arccos({num}/{den}) = {Math.Acos(num / den) * 180.0 / Math.PI}");
            } else if (type == "sas") {
                double num = Math.Pow(Math.Pow(b, 2) + Math.Pow(c, 2) - 2 * b * c * (Math.Cos(a * Math.PI / 180)), 0.5);
                await ReplyAsync("" + num);
            }
        }

        /*
         * This Task is a command that, when ran, will perform the pythagorean theorom, depending on the type,
         * the type being which variable is the one to be solved for.
         */
        [Command("pythag")]
        public async Task pyth(string type, int a, int b) {
            if (type == "c") {
                await ReplyAsync($"{Math.Pow(Math.Pow(a, 2) + Math.Pow(b, 2), 0.5)}");
            } else {
                await ReplyAsync($"{Math.Pow(Math.Pow(b, 2) - Math.Pow(a, 2), 0.5)}");
            }
        }

        /*
         * This Task is a command that, when ran, will calculate the area of the specified triangle depending on
         * type, which in this case is which type of triangle it is.
         */
        [Command("triarea")]
        public async Task area(string type, double a, double b, double c) {
            if (type == "heron") {
                double s = 0.5 * (a + b + c);
                await ReplyAsync("" + Math.Pow(s * (s - a) * (s - b) * (s - c), 0.5));
            } else if (type == "right") {
                await ReplyAsync("" + a * b * 0.5);
            }
        }
        /*
         * This Task is a command that, when ran, will find if with the wall length provided
         * there could be a pattern on the wall that repeats every multiple for an even amount of times 
         * If not, loop through numbers above wallLength until there is one that works.
         */
        [Command("wall")]
        public async Task multiple(int wallLength, int multiple) {
            while (wallLength++ % multiple != 0) ;
            await ReplyAsync("" + wallLength);
        } 
    }
}
