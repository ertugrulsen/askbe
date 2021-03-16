using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Model.AskCommentModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace AskDefinex.Common.Extension
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ElasticConfiguration:url"];
            var defaultIndex = configuration["ElasticConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings
                .DefaultMappingFor<QuestionDetailModel>(m => m
                    .Ignore(p => p.Count)
                    .PropertyName(p => p.Id, "id")
                );

        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName, c => c
                .Settings(s => s
                    .Analysis(a => a
                        .CharFilters(cf => cf
                            .Mapping("programming_language", mcf => mcf
                                .Mappings(
                                    "c# => csharp",
                                    "C# => Csharp"
                                )
                            )
                        )
                        .Analyzers(an => an
                            .Custom("detail", ca => ca
                                .CharFilters("html_strip", "programming_language")
                                .Tokenizer("standard")
                                .Filters("standard", "lowercase", "stop")
                            )
                        )
                    )
                )
                .Map<QuestionDetailModel>(x => x
                        .AutoMap()
                        .Properties(p => p
                            .Text(t => t
                                .Name(n => n.Header)
                                .Analyzer("detail")
                                .Boost(3)
                            )
                            .Text(t => t
                                .Name(n => n.Detail)
                                .Analyzer("detail")
                                .Boost(1)
                            )
                            .Text(t => t
                                .Name(n => n.CreateDate)
                                .Boost(2)
                            )
                            .Text(t => t
                                .Name(n => n.DownVotes)
                                .Boost(2)
                            )
                            .Text(t => t
                                .Name(n => n.UpVotes)
                                .Boost(2)
                            )
                            .Text(t => t
                                .Name(n => n.CreateUser)
                                .Boost(2)
                            )
                            .Text(t => t
                                .Name(n => n.IsActive)
                                .Boost(2)
                            )
                            .Text(t => t
                                .Name(n => n.IsAccepted)
                                .Boost(2)
                            )
                            .Nested<AnswerDetailModel>(np => np
                                .AutoMap()
                                .Name(nn => nn.AnswerList)
                                .Properties(cp => cp
                                    .Text(t => t
                                        .Name(n => n.UserId)
                                        .Boost(0.6)
                                    )
                                    .Text(t => t
                                        .Name(n => n.Answer)
                                        .Boost(0.5)
                                    )
                                )
                            )
                            .Nested<CommentDetailModel>(np => np
                                .AutoMap()
                                .Name(nn => nn.CommentList)
                                .Properties(cp => cp
                                    .Text(t => t
                                        .Name(n => n.UserId)
                                        .Boost(0.6)
                                    )
                                    .Text(t => t
                                        .Name(n => n.Comment)
                                        .Boost(0.5)
                                    )
                                )
                            )
                        )
                    )
            );
        }
    }
}
