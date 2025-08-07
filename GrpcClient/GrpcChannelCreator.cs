using Grpc.Net.Client;

namespace GrpcClient;

record ConnectionOptions(string Host, int Port);

internal static class GrpcChannelCreator
{
    public static GrpcChannel CreateChannel(ConnectionOptions connectionOptions)
    {
        var uri = new UriBuilder
        {
            Host = connectionOptions.Host,
            Port = connectionOptions.Port,
        }.Uri;
        return GrpcChannel.ForAddress(uri);
    }
}