/*Создание сервера*/
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcpProtocol
{
    internal class Program
    {
        public static void Main()
        {
            // создаём конечную локальную точку подключения
            var ipHost = Dns.GetHostEntry("localhost");
            var ipAddress = ipHost.AddressList[0];
            var ipEndPoint = new IPEndPoint(ipAddress, 9989);
            // создаём сокет
            try
            {
                var sListener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // связываем его с точкой подключения
                sListener.Bind(ipEndPoint);
                // запускаем прослушивание
                sListener.Listen(10);

                while (true)
                {
                    Console.WriteLine("Start connection");
                    // ожидаем подключения клиента
                    var handler = sListener.Accept();

                    var bytes = new byte[1024];
                    // принимаем от него сообщение
                    var bytesRec = handler.Receive(bytes);
                    var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    Console.WriteLine(data);
                    const string theReply = "Thanks!";
                    var msg = Encoding.ASCII.GetBytes(theReply);
                    // отправляем ответ
                    handler.Send(msg);
                    // и завершаем сеанс
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: " + ex.Message);
            }
        }
    }
}