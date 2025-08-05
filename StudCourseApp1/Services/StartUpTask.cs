// namespace StudCourseApp1;
//
// public class StartUpTask : IHostedService
// {
//     public readonly AuthService _authService;
//
//     public StartUpTask(AuthService authService)
//     {
//         _authService = authService;
//     }
//     
//     public Task StartAsync(CancellationToken cancellationToken)
//     {
//         string s =  _authService.createAdminAcc();
//         Console.WriteLine(s);
//     }
//
//     public Task StopAsync(CancellationToken cancellationToken)
//     {
//         throw new NotImplementedException();
//     }
// }