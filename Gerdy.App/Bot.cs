using System.Text;
using Discord;
using Discord.WebSocket;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gerdy.App;
internal class Bot {
    static DiscordSocketClient? client;

    static public async Task RunAsync() {
        const string configFileName = "GerdyConfig.json";
        string botConfigFile = string.Empty;

        using(FileStream fs = File.OpenRead($"{configFileName}")) {
            using var sr = new StreamReader(fs, new UTF8Encoding(false));
            botConfigFile = await sr.ReadToEndAsync().ConfigureAwait(false);
        }

        DiscordBotConfig tokenFile = JsonSerializer.Deserialize<DiscordBotConfig>(botConfigFile);

        string token = tokenFile.Token;
        
        client = new DiscordSocketClient();
        client.Log += Log;

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
        await Task.Delay(-1);
    }
    
    static Task Log(LogMessage msg) {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}
