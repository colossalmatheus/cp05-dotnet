# Banco API

## Visão Geral
Este projeto consiste em uma API RESTful desenvolvida em ASP.NET Core para gerenciar informações de bancos. A API permite operações CRUD (Criar, Ler, Atualizar e Deletar) em uma coleção de bancos, armazenando os dados em um banco de dados MongoDB.

## Funcionalidades Principais
- **Criação de Bancos**: Permite a criação de registros de bancos com nome e código.
- **Consulta de Bancos**: Recupera uma lista de todos os bancos ou um banco específico pelo ID.
- **Atualização de Bancos**: Atualiza as informações de um banco existente.
- **Exclusão de Bancos**: Remove um banco existente do sistema.
- **Validações**: Realiza validações no corpo da requisição, garantindo que os dados fornecidos estejam corretos.
- **Tratamento de Exceções**: Lida com possíveis erros de forma apropriada, retornando respostas adequadas ao cliente.
- **Swagger**: Inclui uma interface Swagger, facilitando a exploração e o teste da API diretamente no navegador.

## Estrutura do Projeto

- **Controllers/BancoController.cs**: Controlador responsável pelos endpoints relacionados à manipulação de bancos.
- **Interfaces/IBancoRepository.cs**: Interface que define o contrato para o repositório de bancos.
- **Models/BancoModel.cs**: Modelo que representa a estrutura de um banco na aplicação.
- **Repositories/BancoRepository.cs**: Implementação do repositório que interage com o MongoDB para manipulação dos dados dos bancos.
- **Program.cs**: Configuração da WebAPI, incluindo injeção de dependências e configuração do Swagger.

## Endpoints Disponíveis

### 1. POST /api/Banco
Adiciona um novo banco.

### 2. PUT /api/Banco/{id}
Atualiza um banco existente.

### 3. DELETE /api/Banco/{id}
Remove um banco existente.

### 4. GET /api/Banco
Retorna uma lista com todos os bancos cadastrados.

### 5. GET /api/banco/{id}
Retorna os dados de um banco específico pelo ID.

### 6. GET /swagger
Exibe a interface do Swagger para documentação e testes interativos.

### Executando Testes
Para rodar os testes automatizados do projeto, use o seguinte comando:
```
dotnet test
```

Equipe
Matheus Colossal Araujo (Desenvolvedor FullStack) - RM99572
