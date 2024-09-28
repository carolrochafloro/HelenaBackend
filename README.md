# Helena

Esse projeto nasceu da necessidade de articular vários medicamentos que minha avó precisa tomar, prescritos por diversas médicas. Como várias pessoas da família ajudam nos cuidados com dona Maria Helena, decidi encontrar uma forma de facilitar a visualização dos horários de cada remédio e a comunicação com as médicas para evitar interações medicamentosas indesejadas.  

O back-end vai ser uma API REST desenvolvida utilizando ASP.NET e o banco de dados será o PostgreSQL. 

## MVP
A primeira versão terá:
- Tela de cadastro e login;
- Tela inicial com visualização dos remédios ativos;
- Função de adicionar, editar ou remover remédio;
- Tela de visualização de todos os remédios cadastrados.

Claro! Aqui está uma explicação para o README.md sobre como rodar o projeto usando Docker e Docker Compose:

---

## Como Rodar o Projeto

Este projeto utiliza Docker e Docker Compose para facilitar a configuração e execução do ambiente de desenvolvimento. Siga os passos abaixo para rodar o projeto localmente.

### Pré-requisitos

- [Docker](https://www.docker.com/get-started) instalado
- [Docker Compose](https://docs.docker.com/compose/install/) instalado

### Passos para Rodar o Projeto

1. **Clone o Repositório**

   Clone o repositório para sua máquina local usando o comando:

   ```bash
   git clone https://github.com/carolrochafloro/HelenaBackend
   cd HelenaBackend
   ```

2. **Configurar Variáveis de Ambiente**

   Crie um arquivo `.env` na raiz do projeto e adicione as variáveis de ambiente conforme o arquivo .env.example:

3. **Construir e Iniciar os Contêineres**

   Use o Docker Compose para construir e iniciar os contêineres:

   ```bash
   docker-compose up --build
   ```

   Este comando irá:
   - Construir a imagem Docker para a aplicação.
   - Iniciar um contêiner para a aplicação (`helena-api`).
   - Iniciar um contêiner para o banco de dados PostgreSQL (`db`).

4. **Acessar a Aplicação**

   Após a construção e inicialização dos contêineres, a aplicação estará disponível nos seguintes endereços:
   - `http://localhost:8080`
   - `http://localhost:8081`

### Parar os Contêineres

Para parar os contêineres, use o comando:

```bash
docker-compose down
```

### Limpar Recursos

Para remover os volumes e limpar os recursos utilizados pelos contêineres, use o comando:

```bash
docker-compose down -v
```

---
