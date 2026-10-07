# Sistema de ordens

Sistema de envio e acumulação de ordens de ativos que calcula a exposição financeira por ativo e rejeita ordens que ultrapassem o limite de R$ 1.000.000.


## Tecnologias

- Linguagens: C# e TypeScript
- Backend: .NET 10, ASP.NET Core Web API, Entity Framework Core InMemory, FluentValidation, Swagger (Swashbuckle), xUnit
- Frontend: Angular

## Sobre o projeto

A solução tem duas aplicações:

- OrderAccumulator (backend): API REST em .NET que recebe ordens, calcula a exposição financeira por ativo e rejeita ordens que ultrapassem o limite.
- OrderGenerator (frontend): aplicação Angular com o formulário para envio das ordens e exibição da resposta da API.


## Pré-requisitos
- .NET SDK 10
- Node.js 20+
- Angular CLI: npm install -g @angular/cli

## Como executar
### backend
```
cd backend/src
dotnet restore
dotnet dev-certs https --trust
dotnet run --project OrderAccumulator.Api --urls https://localhost:7201
```


### frontend 
```
cd frontend
npm install
ng serve
```

Abrir url na porta: http://localhost:4200

   >  This is a challenge by [Coodesh](https://coodesh.com/)