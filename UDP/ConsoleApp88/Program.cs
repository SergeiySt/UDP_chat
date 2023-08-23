


using System.Net;
using System.Net.Sockets;
using System.Text;

using var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


//var localIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);

//var localIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 5555);
var localIP = new IPEndPoint(IPAddress.Parse("25.39.201.3"), 12975);


udpSocket.Bind(localIP);
Console.WriteLine("UDP-сервер запущен и использует Hamachi...");

byte[] buffer = new byte[1024];

EndPoint remoteIP = new IPEndPoint(IPAddress.Any, 0);

var result = await udpSocket.ReceiveFromAsync(buffer, remoteIP);
var message = Encoding.UTF8.GetString(buffer, 0, result.ReceivedBytes);

Console.WriteLine($"Получено {result.ReceivedBytes} байт");
//Console.WriteLine($"Удаленный адрес {result.RemoteEndPoint}");
Console.WriteLine($"Удаленный адрес {remoteIP}");



