# 🚀 DevLearning - Plataforma de Ensino | Microservices Architecture

![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen)
![Platform](https://img.shields.io/badge/.NET-9.0-purple)
![Database](https://img.shields.io/badge/Databases-SQL_Server_/_MongoDB-blue)
![Architecture](https://img.shields.io/badge/Architecture-Clean_Architecture-orange)

> Projeto desenvolvido como entrega final do curso **Arquitetura de Software e Microsserviços** do [Balta.io](https://balta.io).  
O objetivo é fornecer uma API moderna, escalável e performática para uma plataforma de ensino completa — do catálogo aos estudantes.

---

## ✨ Principais Funcionalidades

A API gerencia os domínios:

| Domínio | Descrição |
|--------|----------|
| 🎓 **Students** | Perfil, matrículas e progresso |
| 📚 **Courses** | Catálogo de cursos, carga horária, nível |
| ✍️ **Authors** | Professores e criadores de conteúdo |
| 🏷️ **Categories** | Classificação do conteúdo (Backend, Mobile etc.) |
| 🚀 **Careers** | Trilhas compostas por múltiplos cursos |

> Aplicação totalmente desacoplada entre leitura e escrita, seguindo uma abordagem de **Polyglot Persistence**.

---

## 🛠️ Tecnologias & Arquitetura

### 🧩 Core
- **C# .NET 9.0**
- **ASP.NET Core Web API**

### 🗄️ Persistência
- **SQL Server** → Dados transacionais e relacionamentos
- **MongoDB** → Dados ricos e consultas rápidas
- **Dapper** → Micro-ORM para alta performance
- **MongoDB.Driver** → Acesso ao NoSQL

### 📐 Padrões Aplicados
- Repository Pattern
- Dependency Injection
- DTO + Mapping por Extension Methods
- Segregação de Contexto por Domínio

---

## 🏛️ Estrutura dos Bancos

| SQL Server (Relacional) | MongoDB (NoSQL) |
|------------------------|----------------|
| Author | Career / CareerItem |
| Category | Student / StudentCourse |
| — | Course |


> Cada tecnologia é utilizada no cenário onde oferece maior benefício.

---

## 🚀 Executando o Projeto

### 📌 Pré-requisitos
Antes de começar, instale:
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server (Local ou Docker)
- MongoDB (Local, Docker ou Atlas)
- VS Code ou Visual Studio 2022

---

### 📥 1️⃣ Clonar o Repositório
```bash
git clone https://github.com/juhfrancic/DevLearningMicroServicosAPI
cd DevLearningMicroServicosAPI
```

---

### 🔧 2️⃣ Configurar Ambiente

No `appsettings.json` da API:

```json
{
  "ConnectionStrings": {
    "SqlConnectionString": "Server=localhost;Database=DevLearning;User Id=sa;Password=sua_senha;TrustServerCertificate=True;",
    "MongoDb": "mongodb://localhost:27017"
  },
  "DatabaseName": "DevLearningMongoDb"
}
```

---

### 🗃️ 3️⃣ Criar Banco (SQL Server)

No SSMS ou Azure Data Studio:

> Executar o script em:  
📌 `/Docs/Scripts/CreateDatabase.sql`

---

### ▶️ 4️⃣ Rodar a aplicação

```bash
dotnet restore
dotnet build
dotnet run --project ./DevLearning.API/DevLearning.API.csproj
```

---

## 👥 Equipe de Desenvolvimento

| Integrante | GitHub |
|---|---|
| Integrante 1 | [@juhfrancic](https://github.com/juhfrancic) |
| Integrante 2 | [@GustavoRF1](https://github.com/GustavoRF1) |
| Integrante 3 | [@pedro-belarmino](https://github.com/pedro-belarmino) |
| Integrante 4 | [@krysgh](https://github.com/krysgh) |
| Integrante 5 | [@kihus](https://github.com/kihus) | 

---

> 🧠 Construído com 💜 durante os estudos no Balta.io  
> Feedbacks, PRs e ⭐ são muito bem-vindos!

