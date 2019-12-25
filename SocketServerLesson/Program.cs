using System;
using System.Net;
using System.Net.Sockets;

namespace SocketServerLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var localIp = IPAddress.Parse("127.0.0.1");
                var port = 3231;
                var endPoint = new IPEndPoint(localIp, port);
                socket.Bind(endPoint);
                socket.Listen(5); // макс число соединений в очереди
                Console.WriteLine($"Приложение слушает порт {port}.");
                while (true)
                {
                    var incomingSocket = socket.Accept(); // блокирует поток, пока не получит нужное соединение
                    Console.WriteLine($"Получено входящее сообщение.");
                    while (incomingSocket.Available > 0)
                    {
                        var buffer = new byte[incomingSocket.Available];
                        incomingSocket.Receive(buffer);
                        Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));
                    }
                    incomingSocket.Close();
                    Console.WriteLine($"Входящее соединение закрыто.");
                }                
            }
        }
    }
}
