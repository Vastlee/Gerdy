using Gerdy.App;

public class Program {
    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync() {
        await Bot.RunAsync();
    }
}
