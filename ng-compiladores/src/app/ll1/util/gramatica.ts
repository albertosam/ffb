import { Gramatica } from '../modelos/gramatica';
import { getConjuntoFirst } from './first';
import { getConjuntoFollow } from './follow';


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

export function ehTerminal(gramatica: Gramatica, valor: string) {
    return gramatica.terminais.includes(valor);
}

export function montarTabelaPreditiva(gramatica: Gramatica) {
    let tabela: any[] = [];

    gramatica.variaveis.forEach((variavel) => {
        tabela[variavel] = [];

        gramatica.terminais.forEach((terminal) => {

            if (gramatica.regras[variavel].first.includes(terminal)) {
                let indice = buscarIndice(gramatica, variavel, terminal);
                tabela[variavel][terminal] = indice;
            } else if (gramatica.regras[variavel].follow.includes(terminal)) {
                if (gramatica.regras[variavel].first.includes('%')) {
                    let indice = buscarIndice(gramatica, variavel, '%');
                    tabela[variavel][terminal] = indice;
                }
            } else {
                tabela[variavel][terminal] = null;
            }

        });

    });

    gramatica.tabela = tabela;
}


export function buscarIndice(gramatica: Gramatica, variavel: string, terminal: string) {
    let saida: number;

    for (let i = 0; i < gramatica.regras[variavel].partes.length; i++) {
        let parte = gramatica.regras[variavel].partes[i];

        for (let j = 0; j < gramatica.indices[parte].tokens.length; j++) {
            let token = gramatica.indices[parte].tokens[j];
            if (!token.isVariavel) {
                if (token.texto == terminal) {
                    saida = parte;
                }
                break;
            } else {
                var primeiros = gramatica.regras[token.texto].first;
                if (primeiros.indexOf(terminal) !== -1) {
                    saida = parte;
                }
                break;
            }
        }
    }

    return saida;
}

export function identificarFirstFollow(gramatica: Gramatica) {
    gramatica.variaveis.forEach(variavel => {
        gramatica.regras[variavel].first = getConjuntoFirst(variavel, gramatica);

        let pilhaDeBusca = [];  // limpa pilha de variáveis buscadas
        gramatica.regras[variavel].follow = getConjuntoFollow(variavel, variavel, true, gramatica, pilhaDeBusca);
    });
}