using System.Text;
using Discord;
using Discord.WebSocket;
using System.Text.Json;

public class Program {
    private DiscordSocketClient? client;
    
    public static Task Main(string[] args) => new Program().MainAsync();
    
    public async Task MainAsync() {
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
    
    private Task Log(LogMessage msg) {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}