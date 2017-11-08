using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Fun.Data;
using Fun.Extensions;

namespace Fun.Processes
{
    public class ObservableProcess<T> : PublisherBase<T>, IDisposable
    {
        private readonly Process _process;

        private readonly string _input;

        private readonly Func<Result<string>, Result<T>> _mapEvent;

        public ObservableProcess(
            string filename, 
            string input, 
            Func<Result<string>, Result<T>> mapEvent)
        {
            _input = input;
            _mapEvent = mapEvent;

            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = filename,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };
            
            _process.OutputDataReceived += (sender, args) => OnOutput(args.Data);
            _process.ErrorDataReceived += (sender, args) => OnError(args.Data);
        }

        public void Dispose()
        {
            foreach (var s in Subscribers)
                s.OnComplete();
            _process.Dispose();
        }

        public Task<Result<Unit>> Await() =>
            Result.TryAsync(() =>
            {
                _process.Start();
                
                using (var writer = _process.StandardInput)
                {
                    writer.WriteLine(_input);
                }

                _process.BeginOutputReadLine();
                _process.BeginErrorReadLine();

                _process.WaitForExit();
                Dispose();
            });

        private void OnOutput(string text)
        {
            var result = _mapEvent(Result.Value(text));

            foreach (var s in Subscribers)
                s.OnNext(result);
        }

        private void OnError(string text)
        {
            var result = _mapEvent(new Exception(text).AsError<string>());

            foreach (var s in Subscribers)
                s.OnNext(result);
        }
    }
}
