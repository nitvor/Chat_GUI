using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chat_GUI
{
    public class Listener
    {
        private Controller _controller;
        private StreamReader _inputStream;
        private StreamWriter _outputStream;
        private Thread _receiveThread;
        private TcpClient _tcpClient;

        public Listener(Controller controller)
        {
            this._controller = controller;
            this._tcpClient = new TcpClient("localhost", 4711);
            this._outputStream = new StreamWriter(this._tcpClient.GetStream());
            this._inputStream = new StreamReader(this._tcpClient.GetStream());
            this._receiveThread = new Thread(new ThreadStart(this.receive));
            this._receiveThread.IsBackground = true;
            this._receiveThread.Start();
        }

        public void Send(XElement element)
        {
            try
            {
                this._outputStream.WriteLine(element);
                this._outputStream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void receive()
        {
            while (true)
            {
                    string str = this._inputStream.ReadLine();
                    string tmp = "";
                    string pattern = @"\b</\b";
                    if (str != null) {
                        if (str!=null && Regex.Matches(str, pattern).Count == 0 && !str.EndsWith("/>"))
                        {
                            while (!tmp.StartsWith("</"))
                            {
                                tmp = this._inputStream.ReadLine();
                                str += tmp;
                            }
                        }
                        XElement xe = XElement.Parse(str);
                        this._controller.Receive(xe);
                   }
            }
        }
    }
}
