#openweathermap
-Descrição:
Esta aplicação se resume em uma API que ao subir também sobe uma task que fica rodando em background de 2 em 2 minutos consultando dados em uma outra API publica de informações metereológicas.
Os dados metereológicos das captais de Porto Alegre, Florianópolis e Curitiba são capturados e armazenados em memória enquanto a aplicação está rodando.

Os dados capturados podem ser capturados através de consultas na API REST passando o nome da cidade, data de início e data de fim através de parametros na URL.
Exemplo:
http://localhost:2241/Weather/ConsultWeatherByDateInterval?city=Porto%20Alegre&startDate=2021%2F10%2F06%2001%3A30%3A00&endDate=2021%2F10%2F07%2023%3A00%3A00

Possui também uma autorização via Bearer token, porém sem expiração:
http://localhost:2241/Token

Possui alguns testes automatizados que podem esclarecer os demais comportamentos e regras.