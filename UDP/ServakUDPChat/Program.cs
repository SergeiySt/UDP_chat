






using System.Net;
using System.Net.Sockets;
using System.Text;


//IPAddress localAddress = IPAddress.Parse("127.0.0.1");
IPAddress localAddress = IPAddress.Parse("25.39.201.3");


int localPort = 12975;

Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
socket.Bind(new IPEndPoint(localAddress, localPort));

byte[] buffer = new byte[1024];

Task receiveTask = ReceiveGameResult(socket);

//await ReceiveGameResult(socket);



Console.WriteLine("Введите свое имя: ");
string username = Console.ReadLine();

Console.WriteLine("Введите порт для отправки сообщений: ");
if (!int.TryParse(Console.ReadLine(), out int remotePort)) return;

await SendGameResult(socket, "join_game:" + username);

await PlayGame(socket, remotePort);

await receiveTask;
//Task.Run(ReciveMessageAsync);

//Task.Run(() => ReciveMessageAsync(localAddress, localPort));

////await SendMessageAsync();
//await SendMessageAsync(localAddress, remotePort, username);

//async Task SendMessageAsync()



async Task ReceiveGameResult(Socket socket)
{
    byte[] resultBytes = new byte[1024];
    await socket.ReceiveAsync(resultBytes, SocketFlags.None);
    string result = Encoding.UTF8.GetString(resultBytes).Trim();

    if (result == "win")
    {
        Console.WriteLine("Вы победили!");
    }
    else if (result == "lose")
    {
        Console.WriteLine("Вы проиграли.");
    }
}

//async Task SendMessageAsync(IPAddress localAddress, int remotePort, string username)
//{
//    using Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//    Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");


//    while (true)
//    {
//        var message = Console.ReadLine();

//        if (string.IsNullOrWhiteSpace(message)) break;

//        message = $"{username}: {message}";

//        byte[] data = Encoding.UTF8.GetBytes(message);

//        //await sender.SendToAsync(data, new IPEndPoint(localAddress, remotePort));
//        IPAddress serverIPAddress = IPAddress.Parse("158.120.16.16"); // Замените на IP-адрес Hamachi сервера
//        await sender.SendToAsync(data, new IPEndPoint(serverIPAddress, remotePort));
//    }
//}


//async Task ReciveMessageAsync()
//async Task ReciveMessageAsync(IPAddress localAddress, int localPort)
//{
//    using Socket reciver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//    reciver.Bind(new IPEndPoint(localAddress, localPort));

//    bool start = false; // Инициализируем переменную за пределами цикла

//    while (true)
//    {
//        byte[] data = new byte[1024];
//        var result = await reciver.ReceiveFromAsync(data, new IPEndPoint(IPAddress.Any, 0));
//        var message = Encoding.UTF8.GetString(data, 0, result.ReceivedBytes);

//        Console.WriteLine(message);

//        if (message.Contains("start_game") && !start)
//        {
//            Console.WriteLine("Игра началась! Угадайте число...");
//            start = true; // Устанавливаем флаг начала игры
//        }

//        if (message.Contains("join_game"))
//        {
//            Console.WriteLine($"Игрок {message.Split(':')[1]} подключился через Hamachi.");
//        }
//    }

//}

async Task SendGameResult(Socket socket, string result)
{
    byte[] resultBytes = Encoding.UTF8.GetBytes(result);
    await socket.SendToAsync(resultBytes, new IPEndPoint(localAddress, remotePort));
}

async Task PlayGame(Socket socket, int remotePort)
{
    while (true)
    {
        Console.WriteLine("Введите вашу попытку:");
        string guess = Console.ReadLine();

        byte[] guessBytes = Encoding.UTF8.GetBytes(guess);
        await socket.SendToAsync(guessBytes, new IPEndPoint(localAddress, remotePort));

        byte[] responseBytes = new byte[1024];
        await socket.ReceiveAsync(responseBytes, SocketFlags.None);
        string response = Encoding.UTF8.GetString(responseBytes).Trim();

        Console.WriteLine(response);

        if (response == "4 быка")
        {
            break;
        }
    }
}
