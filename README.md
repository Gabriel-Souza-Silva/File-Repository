# File-Repository
File-Repository é um projeto desenvolvido para inclusão de arquivos diversos e gerenciar eles a partir de uma tela no navegador.

O projeto consiste em inclusões dos arquivos por uma API ou pela página contendo os dados do arquivo permitindo consultar-los e manipular-los.

- [Ferramentas Usadas](#ferramentas-usadas)
- [Como executar](#como-executar)

## Ferramentas Usadas

- IDE de Desenvolvimento: [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/)
- Framework para desenvolvimento da API: [.Net Core 3](https://dotnet.microsoft.com/download/dotnet-core/3.0)
- Framework para desenvolvimento do Frontend : [React JS](https://pt-br.reactjs.org/)

## Como Executar

- Abrir o projeto e selecionar a opção "Executar"(IIS Express) para que a IDE possa baixar as dependências. 

    URL Base: <p><code>https://localhost:44321/</code></p>

- Executar o Frontend
    
    Dentro da URL do projeto e com a API executando, execute o comando "yarn start" caso tenha yarn instalado, caso não tenha utilize "npm start" e a pagina web abrirá automaticamente.

## Erros

<p><code>200 OK</code> Tudo funcionou como esperado.</p>
<p><code>201 OK</code> O Recurso foi criado com sucesso.</p>
<p><code>400 Bad Request</code> Geralmente, um problema com os parâmetros.</p>
<p><code>404 Not Found</code> O recurso acessado não existe.</p>
<p><code>500 Server errors</code> Falha minha, algum erro no servidor.</p>

## Recursos

### Rotas

Permite busca de todos os arquivos
<p><code>GET https://localhost:44321/files/</code></p>

<p><strong>Exemplo de Resposta</strong></p>
<pre><code>[
  {
    "id": 1,
    "titulo": "Bob Esponja",
    "sinopse": "Esponja que frita hamburguer",
    "duracao": "20min",
    "ano": 2010,
    "tipodeMidia": 2,
    "direcao": "nickelodeon",
    "elenco": "Patrick",
    "nomeArquivo": "Bob Esponja.jpg"
  }
]
</code></pre>

Permite busca de um arquivo especifico a partir do ID
<p><code>GET https://localhost:44321/files/{ID}</code></p>

Permite busca de um arquivo utlizando as query string de titulo, ator e diretor
<p><code>GET https://localhost:44321/files/search</code></p>

Permite inclusao de um arquivo utlizando um MultiPart Form Data e mandando todas as propriedades abaixo:
 - files (arquivo)
 - Titulo (String)
 - Sinopse (String)
 - Duracao (String)
 - Ano (Inteiro)
 - TipodeMidia (Inteiro)
 - Direcao (String)
 - Elenco (String)
<p><code>POST https://localhost:44321/files</code></p>


<p><strong>Exemplo de Resposta</strong></p>
<pre><code>
{
    "id": 1,
    "titulo": "Bob Esponja",
    "sinopse": "Esponja que frita hamburguer",
    "duracao": "20min",
    "ano": 2010,
    "tipodeMidia": 2,
    "direcao": "nickelodeon",
    "elenco": "Patrick",
    "nomeArquivo": "Bob Esponja.jpg"
}

</code></pre>