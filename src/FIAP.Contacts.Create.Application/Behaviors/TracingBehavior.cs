using System.Diagnostics;
using MediatR;

namespace FIAP.Contacts.Create.Application.Behaviors;

public class TracingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ActivitySource _activitySource;

    public TracingBehavior(ActivitySource activitySource)
    {
        _activitySource = activitySource;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Captura a atividade atual (que foi iniciada no consumer)
        var parentActivity = Activity.Current;

        // Cria uma nova atividade filha para o processamento do mediator
        using var activity = _activitySource.StartActivity(
            $"Mediator: {typeof(TRequest).Name}",
            ActivityKind.Internal,
            parentActivity?.Context ?? default);

        try
        {
            // Adiciona contexto adicional
            activity?.AddTag("mediator.request.type", typeof(TRequest).Name);

            // Se o request tiver propriedades relevantes para o trace, você pode adicioná-las aqui
            if (request != null)
            {
                foreach (var prop in typeof(TRequest).GetProperties())
                {
                    var value = prop.GetValue(request)?.ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        activity?.AddTag($"mediator.request.{prop.Name}", value);
                    }
                }
            }

            // Executa o handler
            var response = await next();

            // Marca o span como bem-sucedido
            activity?.SetStatus(ActivityStatusCode.Ok);

            return response;
        }
        catch (Exception ex)
        {
            // Em caso de erro, marca o span como falha
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            throw;
        }
    }
}