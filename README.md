# TP04 - Sistemas Web II

## 📚 Sobre o projeto

Sistema de cadastro e gerenciamento de tarefas, desenvolvido como parte do Trabalho Prático 04 da disciplina de Sistemas Web II, utilizando:

- ASP.NET Core MVC
- .NET 10
- Entity Framework Core
- SQLite
- Bootstrap
- JavaScript (Fetch API)
- Swagger / OpenAPI
- xUnit

O projeto utiliza uma arquitetura organizada em três projetos: uma biblioteca de classes (DLL) para as regras de negócio, uma aplicação Web ASP.NET Core MVC e um projeto independente para testes automatizados.

## 💻 Requisitos

- Visual Studio 2026
- SDK .NET 10

## 🛠️ Executar

1. Abra `TP04.sln` no Visual Studio.
2. Restaure os pacotes NuGet.
3. Configure `TP04.Web` como projeto de inicialização.
4. Na primeira execução, crie ou atualize o banco de dados utilizando as migrations do Entity Framework Core:

   ```powershell
   dotnet ef database update --project TP04.Web --startup-project TP04.Web
   ```

5. Execute o projeto com `Ctrl + F5` ou `F5`.
6. A aplicação abrirá diretamente na página de gerenciamento de tarefas.

A aplicação utiliza SQLite para armazenar os dados. A documentação da API pode ser acessada pelo endereço `/swagger`.

## 🚀 Funcionalidades

- Cadastro de tarefas.
- Listagem das tarefas cadastradas na página inicial.
- Edição de tarefas existentes.
- Exclusão de tarefas com confirmação.
- Página de detalhes de cada tarefa.
- Cadastro de tarefas com título, descrição, data de vencimento, status e prioridade.
- Gerenciamento de status: Pendente, Em andamento e Concluída.
- Definição de prioridades: Baixa, Média e Alta.
- Pesquisa de tarefas por título e descrição.
- Filtro de tarefas por status.
- Indicadores com a quantidade total de tarefas e a distribuição por status.
- Validação dos campos e dos dados informados.
- Persistência dos dados utilizando Entity Framework Core e SQLite.
- API REST com operações CRUD.
- Interface Web integrada à API utilizando JavaScript e Fetch API.
- Documentação e testes dos endpoints por meio do Swagger.
- Testes automatizados utilizando xUnit.

## 🏗️ Estrutura principal

- `TP04.Domain/` — biblioteca de classes (DLL), contendo as entidades, enums e serviços com as regras de negócio.
- `TP04.Web/` — aplicação ASP.NET Core MVC responsável pela interface Web e pela API.
- `TP04.Web/Controllers/` — controllers responsáveis pela navegação das páginas e pelas requisições da API.
- `TP04.Web/Data/` — contexto do Entity Framework Core e configurações das entidades.
- `TP04.Web/Views/` — páginas Razor utilizadas na interface da aplicação.
- `TP04.Web/wwwroot/` — arquivos CSS, JavaScript e recursos estáticos da aplicação.
- `TP04.Web/Migrations/` — migrações do Entity Framework Core para criação e atualização da estrutura do banco.
- `TP04.Tests/` — projeto de testes automatizados utilizando xUnit.
- `Program.cs` — configuração dos serviços, banco de dados, Swagger e roteamento da aplicação.
- `appsettings.json` — configurações da aplicação e conexão com o banco de dados.

## 👥 Autores (Dupla)

- **Lucas Santos** — [GitHub/Lucas-Santos21](https://github.com/Lucas-Santos21)
- **Kaueh Farias** — [GitHub/KauehF](https://github.com/KauehF)

---

**Curso:** Análise e Desenvolvimento de Sistemas – ADS  
**Instituição:** Instituto Federal de São Paulo – IFSP  
**Campus:** Cubatão – SP  
**Disciplina:** Sistemas Web II  
**Trabalho:** Trabalho Prático 04 — Gerenciador de Tarefas com Web API
