using System;
using System.Management.Automation;
using System.Net.Sockets;
using System.Threading;

namespace GreenHeronEverywareAutomation
{
    [Cmdlet(VerbsLifecycle.Invoke, "GHSwitch")]
    public class InvokeGHSwitch : PSCmdlet
    {
        [Parameter(Position = 1)]
        public string Server { get; set; }

        [Parameter(Position = 2)]
        public int Port { get; set; }

        [Parameter(Position = 3, ValueFromPipeline = true, Mandatory = true)]
        public string[] Commands { get; set; }

        private TcpClient socket;

        protected override void BeginProcessing()
        {
            ValidateParameters();

            this.socket = new TcpClient(this.Server, this.Port);
        }

        protected override void EndProcessing()
        {
            this.Cleanup();
        }

        protected override void StopProcessing()
        {
            this.Cleanup();
        }

        protected override void ProcessRecord()
        {
            using (var stream = socket.GetStream())
            {

                var reader = new System.IO.StreamReader(stream);
                var writer = new System.IO.StreamWriter(stream);
                char[] buffer = new char[1024];
                var encoding = new System.Text.ASCIIEncoding();

                try
                {
                    writer.AutoFlush = true;

                    while (socket.Connected)
                    {
                        if (socket.Connected)
                        {
                            for (int i = 0; i < this.Commands.Length; i++)
                            {
                                string command = this.Commands[i].Replace("||", new string((char)31, 1));
                                writer.WriteLine(command);
                                writer.Flush();
                                Thread.Sleep(100);
                            }
                        }

                        break;
                    }
                }
                finally
                {
                    reader.Close();
                    writer.Close();
                }
            }
        }

        private void ValidateParameters()
        {
            if (string.IsNullOrWhiteSpace(Server))
            {
                ThrowParameterError("Server");
            }

            if (Port == 0)
            {
                ThrowParameterError("Port");
            }
        }

        private void Cleanup()
        {
            if (socket != null)
            {
                socket.Close();
                socket = null;
            }
        }

        private void ThrowParameterError(string parameterName)
        {
            ThrowTerminatingError(
                new ErrorRecord(
                    new ArgumentException(string.Format(
                        "Must specify '{0}'", parameterName)),
                    Guid.NewGuid().ToString(),
                    ErrorCategory.InvalidArgument,
                    null));
        }
    }
}
