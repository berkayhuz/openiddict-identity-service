// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🚦 RateLimitingMiddleware – Global Request Throttler

/*
 * A lightweight middleware that applies global request rate limiting.
 * 
 * How it works:
 *   🔹 Uses System.Threading.RateLimiting.RateLimiter
 *   🔹 If the lease is acquired, the request proceeds
 *   🔹 If not, responds with HTTP 429 Too Many Requests
 * 
 * Apply this middleware early in the pipeline to protect API from abuse.
 */

using System.Threading.RateLimiting;

namespace IdentityService.Web.Middlewares;

internal class RateLimitingMiddleware(RequestDelegate _next, RateLimiter _rateLimiter)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var lease = await _rateLimiter.AcquireAsync(1).ConfigureAwait(false);

        if (lease.IsAcquired)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            finally
            {
                lease.Dispose();
            }
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            context.Response.ContentType = "text/plain";

            await context.Response
                .WriteAsync("Too many requests. Please slow down.")
                .ConfigureAwait(false);
        }
    }
}

#endregion
