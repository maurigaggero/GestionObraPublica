using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace GOP.Client.Services.Auth
{
    public class RenovadorToken : IDisposable
    {
        Timer timer;
        private readonly UserService _userService;

        public RenovadorToken(UserService userService)
        {
            this._userService = userService;
        }

        public void Iniciar()
        {
            timer = new Timer();
            timer.Interval = 1000 * 60 * 1; 
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _userService.ManejarRenovacionToken();
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
