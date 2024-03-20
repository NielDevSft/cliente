## Inicializar projeto
Para rodar o sistema em desenvolvimento é nescessário apenas executar `docker-compose up --build --remove-orphans -d` e estará disponibilizada na link [localhost](https://localhost:5003/index.html)

## Sobre

Este microserviço foi desenvolvido para funcionar em conjunto com a API de autenticação disponível em [app-account](https://github.com/NielDevSft/app-account). Futuramente, será integrado ao meu portfólio no projeto [portfolio-fullstack-microfrontend](https://github.com/NielDevSft/portfolio-fullstack-microfrontend), fazendo parte de um conjunto de microfrontends junto com outras aplicações. O frontend que atualmente implementa suas funcionalidades é o mesmo da aplicação [empresa](https://github.com/NielDevSft/empresa). Todas as operações são autenticadas com JWT e o sistema está sendo preparado para facilitar a escalabilidade no futuro.

## Integrações

- **API de Autenticação**: Este microserviço trabalha em conjunto com a API de autenticação fornecida em [app-account](https://github.com/NielDevSft/app-account).
- **Projeto Portfolio Fullstack Microfrontend**: Será integrado ao projeto [portfolio-fullstack-microfrontend](https://github.com/NielDevSft/portfolio-fullstack-microfrontend) como parte de um conjunto de microfrontends.
- **Frontend**: As funcionalidades deste microserviço são acessadas pelo frontend da aplicação [empresa](https://github.com/NielDevSft/empresa).

## Autenticação

Todas as operações neste microserviço são autenticadas utilizando JWT (JSON Web Tokens).

## Escalabilidade

O sistema está sendo projetado e implementado com foco na escalabilidade, facilitando futuras expansões e aumento da capacidade conforme necessário.
