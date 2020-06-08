import { getConjuntoFollow } from '../util/follow';
import { getConjuntoFirst } from '../util/first';

export interface Token {
    isVariavel: boolean;
    texto: string;
}

export interface GramaticaRegra {
    texto: string;
    partes: number[];
    first: string[];
    follow: string[];
}

export interface Gramatica {
    variaveis: string[];
    terminais: string[];
    regras: { [key: string]: GramaticaRegra };
    indices: Indice[]
    tamanho: number;
    tabela: any[];
}

export class Gramatica implements Gramatica {

    variaveis: string[];
    terminais: string[];
    regras: { [key: string]: GramaticaRegra };
    indices: Indice[]
    tamanho: number;
    tabela: any[];

    constructor() {
        this.variaveis = [];
        this.terminais = ['$'];
        this.regras = {};
        this.indices = [];
        this.tamanho = 0;
        this.tabela = [];
    }

    buscarIndice(variavel: string, terminal: string) {
        let saida: number;

        for (let i = 0; i < this.regras[variavel].partes.length; i++) {
            let parte = this.regras[variavel].partes[i];

            for (let j = 0; j < this.indices[parte].tokens.length; j++) {
                let token = this.indices[parte].tokens[j];
                if (!token.isVariavel) {
                    if (token.texto == terminal) {
                        saida = parte;
                    }
                    break;
                } else {
                    var primeiros = this.regras[token.texto].first;
                    if (primeiros.indexOf(terminal) !== -1) {
                        saida = parte;
                    }
                    break;
                }
            }
        }

        return saida;
    }

    montarTabela() {
        let tabela: any[] = [];

        this.variaveis.forEach((variavel) => {
            tabela[variavel] = [];

            this.terminais.forEach((terminal) => {

                if (this.regras[variavel].first.includes(terminal)) {
                    let indice = this.buscarIndice(variavel, terminal);
                    tabela[variavel][terminal] = indice;
                } else if (this.regras[variavel].follow.includes(terminal)) {
                    if (this.regras[variavel].first.includes('%')) {
                        let indice = this.buscarIndice(variavel, '%');
                        tabela[variavel][terminal] = indice;
                    }
                } else {
                    tabela[variavel][terminal] = null;
                }

            });

        });

        this.tabela = tabela;
    }

    identificarFirstFollow() {
        this.variaveis.forEach(variavel => {
            this.regras[variavel].first = getConjuntoFirst(variavel, this);

            let pilhaDeBusca = [];  // limpa pilha de variáveis buscadas
            this.regras[variavel].follow = getConjuntoFollow(variavel, variavel, true, this, pilhaDeBusca);
        });
    }
}

export interface Indice {
    texto: string;
    tokens: Token[];
    variavel: string;
}
