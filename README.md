# Cadastro Login

## Descrição
Este projeto é uma API em ASP.NET Core responsável pela gestão de usuários, permitindo operações como criação de contas, login e redefinição de senhas. Utiliza o padrão de projeto MVC e integra a biblioteca AutoMapper para facilitar o mapeamento de objetos entre DTOs e entidades do domínio.

## Funcionalidades
- **Registro de Usuário**: Cria novas contas de usuário.
- **Login de Usuário**: Autentica usuários e fornece um token de autenticação.
- **Redefinição de Senha**: Permite que usuários redefinam suas senhas de forma segura.

## Estrutura do Projeto

### Serviços

#### UserService
Gerencia as operações relacionadas a usuários, incluindo:
- **CreateAccount**: Cria um novo usuário a partir dos dados fornecidos e gerencia a persistência no banco de dados.
- **Login**: Valida as credenciais do usuário e gera um token JWT em caso de sucesso.
- **ResetPasswordAsync**: Permite que usuários redefinam suas senhas, validando a nova senha e a confirmação.

### Controladores

#### UserController
Expõe endpoints da API para as funcionalidades acima:
- **POST /user/register**: Endpoint para registrar um novo usuário.
- **POST /user/login**: Endpoint para realizar o login de um usuário.
- **POST /user/reset-password**: Endpoint para redefinir a senha do usuário, que requer autenticação.

## Requisitos
- .NET 6 ou superior
- Entity Framework Core para interação com o banco de dados
- ASP.NET Core Identity para gerenciamento de usuários
- AutoMapper para mapeamento de objetos



