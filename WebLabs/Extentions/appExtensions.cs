using WebLabs.Middleware;

namespace WebLabs.Extentions
{
	public static class appExtensions
	{
		public static IApplicationBuilder UseFileLogging(this IApplicationBuilder app) => app.UseMiddleware<LogMiddleware>();
	}
}
