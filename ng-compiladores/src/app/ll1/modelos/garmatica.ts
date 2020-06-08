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

export interface IGramatica {
    variaveis: string[];
    terminais: string[];
    regras: { [key: string]: GramaticaRegra };
    indices: Indice[]
    tamanho: number;
    tabela: any[];
}

export interface Indice {
    texto: string;
    tokens: Token[];
    variavel: string;
}

export class Gramatica implements IGramatica {
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
}
