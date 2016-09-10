/*Создание клиента*/
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcpClient
{
    internal class Program
    {
        public static void Main()
        {
            byte[] bytes = new byte[1024];
            // конечная точка подключения
            var ipHost = Dns.Resolve("localhost");
            var ipAddress = ipHost.AddressList[0];
            var ipEndPoint = new IPEndPoint(ipAddress, 9989);
            // создаём сокет
            var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // устанавливаем соединение
                sender.Connect(ipEndPoint);
                // отправляем сообщение
                const string msg = "Hello server!";
                var bytesSend = sender.Send(Encoding.ASCII.GetBytes(msg));
                // получаем ответ от сервера
                var bytesRec = sender.Receive(bytes);
                Console.WriteLine("Server:" + Encoding.ASCII.GetString(bytes, 0, bytesRec));
                // завершаем работу
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No connection:\n"+ex/*.Message*/);
            }
        }
    }
}
