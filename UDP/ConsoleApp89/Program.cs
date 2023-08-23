
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

using var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

string message = "Hello World";

byte[] data = Encoding.UTF8.GetBytes(message);

EndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);

int bytes = await udpSocket.SendToAsync(data, remotePoint);