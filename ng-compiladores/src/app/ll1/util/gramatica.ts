import { Gramatica } from '../modelos/garmatica';


export function criarGramatica(texto: string) {
    let cont = 1;

    let gramatica: Gramatica = new Gramatica();
    texto.split('\n').forEach(linha => {

        linha = linha.trim();
        if (linha.length == 0)
            return;

        let expressoes = linha.split('->');
        if (expressoes.length < 2)
            throw 'Expressão em formato incorreto. É esperado o seguinte formato: VARIAVEL -> EXPRESSAO';

        let variavel = expressoes[0].trim();
        let definicao = expressoes[1].trim();

        let partes = definicao.split('|');
        let partesIndexes = [];

        partes.forEach(parte => {
            let cadeias = parte.split(' ');
            let tokens = [];
            cadeias.forEach(token => {
                if (token.trim() === '') return;

                tokens.push({ texto: token, isVariavel: false });
            });

            gramatica.indices[cont] = { texto: parte, tokens: tokens, variavel: variavel };
            partesIndexes.push(cont);
            cont++;
        });

        gramatica.regras[variavel] = { texto: definicao, partes: partesIndexes, first: [], follow: [] };
        gramatica.variaveis.push(variavel);
    });

    gramatica.tamanho = cont - 1;

    // revisa os tokens para indicando se é variável ou terminal
    for (var i = 1; i < cont; i++) {
        for (var ii = 0; ii < gramatica.indices[i].tokens.length; ii++) {
            let token = gramatica.indices[i].tokens[ii].texto;
            // se o token está na lista de variáveis, atualiza lista de índices
            if (gramatica.variaveis.includes(token)) {
                gramatica.indices[i].tokens[ii].isVariavel = true;
            } else {
                // atualiza lista de terminais
                if (gramatica.indices[i].tokens[ii].texto != '%') {
                    gramatica.terminais = gramatica.terminais.concat(gramatica.indices[i].tokens[ii].texto);
                }
            }
        }
    }


    return gramatica;
}