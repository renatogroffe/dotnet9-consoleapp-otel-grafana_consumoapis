# dotnet9-consoleapp-otel-grafana_consumoapis
Exemplo de Console App criada com o.NET 9 e utilizando distributed tracing com Grafana + OpenTelemetry (implementacao generica ou especifica do Grafana) para consumo de APIs REST. Para uso com ambientes empregados em testes de observabilidade e disponibilizados via Docker Compose.

Repositórios com os scripts + Docker Compose para a criação dos ambientes que farão uso do OpenTelemetry, PostgreSQL, MySQL e Redis:
- [Jaeger](https://github.com/renatogroffe/dockercompose-opentelemetry-jaeger-postgres-mysql-redis)
- [Grafana](https://github.com/renatogroffe/dockercompose-opentelemetry-grafana-postgres-mysql-redis)
- [Elastic APM](https://github.com/renatogroffe/dockercompose-opentelemetry-elasticapm-postgres-mysql-redis)
- [Zipkin](https://github.com/renatogroffe/dockercompose-opentelemetry-zipkin-postgres-mysql-redis)

Repositórios com as outras aplicações utilizadas nos testes com tracing distribuído:
- [API que acessa PostgreSQL, MySQL e Redis - .NET 9 + ASP.NET Core](https://github.com/renatogroffe/aspnetcore9-otel-grafana-postgres-mysql-redis_apicontagem)
- [API REST criada com Node.js](https://github.com/renatogroffe/nodejs-otel_apiconsumobackend)
- [API REST criada com Java + Spring + Apache Camel](https://github.com/renatogroffe/java-spring-camel_apiconsumobackend)
