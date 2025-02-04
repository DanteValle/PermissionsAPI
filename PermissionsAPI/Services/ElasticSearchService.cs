using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using System;
using PermissionsAPI.Model;
using Nest;

namespace PermissionsAPI.Services
{
    public interface IElasticSearchService
    {
        Task IndexPermissionAsync(Permission permission);
    }

    public class ElasticSearchService : IElasticSearchService
    {
        private readonly ElasticClient _client;
        private const string IndexName = "permissions_index";

        public ElasticSearchService()
        {
            try
            {
                var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
               .DefaultIndex(IndexName);
                _client = new ElasticClient(settings);

                // Crear el índice si no existe
                var existsResponse = _client.Indices.Exists(IndexName);
                if (!existsResponse.Exists)
                {
                    _client.Indices.Create(IndexName, c => c
                        .Map<Permission>(m => m.AutoMap())
                    );
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task IndexPermissionAsync(Permission permission)
        {
            try
            {
                await _client.IndexDocumentAsync(permission);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
