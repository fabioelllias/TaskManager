#Fase 1: API Coding


Regras de negócio: https://meteor-ocelot-f0d.notion.site/NET-C-5281edbec2e4480d98552e5ca0242c5b

A aplicação faz uso de containers docker.

[Instruções para instalar o docker em ambiente Linux]
    (https://docs.docker.com/engine/install/ubuntu/)

[Instruções para instalar o docker em ambiente Windows]
    https://docs.docker.com/desktop/setup/install/windows-install/

Localização do Dockerfile:      TaskManager.API\Dockerfile
Localização do docker-compose:  docker-compose.yml


#Fase 2: Refinamento


Tendo em vista futuras sprints

Para refinar a questão do desempenho, seria importante avaliar que outros critérios
poderiam ser adicionados, tais como tempo médio de execução, complexidade e outros fatores correlatos.

Há abertura para inclusão de gamificação, como forma de estimular os desenvolvedores a aumentar a produtividade?

Existe cenários onde algumas tarefas possam ser realizadas em pares? Ou mesmo transferidas para outro desenvolvedor?



#Fase 3: Final


Melhorias sugeridas:

- Implementar Health Checks para monitoramento da saúde das APIs
- Implementar Log e Audtoria
- Implementar o pattern Mediator para minimizar o acoplamento da camada das APIs
- Implementar testes de integração
- Implementar politica de retentativas as chamadas http 
- Disponibilizar a aplicação em ambiente cloud

Link do Projeto
https://github.com/fabioelllias/TaskManager