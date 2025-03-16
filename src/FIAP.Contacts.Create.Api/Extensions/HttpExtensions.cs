using System.Net;

namespace FIAP.Contacts.Create.Api.Extensions
{
    public static class HttpExtensions
    {
        public static bool IsSuccess(this HttpStatusCode statusCode) =>
            new HttpResponseMessage(statusCode).IsSuccessStatusCode;
    }
}
